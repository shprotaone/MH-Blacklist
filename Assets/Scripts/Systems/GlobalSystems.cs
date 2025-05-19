using System;
using Cell;
using Data;
using Data.JSON;
using Enums;
using Parser;
using UnityEngine;

namespace Systems
{
    public class GlobalSystems
    {
        public event Action OnChangeStyle;

        private GlobalSystems _instance;
        private LanguageProvider _languageProvider;
        private AssetProvider _assetProvider;
        private PlayerDataParser _playerDataParser;
        private ProgressSeeker _progressSeeker;
        private KillList _killList;
        private UIController _uiController;
        private MonsterResourcesParser _monsterResourcesParser;
        private InputSystemHandler _inputSystemHandler;

        public LanguageProvider LanguageProvider => _languageProvider;
        public MonsterResourcesParser MosterResourcesParser => _monsterResourcesParser;
        public InputSystemHandler InputSystemHandler => _inputSystemHandler;
        public PlayerDataParser PlayerDataParser => _playerDataParser;
        public StyleType CurrentStyle { get; set; }

        public void Initialize(AssetProvider assetProvider,PlayerDataParser playerDataParser,
            LanguageProvider languageProvider,ProgressSeeker progressSeeker,
            KillList killList,UIController uiController,MonsterResourcesParser monsterResourcesParser)
        {
            _progressSeeker = progressSeeker;
            _languageProvider = languageProvider;
            _assetProvider = assetProvider;
            this._playerDataParser = playerDataParser;
            _killList = killList;
            _uiController = uiController;
            _monsterResourcesParser = monsterResourcesParser;
            _inputSystemHandler = new InputSystemHandler();
        }

        public Sprite GetSprite(string imageName)
        {
            return _assetProvider.GetSprite(imageName);
        }

        public bool GetDefeated(MonsterModel model)
        {
            return _playerDataParser.GetDefeated(model);
        }

        public string GetName(string dataName)
        {
            return _languageProvider.GetName(dataName);
        }

        public Sprite GetSprite(RankType imageName, StyleType dataStyle)
        {
            return _assetProvider.GetRankSprite(imageName, dataStyle);
        }

        public Sprite GetSprite(WeaknessType weaknessType, StyleType style)
        {
            return _assetProvider.GetWeaknessSprite(weaknessType, style);
        }

        public string GetMonsterTypeName(MonsterType dataType)
        {
            return _languageProvider.GetTypeName(dataType);
        }

        public void SetLanguage(string language)
        {
            _languageProvider.SetLanguage(language);
        }

        public void SetDefeatedState(MonsterModel model, bool isDefeated)
        {
            _playerDataParser.SetDefeated(model,isDefeated);
            _progressSeeker.UpdateSlider();
        }

        public string GetStyle(StyleType modelStyle)
        {
            if (modelStyle == StyleType.RISE)
            {
                return "RISE";
            }

            if (modelStyle == StyleType.WORLD)
            {
                return "WORLD";
            }

            if (modelStyle == StyleType.WILDS)
            {
                return "WILDS";
            }

            return " ";
        }

        public void ChangeStyle()
        {
            OnChangeStyle?.Invoke();
            _playerDataParser.SaveAppData(_languageProvider.GetLanguageString(),CurrentStyle);
        }

        public void AddToKillList(MonsterCell monsterCell)
        {
            _killList.TryAddToList(monsterCell.Model);
        }

        public void CallDetail(MonsterModel model)
        {
            _uiController.FillDetailedList(model);
        }

        public Sprite GetSprite(WeaknessStatusType weaknessType, StyleType style)
        {
            return _assetProvider.GetWeaknessSprite(weaknessType, style);
        }

        public Lang GetLang()
        {
            return _languageProvider.GetLanguage();
        }
    }
}