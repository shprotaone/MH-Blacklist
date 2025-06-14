using System.Collections.Generic;
using Cell;
using Cysharp.Threading.Tasks;
using Data;
using Data.JSON;
using Enums;
using Storages;
using Systems.Factory;
using Systems.UI;
using View;

namespace Systems
{
    public class MonsterListChanger
    {
        private List<MonsterScrollView> _scrolls;

        private GlobalSystems _globalSystems;
        
        private MonsterScrollView _currentScroll;
        private UIFactory _factory;
        private UIController _uiController;
        private RankTabController _rankTabController;
        private MonsterTierListStorage _tierListStorage;
        private CellFactory _cellFactory;
        private FindSystem _findSystem;
        private DesignChanger _designChanger;
        private ProgressSeekerView _progressSeekerView;
        private CurtainSystem _curtainSystem;
        private SaveLoadSystem _saveLoadSystem;

        private Monsters _currentMonsterList;
        private Monsters _riseMonsters;
        private Monsters _worldMonsters;
        private Monsters _wildsMonsters;
        private List<MonsterCell> _allCells = new ();

        public int CellSize { get; private set; }

        public void Initialize(UIController uiController,CellFactory cellFactory,FindSystem findSystem,
            DesignChanger designChanger,GlobalSystems globalSystems)
        {
            _uiController = uiController;
            _tierListStorage = globalSystems.TierListStorage;
            _cellFactory = cellFactory;
            _findSystem = findSystem;
            _designChanger = designChanger;
            _curtainSystem = globalSystems.CurtainSystem;
            _globalSystems = globalSystems;
            _factory = uiController.UIFactory;
            _rankTabController = uiController.RankTabController;
            _saveLoadSystem = _globalSystems.SaveLoadSystem;
            _progressSeekerView = _uiController.ProgressSeeker;
            _scrolls = new();
            _curtainSystem.OnFullCurtain += ShowMonsters;
        }

        public void CreateRiseList()
        {
            _globalSystems.CurrentStyle = StyleType.RISE;
            _currentMonsterList = _riseMonsters;
            _uiController.SettingsView.Controller.SetScaledButton(StyleType.RISE);
            _curtainSystem.Show();
            
            _globalSystems.PlayerDataParser.SaveAppData(_globalSystems.CurrentStyle);
        }

        public void CreateWorldList()
        {
            _globalSystems.CurrentStyle = StyleType.WORLD;
            _currentMonsterList = _worldMonsters;

            _uiController.SettingsView.Controller.SetScaledButton(StyleType.WORLD);
            _curtainSystem.Show();
            _globalSystems.PlayerDataParser.SaveAppData(_globalSystems.CurrentStyle);
        }

        public void CreateWildsList()
        {
            _globalSystems.CurrentStyle = StyleType.WILDS;
            _currentMonsterList = _wildsMonsters;
            
            _uiController.SettingsView.Controller.SetScaledButton(StyleType.WILDS);
            _curtainSystem.Show();
            _globalSystems.PlayerDataParser.SaveAppData(_globalSystems.CurrentStyle);
        }
    
        private async UniTask ShowMonstersRoutine(Monsters monsters,StyleType style)
        {
            ClearScrollList();
            _uiController.QuickListMonsters.ResetList();
            
            _tierListStorage.CreateLists(monsters,style);
            _uiController.CallSettings(false);
            
            await CreateTabsAndScrolls();

            _progressSeekerView.Initialize(_allCells);
            _progressSeekerView.UpdateSlider();
            _findSystem.SetList(_allCells);
            _designChanger.ChangeStyle(style);

            _curtainSystem.Hide();
        }

        private async UniTask CreateTabsAndScrolls()
        {
            _rankTabController.Clear();
            _allCells.Clear();

            if (_tierListStorage.GetLowRankList().Count > 0)
            {
                var scrollView = _factory.GetScrollView();
                scrollView.Initialize(1,_globalSystems.InputSystemHandler);
                await _cellFactory.CreateCells(_tierListStorage.GetLowRankList(),scrollView);
                _allCells.AddRange(scrollView.Cells);
                _rankTabController.CreateTab(RankType.LOW,scrollView);
                _scrolls.Add(scrollView);
                scrollView.Hide();
            }

            if (_tierListStorage.GetHighRankList().Count > 0)
            {
                var scrollView = _factory.GetScrollView();
                scrollView.Initialize(1,_globalSystems.InputSystemHandler);
                await _cellFactory.CreateCells(_tierListStorage.GetHighRankList(),scrollView);
                _allCells.AddRange(scrollView.Cells);
                _rankTabController.CreateTab(RankType.HIGH,scrollView);
                _scrolls.Add(scrollView);
                scrollView.Hide();
            }

            if (_tierListStorage.GetMasterRankList().Count > 0)
            {
                var scrollView = _factory.GetScrollView();
                scrollView.Initialize(1,_globalSystems.InputSystemHandler);
                await _cellFactory.CreateCells(_tierListStorage.GetMasterRankList(),scrollView);
                _allCells.AddRange(scrollView.Cells);
                _rankTabController.CreateTab(RankType.MASTER,scrollView);
                _scrolls.Add(scrollView);
                scrollView.Hide();
            }

            if (_tierListStorage.GetTemperedlist().Count > 0)
            {
                var scrollView = _factory.GetScrollView();
                scrollView.Initialize(1,_globalSystems.InputSystemHandler);
                await _cellFactory.CreateCells(_tierListStorage.GetTemperedlist(),scrollView);
                _allCells.AddRange(scrollView.Cells);
                _rankTabController.CreateTab(RankType.TEMPERED,scrollView);
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

        public async void ShowMonsters()
        {
            await ShowMonstersRoutine(_currentMonsterList, _globalSystems.CurrentStyle);
        }

        public void SetCurrentMonsterList(StyleType styleType)
        {
            if (styleType == StyleType.RISE) 
                _currentMonsterList = _riseMonsters;
            else if (styleType == StyleType.WORLD) 
                _currentMonsterList = _worldMonsters;
            else if (styleType == StyleType.WILDS)
                _currentMonsterList = _wildsMonsters;
        }

        public void SetMonsterList()
        {
            _riseMonsters = _saveLoadSystem.Load<Monsters>(StaticData.riseFilePath, true);
            _worldMonsters = _saveLoadSystem.Load<Monsters>(StaticData.worldFilePath, true);
            _wildsMonsters = _saveLoadSystem.Load<Monsters>(StaticData.wildsFilePath, true);
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
}