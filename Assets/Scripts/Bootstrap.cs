using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Data;
using DefaultNamespace;
using Systems;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _language;
    [SerializeField] private AssetProvider _assetProvider;
    [SerializeField] private FindSystem _findSystem;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ProgressSeeker _progressSeeker;
    [SerializeField] private DesignChanger _designChanger;
    [SerializeField] private CellFactory _cellFactory;
    [SerializeField] private MonsterListChanger _monsterListChanger;
    [SerializeField] private QuickMonsterListController _quickMonsterListController;

    private CurtainSystem _curtainSystem;
    private GlobalSystems _globalSystems;
    private PlayerData _playerData;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private MonsterTierList _monsterTierList;
    private KillList _killList;
    private SettingsController _settingsController;

    private async void Start()
    {
        await InitCurtain();
        _globalSystems = new GlobalSystems();
        _languageProvider = new LanguageProvider();
        _saveLoadSystem = new SaveLoadSystem();
        _killList = new KillList();
        _monsterTierList = new MonsterTierList(_languageProvider);
        _settingsController = new SettingsController();


        _saveLoadSystem.Initialize(_assetProvider);
        _playerData = new PlayerData(_saveLoadSystem);
        await _uiController.Initialize(_assetProvider,_settingsController);
        _settingsController.Initialize(_uiController,_languageProvider,_saveLoadSystem,_monsterListChanger);
        _quickMonsterListController.Initialize(_killList,_assetProvider);
        _monsterListChanger.Initialize(_uiController, _cellFactory, _findSystem, _designChanger, _progressSeeker,
            _curtainSystem,_globalSystems, _monsterTierList);
        await _cellFactory.Initialize(_globalSystems, _assetProvider, _uiController);

        StartCoroutine(Loading());
        _languageProvider.OnLanguageChange += ChangeLanguage;

        _designChanger.Initialize(_assetProvider);
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
        _languageProvider.ChangeLanguageText();
        _monsterListChanger.LoadMonsters();
    }

    private IEnumerator Loading()
    {
        _globalSystems.Initialize(_assetProvider, _playerData, _languageProvider, _progressSeeker,_killList,_uiController);
        _globalSystems.SetLanguage(_language);

        var riseList = _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath, true);
        _monsterListChanger.SetMonsterList(riseList, StyleType.RISE);
        var worldList = _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath, true);
        _monsterListChanger.SetMonsterList(worldList, StyleType.WORLD);
        _findSystem.Initialize(_globalSystems, _uiController);

        _monsterListChanger.SetCurrentMonsterList(StyleType.RISE);
        _curtainSystem.Show();
        _settingsController.SetScaledButton(StyleType.RISE);

        yield break;
    }
}