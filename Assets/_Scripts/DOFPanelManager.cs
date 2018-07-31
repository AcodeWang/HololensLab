using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;

public class DOFPanelManager : MonoBehaviour
{

    public GameObject m_original;

    /// <summary>
    /// Keeps track of if the user is moving the object or not.
    /// Setting this to true will enable the user to move and place the object in the scene.
    /// Useful when you want to place an object immediately.
    /// </summary>
    [Tooltip("Setting this to true will enable the user to move and place the object in the scene without needing to tap on the object. Useful when you want to place an object immediately.")]
    public bool IsBeingPlaced;

    [Tooltip("Setting this to true will allow this behavior to control the DrawMesh property on the spatial mapping.")]
    public bool AllowMeshVisualizationControl = true;

    [Tooltip("Distance from camera to keep the object while placing it.")]
    public float DefaultGazeDistance = 2.0f;

    /// <summary>
    /// The default ignore raycast layer built into unity.
    /// </summary>
    private const int IgnoreRaycastLayer = 2;

    private Dictionary<GameObject, int> layerCache = new Dictionary<GameObject, int>();
    private Vector3 PlacementPosOffset;

    public ToggleGroup fineTuningToggleGroup;
    public Toggle currentFineTuningToggle
    {
        get { return fineTuningToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    enum FineTuningToggle
    {
        Px, Py, Pz, Rx, Ry, Rz
    }

    FineTuningToggle currentFineTuning = FineTuningToggle.Py;

    private float fineTuningCoef = 1f;

    public ToggleGroup coefTuningToggleGroup;
    public Toggle currentCoefToggle
    {
        get { return coefTuningToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        //transform.position = m_original.transform.position + new Vector3(0.4f, 0, 0.4f);

        if (!IsBeingPlaced) { return; }
        Transform cameraTransform = CameraCache.Main.transform;

        RaycastHit hitInfo;
        if (SpatialMappingRaycast(cameraTransform.position, cameraTransform.forward, out hitInfo))
        {
            m_original.transform.rotation = Quaternion.LookRotation(Vector3.up, hitInfo.normal);
        }

        Vector3 placementPosition = GetPlacementPosition(cameraTransform.position, cameraTransform.forward, DefaultGazeDistance);

        // update the placement to match the user's gaze.
        m_original.transform.position = placementPosition;

        // Rotate this object to face the user.
        //interpolator.SetTargetRotation(Quaternion.Euler(0, cameraTransform.localEulerAngles.y, 0));
    }

    public void HandlePlacement()
    {
        if (IsBeingPlaced)
        {
            StartPlacing();
        }
        else
        {
            StopPlacing();
        }
    }
    private void StartPlacing()
    {
        var layerCacheTarget = m_original;
        layerCacheTarget.SetLayerRecursively(IgnoreRaycastLayer, out layerCache);
        InputManager.Instance.PushModalInputHandler(gameObject);

        ToggleSpatialMesh();
        RemoveWorldAnchor();
    }

    private void StopPlacing()
    {
        var layerCacheTarget = m_original;
        layerCacheTarget.ApplyLayerCacheRecursively(layerCache);
        InputManager.Instance.PopModalInputHandler();

        ToggleSpatialMesh();
        AttachWorldAnchor();
    }

    private void AttachWorldAnchor()
    {
        if (WorldAnchorManager.Instance != null)
        {
            // Add world anchor when object placement is done.
            WorldAnchorManager.Instance.AttachAnchor(m_original);
        }
    }

    private void RemoveWorldAnchor()
    {
        if (WorldAnchorManager.Instance != null)
        {
            //Removes existing world anchor if any exist.
            WorldAnchorManager.Instance.RemoveAnchor(m_original);
        }
    }

    /// <summary>
    /// If the user is in placing mode, display the spatial mapping mesh.
    /// </summary>
    private void ToggleSpatialMesh()
    {
        if (SpatialMappingManager.Instance != null && AllowMeshVisualizationControl)
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = IsBeingPlaced;
        }
    }

    /// <summary>
    /// If we're using the spatial mapping, check to see if we got a hit, else use the gaze position.
    /// </summary>
    /// <returns>Placement position in front of the user</returns>
    private static Vector3 GetPlacementPosition(Vector3 headPosition, Vector3 gazeDirection, float defaultGazeDistance)
    {
        RaycastHit hitInfo;
        if (SpatialMappingRaycast(headPosition, gazeDirection, out hitInfo))
        {
            return hitInfo.point;
        }
        return GetGazePlacementPosition(headPosition, gazeDirection, defaultGazeDistance);
    }

    /// <summary>
    /// Does a raycast on the spatial mapping layer to try to find a hit.
    /// </summary>
    /// <param name="origin">Origin of the raycast</param>
    /// <param name="direction">Direction of the raycast</param>
    /// <param name="spatialMapHit">Result of the raycast when a hit occurred</param>
    /// <returns>Whether it found a hit or not</returns>
    private static bool SpatialMappingRaycast(Vector3 origin, Vector3 direction, out RaycastHit spatialMapHit)
    {
        if (SpatialMappingManager.Instance != null)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(origin, direction, out hitInfo, 30.0f, SpatialMappingManager.Instance.LayerMask))
            {
                spatialMapHit = hitInfo;
                return true;
            }
        }
        spatialMapHit = new RaycastHit();
        return false;
    }

    /// <summary>
    /// Get placement position either from GazeManager hit or in front of the user as backup
    /// </summary>
    /// <param name="headPosition">Position of the users head</param>
    /// <param name="gazeDirection">Gaze direction of the user</param>
    /// <param name="defaultGazeDistance">Default placement distance in front of the user</param>
    /// <returns>Placement position in front of the user</returns>
    private static Vector3 GetGazePlacementPosition(Vector3 headPosition, Vector3 gazeDirection, float defaultGazeDistance)
    {
        if (GazeManager.Instance.HitObject != null)
        {
            return GazeManager.Instance.HitPosition;
        }
        return headPosition + gazeDirection * defaultGazeDistance;
    }

    public void FineTuningX10(bool ischecked)
    {
        if (ischecked)
        {
            fineTuningCoef = 10;
        }
    }

    public void FineTuningX1(bool ischecked)
    {
        if (ischecked)
        {
            fineTuningCoef = 1;
        }
    }

    public void FineTuningX01(bool ischecked)
    {
        if (ischecked)
        {
            fineTuningCoef = 0.1f;
        }
    }

    public void FineTuningPx(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[0];
            currentFineTuning = FineTuningToggle.Px;
        }
    }

    public void FineTuningPy(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[1];
            currentFineTuning = FineTuningToggle.Py;
        }
    }

    public void FineTuningPz(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[2];
            currentFineTuning = FineTuningToggle.Pz;
        }
    }

    public void FineTuningRx(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[3];
            currentFineTuning = FineTuningToggle.Rx;
        }
    }

    public void FineTuningRy(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[4];
            currentFineTuning = FineTuningToggle.Ry;
        }
    }

    public void FineTuningRz(bool ischecked)
    {
        if (ischecked)
        {
            //guideImage.texture = guideTextures[5];
            currentFineTuning = FineTuningToggle.Rz;
        }
    }

    public void FineTuningPlus()
    {
        if (currentFineTuningToggle == null)
        {
            return;
        }
        else if (currentFineTuning == FineTuningToggle.Px)
        {
            m_original.transform.Translate(new Vector3(0.01f * fineTuningCoef, 0, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Py)
        {
            m_original.transform.Translate(new Vector3(0, 0.01f * fineTuningCoef, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Pz)
        {
            m_original.transform.Translate(new Vector3(0, 0.01f * fineTuningCoef, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Rx)
        {
            m_original.transform.Rotate(transform.right, 0.5f * fineTuningCoef);
        }
        else if (currentFineTuning == FineTuningToggle.Ry)
        {
            m_original.transform.Rotate(transform.up, -0.5f * fineTuningCoef);
        }
        else if (currentFineTuning == FineTuningToggle.Rz)
        {
            m_original.transform.Rotate(transform.forward, -0.5f * fineTuningCoef);
        }

    }

    public void FineTuningMinus()
    {
        if (currentFineTuningToggle == null)
        {
            return;
        }
        else if (currentFineTuning == FineTuningToggle.Px)
        {
            m_original.transform.Translate(new Vector3(-0.01f * fineTuningCoef, 0, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Py)
        {
            m_original.transform.Translate(new Vector3(0, -0.01f * fineTuningCoef, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Pz)
        {
            m_original.transform.Translate(new Vector3(0, -0.01f * fineTuningCoef, 0));
        }
        else if (currentFineTuning == FineTuningToggle.Rx)
        {
            m_original.transform.Rotate(transform.right, -0.5f * fineTuningCoef);
        }
        else if (currentFineTuning == FineTuningToggle.Ry)
        {
            m_original.transform.Rotate(transform.up, 0.5f * fineTuningCoef);
        }
        else if (currentFineTuning == FineTuningToggle.Rz)
        {
            m_original.transform.Rotate(transform.forward, 0.5f * fineTuningCoef);
        }
    }
}
