using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    static int count = 1;

    [SerializeField]
    private ModelData _modelData;

    private string _name;
    private ModelType _modelType;
    private string _filename;
    private int _id;
    private POIData[] _POIs;

    public Model InitModelFromJson(ModelType modelType, string modelName)
    {
        string jsonPath = "Models/" + modelType.ToString() + "/" + modelName;
        Debug.Log(jsonPath);

        switch (modelType)
        {
            case ModelType.SDK3D:
                {
                    gameObject.AddComponent<SDK3DModel>().InitSDK3DModelDataFromJson(jsonPath,transform);
                    _name = modelName;
                    _modelType = ModelType.SDK3D;
                    _filename = jsonPath;
                    _id = count++;
                    break;
                };
            case ModelType.Other:
                {
                    Debug.Log("Not this ModelType");
                    break;
                }      
        }

        SetModelData();

        return this;
    }

    public static void InitModelFromModelData(ModelData modelData)
    {
        GameObject myModel = new GameObject(modelData.name);
        myModel.AddComponent<Model>().InitModelFromJson(modelData.modelType, modelData.name);
        myModel.transform.position = modelData.position;
        myModel.transform.rotation = modelData.rotation;
        myModel.transform.localScale = modelData.localScale;
        myModel.tag = "Saveable";
    }

    private void SetModelData()
    {
        _modelData = new ModelData();

        _modelData.name = _name;
        _modelData.modelType = _modelType;
        _modelData.filename = _filename;
        _modelData.id = _id;
        _modelData.position = transform.position;
        _modelData.rotation = transform.rotation;
        _modelData.localScale = transform.localScale;
        _modelData.POIs = _POIs;
    }

    public ModelData Save()
    {
        SetModelData();
        return _modelData;
    }
}
