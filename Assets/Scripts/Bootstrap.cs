using System.Collections;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using DefaultNamespace.View;
using Systems;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private string riseFilePath = "RiseMonstersData";
    private string worldFilePath = "WorldMonstersData";

    [SerializeField] private string _language;
    [SerializeField] private AssetProvider _assetProvider;
    [SerializeField] private MonsterScrollView _scrollView;
    [SerializeField] private MainPanelView _mainPanelViewVertical;
    [SerializeField] private MainPanelView _mainPanelViewHorizontal;
    [SerializeField] private FindSystem _findSystem;
    [SerializeField] private UIController _uiController;

    private PlayerData _playerData;
    private LanguageProvider _languageProvider;
    private SaveLoadSystem _saveLoadSystem;
    private MonsterTierList _tierList;
    private Monsters _riseMonsters;
    private Monsters _worldMonsters;

    void Start()
    {
        _languageProvider = new LanguageProvider();
        _tierList = new MonsterTierList(_languageProvider);
        _saveLoadSystem = new SaveLoadSystem();
        _saveLoadSystem.Initialize(_assetProvider);
        _playerData = new PlayerData(_saveLoadSystem);

        StartCoroutine(Loading());

        _mainPanelViewVertical.WorldButton.onClick.AddListener(CreateWorldList);
        _mainPanelViewVertical.RiseButton.onClick.AddListener(CreateRiseList);
        _mainPanelViewHorizontal.WorldButton.onClick.AddListener(CreateWorldList);
        _mainPanelViewHorizontal.RiseButton.onClick.AddListener(CreateRiseList);
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

    private void CreateRiseList()
    {
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_riseMonsters,StyleType.RISE));
    }

    private void CreateWorldList()
    {
        _scrollView.Clear();
        StartCoroutine(ShowMonsters(_worldMonsters,StyleType.WORLD));
    }

    private IEnumerator Loading()
    {
        GlobalSystems.Instance.Initialize(_assetProvider,_playerData,_languageProvider);
        GlobalSystems.Instance.SetLanguage(_language);
        yield return _assetProvider.LoadMonsterCell();

        _saveLoadSystem.Load<Monsters>(riseFilePath,LoadMonsters,true);
        _saveLoadSystem.Load<Monsters>(worldFilePath,LoadMonsters,true);

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