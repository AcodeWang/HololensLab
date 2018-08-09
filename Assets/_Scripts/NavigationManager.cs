using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    public GameObject navGO;
    public TMPro.TextMeshPro navText;

    public GameObject navTarget;

    public GameObject ARCamera;

    // Check if the cursor direction indicator is visible.
    private bool isDirectionIndicatorVisible;

    [Tooltip("Allowable percentage inside the holographic frame to continue to show a directional indicator.")]
    [Range(-0.3f, 0.3f)]
    public float VisibilitySafeFactor = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // The cursor indicator should only be visible if the target is not visible.
        isDirectionIndicatorVisible = !IsTargetVisible(ARCamera.GetComponent<Camera>());

        if(!isDirectionIndicatorVisible && Vector3.Distance(ARCamera.transform.position, navTarget.transform.position) < 1f)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = isDirectionIndicatorVisible;
                gameObject.SetActive(false);
            }

            FindObjectOfType<Scenario>().StopScenerio();

            return;
        }
        else
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }

        navGO.transform.position = ARCamera.transform.position + ARCamera.transform.forward * 0.45f - Vector3.up * 0.06f;

        Vector3 orient = navTarget.transform.position + navTarget.transform.up * 1f - ARCamera.transform.position;

        navGO.transform.GetChild(0).rotation = Quaternion.LookRotation(-new Vector3(orient.x, orient.y, orient.z), Vector3.up);

        navText.text = Vector3.Distance(ARCamera.transform.position, navTarget.transform.position).ToString("F1") + "m";
    }

    public void StartNav()
    {
        navGO.SetActive(!navGO.activeSelf);
    }

    private bool IsTargetVisible(Camera mainCamera)
    {
        // This will return true if the target's mesh is within the Main Camera's view frustums.
        Vector3 targetViewportPosition = mainCamera.WorldToViewportPoint(navTarget.transform.position);
        return (targetViewportPosition.x > VisibilitySafeFactor && targetViewportPosition.x < 1 - VisibilitySafeFactor &&
                targetViewportPosition.y > VisibilitySafeFactor && targetViewportPosition.y < 1 - VisibilitySafeFactor &&
                targetViewportPosition.z > 0);
    }
}
