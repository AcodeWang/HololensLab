using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HoloToolkit.Sharing;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif

public class GameRoomManager : MonoBehaviour {

    static string path;

    // Use this for initialization
    void Start () {

        path = Path.Combine(Application.persistentDataPath, "MyFile.txt");

        if (File.Exists(path)){
            GameRoomManager.ReadAnchorsFromFile();
            Debug.Log("Read");
        }
        else
        {
            GameRoomManager.WriteAnchorsToFile();
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public static void WriteAnchorsToFile(List<byte> data = null)
    {
        using (StreamWriter writer = File.CreateText(path))
        {
            if(data != null)
            {
                writer.Write(data);
            }
        }
    }

    public static List<byte> ReadAnchorsFromFile()
    {
        List<byte> data = new List<byte>();
        data.AddRange(File.ReadAllBytes(path));
        return data;
    }
}
