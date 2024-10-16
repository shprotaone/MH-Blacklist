using System;
using System.IO;
using System.Text;
using Data;
using UnityEngine;

public class SaveLoadSystem
{
    private AssetProvider _assetProvider;

    public void Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key,false);
        string json = JsonUtility.ToJson(data);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback,bool fromResources)
    {
        string path = BuildPath(key,fromResources);
        string json = "";

        try
        {
            if (fromResources)
            {
                json = _assetProvider.GetText(key);
            }
            else
            {
                using (var filestream = new StreamReader(path))
                {
                    json = filestream.ReadToEnd();
                }
            }

            var data = JsonUtility.FromJson<T>(json);
            callback.Invoke(data);
        }
        catch(Exception ex)
        {
            Debug.LogWarning(ex);
            using (FileStream fileStream = new FileStream(path,FileMode.OpenOrCreate))
            {
                var data = new ProgressData();

                var empty = JsonUtility.ToJson(data);
                fileStream.Write(Encoding.Default.GetBytes(empty));
                callback.Invoke(JsonUtility.FromJson<T>(empty));
            }


            // var data = new ProgressData();
            // Save(path,data);
            // var result = JsonUtility.FromJson<T>("");
            // callback.Invoke(result);
            // Delete(key);
            return;
        }
    }

    public void Delete(string key)
    {
        File.Delete(BuildPath(key,true));
        Debug.LogWarning("Сохранение удалено");
    }

    private string BuildPath(string key,bool fromResources)
    {
        string path = "";
        if (fromResources)
        {
            path = key;
        }
        else
        {
            path = Path.Combine(Application.persistentDataPath, key);
        }

        Debug.Log("Path " + path);
        return path;
    }


    /*
    public ProgressData LoadProgressData()
    {
        /*ProgressData data;
        if (!File.Exists(path))
        {
            using (FileStream fileStream = new FileStream(path,FileMode.OpenOrCreate))
            {
                data = new ProgressData();

                var empty = JsonUtility.ToJson(data);
                fileStream.Write(Encoding.Default.GetBytes(empty));
            }

            return data;
        }

        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<ProgressData>(json);

        return data;#1#
        return null;
    }

    public void SaveProgressData(ProgressData monsterProgressData)
    {
        // var json = JsonUtility.ToJson(monsterProgressData);
        // File.WriteAllText(path,json);

    }*/
}