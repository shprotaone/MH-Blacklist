using System;
using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using Enums;
using UnityEngine;

namespace Parser
{
    public class PlayerDataParser
    {
        private string path = "ProgressData.json";
        private string appDataPath = "AppData.json";
        private ProgressData _monsterProgressData;
        private SaveLoadSystem _saveLoadSystem;
        private AppData _appData;

        private List<MonsterProgressData> _progressData;
        public AppData AppData => _appData;

        public PlayerDataParser(SaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            _monsterProgressData = _saveLoadSystem.Load<ProgressData>(path,false);
            _appData = _saveLoadSystem.Load<AppData>(appDataPath, false);

            if (_appData == null || _appData.lastLang == String.Empty || _appData.lastStyle == String.Empty)
            {
                _appData = new AppData("RUS", "RISE");
                _saveLoadSystem.Save(appDataPath,_appData);
            }
            
            Debug.Log(_appData.lastLang + " " + _appData.lastStyle);
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

        public void SaveAppData(StyleType style)
        {
            if (style == StyleType.RISE)
            {
                _appData.lastStyle = "RISE";
            }
            else if (style == StyleType.WORLD)
            {
                _appData.lastStyle = "WORLD";
            }
            else if (style == StyleType.WILDS)
            {
                _appData.lastStyle = "WILDS";
            }
            
            _saveLoadSystem.Save(appDataPath,_appData);
        }

        public void SaveAppData(string lang)
        {
            _appData.lastLang = lang;
            _saveLoadSystem.Save(appDataPath, _appData);
        }
    }
}