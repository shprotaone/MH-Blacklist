using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using DefaultNamespace;
using UnityEngine;

public class MonsterListChanger : MonoBehaviour
{
    [SerializeField] private Transform _listViewContainer;

    private List<MonsterScrollView> _scrolls;

    private MonsterScrollView _currentScroll;
    private UIFactory _factory;
    private UIController _uiController;
    private RankTabController _rankTabController;
    private MonsterTierList _tierList;
    private CellFactory _cellFactory;
    private FindSystem _findSystem;
    private DesignChanger _designChanger;
    private ProgressSeeker _progressSeeker;
    private CurtainSystem _curtainSystem;
    private GlobalSystems _globalSystems;

    private StyleType _currentStyle;

    private Monsters _currentMonsterList;
    private Monsters _riseMonsters;
    private Monsters _worldMonsters;
    private List<MonsterCell> _allCells = new ();

    public int CellSize { get; private set; }

    public void Initialize(UIController uiController,CellFactory cellFactory,FindSystem findSystem,
        DesignChanger designChanger,ProgressSeeker progressSeeker,CurtainSystem curtainSystem
        ,GlobalSystems globalSystems,MonsterTierList monsterTierList)
    {
        _uiController = uiController;
        _tierList = monsterTierList;
        _cellFactory = cellFactory;
        _findSystem = findSystem;
        _designChanger = designChanger;
        _progressSeeker = progressSeeker;
        _curtainSystem = curtainSystem;
        _globalSystems = globalSystems;
        _factory = uiController.UIFactory;
        _rankTabController = uiController.RankTabController;
        _scrolls = new();
        _curtainSystem.OnFullCurtain += ShowMonsters;
    }

    public void CreateRiseList()
    {
        _currentStyle = StyleType.RISE;
        _currentMonsterList = _riseMonsters;
        _uiController.SettingsView.Controller.SetScaledButton(StyleType.RISE);
        _curtainSystem.Show();
    }

    public void CreateWorldList()
    {
        _currentStyle = StyleType.WORLD;
        _currentMonsterList = _worldMonsters;

        _uiController.SettingsView.Controller.SetScaledButton(StyleType.WORLD);
        _curtainSystem.Show();
    }
    
    private async UniTask ShowMonstersRoutine(Monsters monsters,StyleType style)
    {
        ClearScrollList();
        _tierList.CreateLists(monsters,style);
        _uiController.CallSettings(false);

        await CreateTabsAndScrolls();

        _progressSeeker.Initialize(_allCells);
        _progressSeeker.UpdateSlider();
        _findSystem.SetList(_allCells);
        _globalSystems.ChangeStyle();
        _designChanger.ChangeStyle(style);

        _curtainSystem.Hide();
    }

    private async UniTask CreateTabsAndScrolls()
    {
        _rankTabController.Clear();
        _allCells.Clear();

        if (_tierList.GetLowRankList().Count > 0)
        {
            var scrollView = Instantiate(_factory.GetScrollView(), _listViewContainer, false);
            scrollView.Initialize(1);
            await _cellFactory.CreateCells(_tierList.GetLowRankList(),scrollView);
            _allCells.AddRange(scrollView.Cells);
            _rankTabController.CreateTab(RankType.LOW,_currentStyle,scrollView);
            _scrolls.Add(scrollView);
            scrollView.Hide();
        }

        if (_tierList.GetMasterRankList().Count > 0)
        {
            var scrollView = Instantiate(_factory.GetScrollView(), _listViewContainer, false);
            scrollView.Initialize(1);
            await _cellFactory.CreateCells(_tierList.GetMasterRankList(),scrollView);
            _allCells.AddRange(scrollView.Cells);
            _rankTabController.CreateTab(RankType.MASTER,_currentStyle,scrollView);
            _scrolls.Add(scrollView);
            scrollView.Hide();
        }

        if (_tierList.GetTemperedlist().Count > 0)
        {
            var scrollView = Instantiate(_factory.GetScrollView(), _listViewContainer, false);
            scrollView.Initialize(1);
            await _cellFactory.CreateCells(_tierList.GetTemperedlist(),scrollView);
            _allCells.AddRange(scrollView.Cells);
            _rankTabController.CreateTab(RankType.TEMPERED,_currentStyle,scrollView);
            _scrolls.Add(scrollView);
            scrollView.Hide();
        }

        _rankTabController.Show(RankType.LOW);
    }

    private void ClearScrollList()
    {
        foreach (var scroll in _scrolls)
        {
            scroll.Clear();
        }
        _scrolls.Clear();
    }

    public void LoadMonsters()
    {
        _curtainSystem.Show();
        Debug.Log("Try Load monsters");
    }

    private async void ShowMonsters()
    {
        await ShowMonstersRoutine(_currentMonsterList, _currentStyle);
    }

    public void SetCurrentMonsterList(StyleType styleType)
    {
        _currentStyle = styleType;
        if (styleType == StyleType.RISE) 
            _currentMonsterList = _riseMonsters;
        else if (styleType == StyleType.WORLD) 
            _currentMonsterList = _worldMonsters;
    }

    public void SetMonsterList(Monsters monsters, StyleType type)
    {
        if (type == StyleType.RISE)
        {
            _riseMonsters = monsters;
        }

        if (type == StyleType.WORLD)
        {
            _worldMonsters = monsters;
        }

        if (type == StyleType.WILDS)
        {
            
        }
    }

    public void DecreaseCellSize()
    {
        if (CellSize < 2)
        {
            CellSize += 1;
            foreach (var scroll in _scrolls)
            {
                scroll.DecreaseCellSize();
            }
        }

    }

    public void IncreaseCellSize()
    {
        if (CellSize > 0)
        {
            CellSize -= 1;
            foreach (var scroll in _scrolls)
            {
                scroll.IncreaseCellSize();
            }
        }
    }
}