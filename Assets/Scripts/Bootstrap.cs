using System;
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
    [SerializeField] private ProgressSeeker _progressSeeker;
    [SerializeField] private DesignChanger _designChanger;
    [SerializeField] private CellFactory _cellFactory;

    private GlobalSystems _globalSystems;
    private PlayerData _playerData;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private SettingsController _settingsController;
    private MonsterTierList _tierList;
    private Monsters _riseMonsters;
    private Monsters _worldMonsters;

    private Monsters _currentMonsterList;
    private StyleType _currentStyle;

    private async void Start()
    {
        _globalSystems = new GlobalSystems();
        _languageProvider = new LanguageProvider();
        _tierList = new MonsterTierList(_languageProvider);
        _saveLoadSystem = new SaveLoadSystem();
        _settingsController = new SettingsController();

        _saveLoadSystem.Initialize(_assetProvider);
        _playerData = new PlayerData(_saveLoadSystem);
        _scrollView.Initialize(0);
        _uiController.Initialize(this);
        await _cellFactory.Initialize(_globalSystems,_assetProvider,_scrollView.ContentContainer);

        StartCoroutine(Loading());
        _languageProvider.OnLanguageChange += ChangeLanguage;

        _designChanger.Initialize(_assetProvider);
        _settingsController.Initialize(_settingsView,_scrollView,_languageProvider,_saveLoadSystem);
    }

    private void ChangeLanguage()
    {
        _scrollView.Clear();
        _languageProvider.ChangeLanguageText();
        StartCoroutine(ShowMonsters(_currentMonsterList,_currentStyle));
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
        _currentStyle = StyleType.RISE;
        _currentMonsterList = _riseMonsters;
        _uiController.SetScaledButton(StyleType.RISE);
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_riseMonsters,StyleType.RISE));
    }

    public void CreateWorldList()
    {
        _currentStyle = StyleType.WORLD;
        _currentMonsterList = _worldMonsters;

        _uiController.SetScaledButton(StyleType.WORLD);
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_worldMonsters,StyleType.WORLD));
    }

    private IEnumerator Loading()
    {
        _globalSystems.Initialize(_assetProvider,_playerData,_languageProvider,_progressSeeker);
        _globalSystems.SetLanguage(_language);

        _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath,LoadMonsters,true);
        _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath,LoadMonsters,true);

        _findSystem.Initialize(_globalSystems,_scrollView.ContentContainer);

        _currentMonsterList = _riseMonsters;
        yield return ShowMonsters(_currentMonsterList,_currentStyle);
        _uiController.SetScaledButton(StyleType.RISE);
        
    }

    private void LoadMonsters(Monsters obj)
    {
        if (_riseMonsters == null)
            _riseMonsters = obj;
        else
            _worldMonsters = obj;

    }

    private IEnumerator ShowMonsters(Monsters monsters,StyleType style)
    {
        _tierList.CreateLists(monsters,style);

        _cellFactory.CreateCells(_tierList.GetLowRankList());
        _cellFactory.CreateCells(_tierList.GetMasterRankList());
        _cellFactory.CreateCells(_tierList.GetTemperedlist());

        yield return new WaitForSeconds(0.5f);
        _findSystem.SetList();
        _globalSystems.ChangeStyle();

        _designChanger.ChangeStyle(style);

        _progressSeeker.Initialize(_scrollView.ContentContainer);
        _progressSeeker.UpdateSlider();


        yield break;
    }
}