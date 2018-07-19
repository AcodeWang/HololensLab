using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectData {


}

[System.Serializable]
public class SaveData
{
    public ModelData[] modelList;
}


public enum ModelType { SDK3D, Other }


[System.Serializable]
public class ModelData
{

    public string name;
    public ModelType modelType;
    public string filename;
    public int id;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;
    public POIData[] POIs;

    public POIData[] GetPOIs()
    {
        return POIs;
    }

    public static ModelData SDK3DData2ModelData(SDK3DModelData sdk3DModelData)
    {
        ModelData modelData = new ModelData();

        modelData.modelType = ModelType.SDK3D;
        modelData.position = sdk3DModelData.transformation.position;
        modelData.rotation = Quaternion.Euler(sdk3DModelData.transformation.rotation);

        return modelData;
    }
}

[System.Serializable]
public class POIData
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class SDK3DModelData
{
    public string nodeId;
    public string modelId;
    public SDK3DParams modelParams;
    public SDK3DAdditionalInfo additionalInfo;
    public SDK3DModelData[] children;
    public SDK3DConnectionInfo connectionInfo;
    public SDK3DTransformation transformation;
}

[System.Serializable]
public class SDK3DParams
{
    public int num;
    public string str;
}

[System.Serializable]
public class SDK3DAdditionalInfo
{
    public string reference_id;
    public string layer_id;
    public string label;
}

[System.Serializable]
public class SDK3DTransformation
{
    public Vector3 position;
    public Vector3 rotation;
}

[System.Serializable]
public class SDK3DConnectionInfo
{
    public int refId;
    public int[] connectedIds;
}

[System.Serializable]
public class ObjModelsData
{
    public ObjModelData[] objModels;
}

[System.Serializable]
public class ObjModelData
{
    public int id;
    public string name;
    public string designation;
    public string filename;
    //public string diagram;
    public string partDescriptor;
    public ReferencePointData[] referencePoints;

    public static ModelData ObjModelData2ModelData(ObjModelData objModelData)
    {
        ModelData modelData = new ModelData();
        modelData.name = objModelData.name;
        modelData.filename = objModelData.filename;
        modelData.id = objModelData.id;

        List<POIData> tempPOIs = new List<POIData>();
        for(int i = 0; i < objModelData.referencePoints.Length; i++)
        {
            Debug.Log(i);
            POIData tempPOI = new POIData();
            tempPOI.id = i;
            tempPOI.position = objModelData.referencePoints[i].position;

            tempPOIs.Add(tempPOI);
        }
        modelData.POIs = tempPOIs.ToArray();
  
        return modelData;
    }
}

[System.Serializable]
public class ReferencePointData
{
    public string type;
    public int internalId;
    public string id;
    public Vector3 position;
    public Vector3 endPosition;
    public int model;
    public string shape;
    public string supportType;
    public string mountiongZoneID;
    public string index;
}
