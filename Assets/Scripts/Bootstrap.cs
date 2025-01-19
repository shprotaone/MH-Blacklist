using System.Collections;
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
    [SerializeField] private CurtainSystem _curtainSystem;
    [SerializeField] private MonsterListChanger _monsterListChanger;

    private GlobalSystems _globalSystems;
    private PlayerData _playerData;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private SettingsController _settingsController;
    private MonsterTierList _monsterTierList;

    private async void Start()
    {
        _globalSystems = new GlobalSystems();
        _languageProvider = new LanguageProvider();
        _saveLoadSystem = new SaveLoadSystem();
        _settingsController = new SettingsController();
        _monsterTierList = new MonsterTierList(_languageProvider);

        _saveLoadSystem.Initialize(_assetProvider);
        _playerData = new PlayerData(_saveLoadSystem);
        _uiController.Initialize(_monsterListChanger);
        _monsterListChanger.Initialize(_uiController, _cellFactory, _findSystem, _designChanger, _progressSeeker,
            _curtainSystem,_globalSystems, _monsterTierList);
        await _cellFactory.Initialize(_globalSystems, _assetProvider, _uiController);

        StartCoroutine(Loading());
        _languageProvider.OnLanguageChange += ChangeLanguage;

        _designChanger.Initialize(_assetProvider);
        _settingsController.Initialize(_uiController, _languageProvider, _saveLoadSystem);
    }

    private void ChangeLanguage()
    {
        _uiController.ClearScrollList();
        _languageProvider.ChangeLanguageText();
        _monsterListChanger.LoadMonsters();
    }

    private IEnumerator Loading()
    {
        _globalSystems.Initialize(_assetProvider, _playerData, _languageProvider, _progressSeeker);
        _globalSystems.SetLanguage(_language);
        _curtainSystem.Show();

        var riseList = _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath, true);
        _monsterListChanger.SetMonsterList(riseList, StyleType.RISE);
        var worldList = _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath, true);
        _monsterListChanger.SetMonsterList(worldList, StyleType.WORLD);
        _findSystem.Initialize(_globalSystems, _uiController);

        _monsterListChanger.SetCurrentMonsterList(StyleType.RISE);
        _monsterListChanger.LoadMonsters();
        _uiController.SetScaledButton(StyleType.RISE);

        yield break;
    }
}