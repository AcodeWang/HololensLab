using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjModels : MonoBehaviour
{

    private static ObjModelsData _objModelsData;
    private static List<ObjModelData> _objModels = new List<ObjModelData>();

    public static void InitObjModelsDataFromJson(string jsonPath)
    {
        TextAsset jsonString = Resources.Load(jsonPath) as TextAsset;

        //need to change the format of json file add sdkModels object.
        //jsonString. = jsonString.text.Insert(0, "{ /n " + "sdkModels" + ":").Insert(jsonString.text.Length - 1, "}");
        //Debug.Log(jsonString.text);

        _objModelsData = JsonUtility.FromJson<ObjModelsData>(jsonString.text);
        foreach (ObjModelData objModelData in _objModelsData.objModels)
        {
            _objModels.Add(objModelData);

            foreach (ReferencePointData referencePointData in objModelData.referencePoints)
            {
                referencePointData.position.x = -referencePointData.position.x;
            }
        }
    }

    public static List<ObjModelData> getObjModels()
    {
        return _objModels;
    }

    // Use this for initialization
    void Start()
    {
        //InitObjModelsDataFromJson("Models/ObjModels/Descriptions/PrismaPSmartPanelShowroomEQI_RP");
        InitObjModelsDataFromJson("Models/ObjModels/Descriptions/PrismaG_HP4_RP");
        //InitObjModelsDataFromJson("Models/ObjModels/Descriptions/TableauFDLab_RP");

        ModelList();
    }

    public void ModelList()
    {
        GameObject myModel = new GameObject("myModel");
        //myModel.AddComponent<Model>().InitModelFromJson(ModelType.SDK3D, "PrismaPSmartPanelShowroomEQI");
        myModel.AddComponent<Model>().InitModelFromJson(ModelType.SDK3D, "PrismaG_HP4");
        //myModel.AddComponent<Model>().InitModelFromJson(ModelType.SDK3D, "TableauFDLab");
        myModel.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        myModel.transform.position = new Vector3(0, 0, 3f);

        myModel.AddComponent<HoloToolkit.Unity.Billboard>();
        myModel.AddComponent<BoxCollider>().size = new Vector3(1000,1000,1000);
        myModel.AddComponent<HoloToolkit.Unity.SpatialMapping.TapToPlace>();

        foreach (Renderer renderer in myModel.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }

    }

}
