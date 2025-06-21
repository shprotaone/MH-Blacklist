using System;
using System.Collections.Generic;
using Cell;
using Data;
using Data.JSON;
using Enums;
using Parser;
using Storages;
using Systems.Factory;
using Systems.UI;
using UnityEngine;

namespace Systems
{
    public class GlobalSystems
    {
        public event Action OnChangeStyle;

        private static GlobalSystems _instance;
        private LanguageProvider _languageProvider;
        private AssetProvider _assetProvider;
        private PlayerDataParser _playerDataParser;
        private ProgressSeekerView _progressSeekerView;
        private KillList _killList;
        private UIController _uiController;
        private MonsterResourcesParser _monsterResourcesParser;
        private InputSystemHandler _inputSystemHandler;
        private CurtainSystem _curtainSystem;
        private SaveLoadSystem _saveLoadSystem;
        private MonsterTierListStorage _monsterTierListStorage;
        private MonsterListChanger _monsterListChanger;
        private SettingsController _settingsController;
        private AppData _appData;
        private CellFactory _cellFactory;
        private FindSystem _findSystem;
        private DesignChanger _designChanger;

        private GlobalSystems(){ }

        public static GlobalSystems Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalSystems();
                }

                return _instance;
            }
        }
        public LanguageProvider LanguageProvider => _languageProvider;
        public MonsterResourcesParser MosterResourcesParser => _monsterResourcesParser;
        public InputSystemHandler InputSystemHandler => _inputSystemHandler;
        public PlayerDataParser PlayerDataParser => _playerDataParser;
        public StyleType CurrentStyle { get; set; }
        public SettingsController SettingsController => _settingsController;
        public KillList KillList => _killList;
        public MonsterTierListStorage TierListStorage => _monsterTierListStorage;
        public CurtainSystem CurtainSystem => _curtainSystem;
        public SaveLoadSystem SaveLoadSystem => _saveLoadSystem;
        public MonsterListChanger MonsterListChanger => _monsterListChanger;
        public AssetProvider AssetProvider => _assetProvider;

        public void Initialize(CellFactory cellFactory, FindSystem findSystem, DesignChanger designChanger, AssetProvider assetProvider, UIController uiController)
        {
            _languageProvider = new LanguageProvider();
            _saveLoadSystem = new SaveLoadSystem();
            _killList = new KillList();
            _monsterTierListStorage = new MonsterTierListStorage(_languageProvider);
            _settingsController = new SettingsController();
            _monsterResourcesParser = new MonsterResourcesParser();
            _playerDataParser = new PlayerDataParser(_saveLoadSystem);
            _inputSystemHandler = new InputSystemHandler();
            _monsterListChanger = new MonsterListChanger();
            _curtainSystem = new CurtainSystem();
            _cellFactory = cellFactory;
            _findSystem = findSystem;
            _designChanger = designChanger;
            _progressSeekerView = uiController.ProgressSeeker;
            _uiController = uiController;
            _assetProvider = assetProvider;
            _saveLoadSystem.Initialize(assetProvider);
            _settingsController.Initialize(this,_uiController);
            _monsterListChanger.Initialize(_uiController,_cellFactory,_findSystem,_designChanger,this);
            _languageProvider.OnLanguageChange += ChangeLanguage;
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

        public Sprite GetSprite(RankType imageName)
        {
            return _assetProvider.GetRankSprite(imageName);
        }

        public Sprite GetSprite(WeaknessType weaknessType)
        {
            return _assetProvider.GetWeaknessSprite(weaknessType);
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
            _progressSeekerView.UpdateSlider();
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

        public void AddToKillList(MonsterModel model)
        {
            _killList.TryAddToList(model);
        }

        public void CallDetail(MonsterModel model)
        {
            _uiController.FillDetailedList(model);
        }

        public Sprite GetSprite(WeaknessStatusType weaknessType, StyleType style)
        {
            return _assetProvider.GetWeaknessSprite(weaknessType, style);
        }

        public Sprite GetSprite(AttackType attackType)
        {
            return _assetProvider.GetAttackSprite(attackType);
        }

        public Lang GetLang()
        {
            return _languageProvider.GetLanguage();
        }

        private void ChangeLanguage()
        {
            _languageProvider.ChangeLanguageText();
        }

        public void SetMonsterList()
        {
            if (_playerDataParser.AppData.lastStyle == "RISE") CurrentStyle = StyleType.RISE;
            else if (_playerDataParser.AppData.lastStyle == "WORLD") CurrentStyle = StyleType.WORLD;
            else if (_playerDataParser.AppData.lastStyle == "WILDS") CurrentStyle = StyleType.WILDS;
        
            _monsterListChanger.SetCurrentMonsterList(CurrentStyle);
            OnChangeStyle?.Invoke();
        }

        public Sprite GetSprite(LocationType location)
        {
            return _assetProvider.GetSprite(location);
        }

        public Sprite GetDefeatSprite(bool isDefeated)
        {
            return _assetProvider.GetDefeatSprite(CurrentStyle, isDefeated);
        }

        public Sprite GetCellBackground()
        {
            return _assetProvider.GetCellBackground(CurrentStyle);
        }

        public Sprite GetDetailBackground()
        {
            return _assetProvider.GetDetailBackground(CurrentStyle);
        }

        public List<RankType> GetRanks(MonsterModel model)
        {
            return TierListStorage.GetRankList(model);
        }

        public string GetTextSpecialStatus(MonsterModel model)
        {
            return _languageProvider.GetSpecialState(model.specialWeaknessState);
        }
    }
}