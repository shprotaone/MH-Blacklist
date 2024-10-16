using System.Collections;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using DefaultNamespace.View;
using Systems;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{


    [SerializeField] private string _language;
    [SerializeField] private AssetProvider _assetProvider;
    [SerializeField] private MonsterScrollView _scrollView;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private FindSystem _findSystem;
    [SerializeField] private UIController _uiController;

    private PlayerData _playerData;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private SettingsController _settingsController;
    private MonsterTierList _tierList;
    private Monsters _riseMonsters;
    private Monsters _worldMonsters;

    void Start()
    {
        _languageProvider = new LanguageProvider();
        _tierList = new MonsterTierList(_languageProvider);
        _saveLoadSystem = new SaveLoadSystem();
        _settingsController = new SettingsController();
        _saveLoadSystem.Initialize(_assetProvider);
        _playerData = new PlayerData(_saveLoadSystem);
        _scrollView.Initialize(0);
        _uiController.Initialize(this);

        StartCoroutine(Loading());
        _languageProvider.OnLanguageChange += ChangeLanguage;

        _settingsController.Initialize(_settingsView,_scrollView,_languageProvider,_saveLoadSystem);
    }

    private void ChangeLanguage()
    {
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_riseMonsters,StyleType.RISE));
    }

    private void FixedUpdate()
    {
        if(Screen.width > Screen.height)
        {
            _uiController.SetLandscape();
        }
        else
        {
            _uiController.SetPortrait();
        }
    }

    public void CreateRiseList()
    {
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_riseMonsters,StyleType.RISE));
    }

    public void CreateWorldList()
    {
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_worldMonsters,StyleType.WORLD));
    }

    private IEnumerator Loading()
    {
        GlobalSystems.Instance.Initialize(_assetProvider,_playerData,_languageProvider);
        GlobalSystems.Instance.SetLanguage(_language);
        yield return _assetProvider.LoadMonsterCell();

        _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath,LoadMonsters,true);
        _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath,LoadMonsters,true);

        _findSystem.Initialize(_scrollView.ContentContainer);

        yield return ShowMonsters(_riseMonsters,StyleType.RISE);

    }

    private void LoadMonsters(Monsters obj)
    {
        if (_riseMonsters == null)
        {
            _riseMonsters = obj;
        }
        else
        {
            _worldMonsters = obj;
        }

    }

    private IEnumerator ShowMonsters(Monsters monsters,StyleType style)
    {
        _tierList.CreateLists(monsters,style);

        CreateMonsters(_tierList.GetLowRankList());
        CreateMonsters(_tierList.GetMasterRankList());
        CreateMonsters(_tierList.GetTemperedlist());

        yield return new WaitForSeconds(2);
        _findSystem.SetList();
    }

    private void CreateMonsters(List<MonsterModel> monsters)
    {
        foreach (var data in monsters)
        {
            var monsterCell = Instantiate(_assetProvider.GetMonsterCell(), _scrollView.ContentContainer, false);
            monsterCell.Initialize(data);
        }
    }
}