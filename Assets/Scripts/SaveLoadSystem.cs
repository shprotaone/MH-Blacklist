using System;
using System.IO;
using System.Text;
using Data.JSON;
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

    public T Load<T>(string key, bool fromResources)
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
            return data;
        }
        catch(Exception ex)
        {
            Debug.LogWarning(ex);
            using (FileStream fileStream = new FileStream(path,FileMode.OpenOrCreate))
            {
                var data = new ProgressData();

                var empty = JsonUtility.ToJson(data);
                fileStream.Write(Encoding.Default.GetBytes(empty));
                return JsonUtility.FromJson<T>(empty);
            }
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
}