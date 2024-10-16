using System;
using System.Collections;
using System.IO;
using Data;
using UnityEngine;
using UnityEngine.Networking;

/*public class ParseSystem
{
    private bool _isLoaded;
    public event Action<Monsters> OnLoadMonsterComplete;

    public IEnumerator LoadFile(string path)
    {

        #if UNITY_EDITOR
        var json = File.ReadAllText(path);

        Monsters monsters = JsonUtility.FromJson<Monsters>(json);
        OnLoadMonsterComplete?.Invoke(monsters);
        _isLoaded = true;
        #endif

        #if UNITY_ANDROID
        if (_isLoaded) yield break;

        Debug.Log("TRY");
        var www = new UnityWebRequest(path);
        while (!www.isDone)
        {
            Debug.Log("WAIT");
            yield return new WaitForSeconds(0.5f);
        }

        var monsters2 = JsonUtility.FromJson<Monsters>(www.downloadHandler.text);
        OnLoadMonsterComplete?.Invoke(monsters2);

         #endif

        #if UNITY_STANDALONE_WIN
         var json = File.ReadAllText(path);

         Monsters monsters = JsonUtility.FromJson<Monsters>(json);
         return monsters;
        #endif
    }
}*/


