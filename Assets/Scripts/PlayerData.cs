using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class PlayerData
{
    private string path = "ProgressData.json";
    private ProgressData _monsterProgressData;
    private SaveLoadSystem _saveLoadSystem;

    private List<MonsterProgressData> _progressData;

    public PlayerData(SaveLoadSystem saveLoadSystem)
    {
        _saveLoadSystem = saveLoadSystem;
        _monsterProgressData = _saveLoadSystem.Load<ProgressData>(path,false);
    }

    public void SetDefeated(string modelName, RankType rank, StyleType style, bool isDefeated)
    {
        if (_monsterProgressData == null) return;
        var progressData = _monsterProgressData.progresses.FirstOrDefault(x => x.name == modelName &&
                                                                               x.rank == rank &&
                                                                               x.style == style);
        if (progressData == null)
        {
            MonsterProgressData data = new MonsterProgressData();
            data.name = modelName;
            data.rank = rank;
            data.style = style;
            progressData = data;
            _monsterProgressData.progresses.Add(data);
        }

        progressData.isDefeated = isDefeated;
        _saveLoadSystem.Save(path,_monsterProgressData,SaveComplete);
    }

    private void SaveComplete(bool obj)
    {
        Debug.Log("Save " + obj);
    }

    public bool GetDefeated(MonsterModel model)
    {
        if (_monsterProgressData == null) return false;

        var progressData = _monsterProgressData.progresses.FirstOrDefault(x => x.name == model.name &&
                                                                               x.rank == model.rank &&
                                                                               x.style == model.style);

        if (progressData == null) return false;

        return progressData.isDefeated;
    }
}