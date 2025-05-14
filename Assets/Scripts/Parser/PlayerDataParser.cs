using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using UnityEngine;

namespace Parser
{
    public class PlayerDataParser
    {
        private string path = "ProgressData.json";
        private ProgressData _monsterProgressData;
        private SaveLoadSystem _saveLoadSystem;

        private List<MonsterProgressData> _progressData;

        public PlayerDataParser(SaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            _monsterProgressData = _saveLoadSystem.Load<ProgressData>(path,false);
        }

        public void SetDefeated(MonsterModel model, bool isDefeated)
        {
            if (_monsterProgressData == null) return;

            var progressData = _monsterProgressData.progresses.FirstOrDefault(x => x.name == model.name &&
                x.rank == model.rank &&
                x.style == model.style);
            if (progressData == null)
            {
                MonsterProgressData data = new MonsterProgressData();
                data.name = model.name;
                data.rank = model.rank;
                data.style = model.style;
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
}