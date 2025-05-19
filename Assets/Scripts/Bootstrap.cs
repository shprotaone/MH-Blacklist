using System.Collections;
using Cysharp.Threading.Tasks;
using Data;
using Data.JSON;
using Enums;
using Parser;
using Storages;
using Systems;
using Systems.Factory;
using Systems.UI;
using UnityEngine;
using View.Curtain;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AssetProvider _assetProvider;
    [SerializeField] private FindSystem _findSystem;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ProgressSeeker _progressSeeker;
    [SerializeField] private DesignChanger _designChanger;
    [SerializeField] private CellFactory _cellFactory;
    [SerializeField] private MonsterListChanger _monsterListChanger;
    [SerializeField] private QuickMonsterListView quickMonsterListView;

    private CurtainSystem _curtainSystem;
    private GlobalSystems _globalSystems;
    private PlayerDataParser _playerDataParser;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private MonsterTierListStorage monsterTierListStorage;
    private KillList _killList;
    private SettingsController _settingsController;
    private MonsterResourcesParser _monsterResourcesParser;
    private AppData _appData;

    private async void Start()
    {
        await InitCurtain();
        _globalSystems = new GlobalSystems();
        _languageProvider = new LanguageProvider();
        _saveLoadSystem = new SaveLoadSystem();
        _killList = new KillList();
        monsterTierListStorage = new MonsterTierListStorage(_languageProvider);
        _settingsController = new SettingsController();
        _monsterResourcesParser = new MonsterResourcesParser();
        _playerDataParser = new PlayerDataParser(_saveLoadSystem);

        _globalSystems.Initialize(_assetProvider, _playerDataParser, _languageProvider, _progressSeeker,
            _killList,_uiController,_monsterResourcesParser);

        _saveLoadSystem.Initialize(_assetProvider);
        await _uiController.Initialize(_assetProvider,_settingsController,_globalSystems);
        _settingsController.Initialize(_uiController,_languageProvider,_saveLoadSystem,_monsterListChanger);
        quickMonsterListView.Initialize(_killList,_assetProvider,_globalSystems);
        _monsterListChanger.Initialize(_uiController, _cellFactory, _findSystem, _designChanger, _progressSeeker,
            _curtainSystem,_globalSystems, monsterTierListStorage);
        await _cellFactory.Initialize(_globalSystems, _assetProvider, _uiController);

        StartCoroutine(Loading());
        _languageProvider.OnLanguageChange += ChangeLanguage;

        _designChanger.Initialize(_assetProvider);
        _languageProvider.ChangeLanguageText();
        Application.targetFrameRate = 60;
    }

    private async UniTask InitCurtain()
    {
        _curtainSystem = new CurtainSystem();
        var go = await _assetProvider.GetPrefabAsync("LoadingCurtain");
        var instance = Instantiate(go, _uiController.CurtainPanel, false);
        _curtainSystem.Initialize(instance.GetComponent<CurtainView>());
        _curtainSystem.Show();
    }

    private void ChangeLanguage()
    {
        //TODO: Как будто должно находится не тут
        
        _languageProvider.ChangeLanguageText();
        _monsterListChanger.LoadMonsters();
        _playerDataParser.SaveAppData(_languageProvider.GetLanguageString(), _globalSystems.CurrentStyle);
    }

    private IEnumerator Loading()
    {
        _monsterResourcesParser.Initialize(_assetProvider);
        _globalSystems.SetLanguage(_playerDataParser.AppData.lastLang);

        var riseList = _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath, true);
        _monsterListChanger.SetMonsterList(riseList, StyleType.RISE);
        var worldList = _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath, true);
        _monsterListChanger.SetMonsterList(worldList, StyleType.WORLD);
        var wildsList = _saveLoadSystem.Load<Monsters>(StaticData.wildsFilePath, true);
        _monsterListChanger.SetMonsterList(wildsList,StyleType.WILDS);
        
        _findSystem.Initialize(_globalSystems, _uiController);

        SetMonsterList();

        yield break;
    }

    private void SetMonsterList()
    {
        if (_playerDataParser.AppData.lastStyle == "RISE") _globalSystems.CurrentStyle = StyleType.RISE;
        else if (_playerDataParser.AppData.lastStyle == "WORLD") _globalSystems.CurrentStyle = StyleType.WORLD;
        else if (_playerDataParser.AppData.lastLang == "WILDS") _globalSystems.CurrentStyle = StyleType.WILDS;
        
        _monsterListChanger.SetCurrentMonsterList(_globalSystems.CurrentStyle);
        _settingsController.SetScaledButton(_globalSystems.CurrentStyle);
        _curtainSystem.Show();
        
    }
}