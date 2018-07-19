using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SDK3DModel : MonoBehaviour
{

    private SDK3DModelData _SDKModelData;

    public SDK3DModelData GetSDKModelData()
    {
        return _SDKModelData;
    }

    public void SetSDKModelData(SDK3DModelData sdkModelData)
    {
        _SDKModelData = sdkModelData;
    }

    private POI _POI;

    public void InitSDK3DModelDataFromJson(string jsonPath,Transform parent)
    {
        TextAsset jsonString = Resources.Load(jsonPath) as TextAsset;
        _SDKModelData = JsonUtility.FromJson<SDK3DModelData>(jsonString.text);

        //初始化Children x坐标值有问题，如果Children还包含其他children则并没有初始化
        _SDKModelData.transformation.position.x = -_SDKModelData.transformation.position.x;
        foreach (SDK3DModelData sdk3DModel in _SDKModelData.children)
        {
            sdk3DModel.transformation.position.x = -sdk3DModel.transformation.position.x;
        }

        InitSDK3DModel(_SDKModelData, parent);
    }

    public void InitSDK3DModel(SDK3DModelData sdk3DModelData, Transform parent)
    {
        string goPath = "Models/ObjModels/" + sdk3DModelData.modelId + "/" + sdk3DModelData.modelId;
        GameObject go = Resources.Load<GameObject>(goPath);

        //读取模型OBJ文件，如果存在则生成
        if (go)
        {
            go = Instantiate<GameObject>(Resources.Load<GameObject>(goPath), sdk3DModelData.transformation.position, Quaternion.Euler(sdk3DModelData.transformation.rotation), parent);
            go.transform.SetParent(parent);
            //if(sdk3DModelData.transformation.position.x > 0)
            //{
            //    sdk3DModelData.transformation.position.x = -sdk3DModelData.transformation.position.x;
            //}
            go.transform.localPosition = sdk3DModelData.transformation.position;
            go.AddComponent<SDK3DModel>().SetSDKModelData(sdk3DModelData);
            

            if (sdk3DModelData.additionalInfo.layer_id == "Device")
            {
                foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
                {
                    renderer.gameObject.AddComponent<MeshCollider>();

                    foreach (Material material in renderer.materials)
                    {
                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        material.SetInt("_ZWrite", 0);
                        material.DisableKeyword("_ALPHATEST_ON");
                        material.DisableKeyword("_ALPHABLEND_ON");
                        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        material.renderQueue = 3000;

                        material.SetFloat("_Mode", 3f);
                        material.SetColor("_Color", new Color(material.color.r, material.color.g, material.color.b, 0.2f));
                    }

                }

                foreach (ObjModelData objModel in ObjModels.getObjModels())
                {
                    if (objModel.name.ToLower() == sdk3DModelData.modelId || objModel.name == sdk3DModelData.modelId)
                    {
                        POI poi = go.AddComponent<POI>();

                        poi.modelId = sdk3DModelData.modelId;
                        poi.referenceId = sdk3DModelData.additionalInfo.reference_id;
                        poi.designation = objModel.designation;
                        poi.info = poi.referenceId + "\r\n" + objModel.designation;
                        poi.designationLabel = sdk3DModelData.additionalInfo.label;

                        //if (sdk3DModelData.additionalInfo.label != null)
                        //{
                        //    //poi.label = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Label"), go.transform);
                        //    poi.label = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/POI"), go.transform);
                        //    poi.label.transform.localPosition = new Vector3(-9, 0, 100);
                        //    //poi.label.transform.SetParent(null);
                        //    //poi.label.transform.localScale = poi.transform.localScale / 1000;
                        //    //poi.label.transform.position = poi.transform.position / 1000 + transform.forward * 0.06f;
                        //    //poi.label.GetComponentInChildren<TextMesh>().text = sdk3DModelData.additionalInfo.label;
                        //}

                        poi.connectionInfo = sdk3DModelData.connectionInfo;
                    }
                }
            }

            foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }

        }
        else
        {
            go = parent.gameObject;
        }

        if (sdk3DModelData.children != null)
        {
            foreach (SDK3DModelData sdkModel in sdk3DModelData.children)
            {
                InitSDK3DModel(sdkModel, go.transform);
            }
        }
    }

    void OnMouseDown()
    {

    }

    public string OnTouch()
    {
        return _POI.designation;
    }
}
