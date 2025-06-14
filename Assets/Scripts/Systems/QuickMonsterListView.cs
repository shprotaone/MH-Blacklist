using System.Collections.Generic;
using Cell;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class QuickMonsterListView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Button _openListButton;
        [SerializeField] private Button _closeButton;

        private List<QuickCell> _cells;
        private KillList _killList;
        private QuickCell _quickCell;

        public void Initialize(AssetProvider assetProvider)
        {
            _killList = GlobalSystems.Instance.KillList;
            _quickCell = assetProvider.GetQuickCell();
            _cells = new List<QuickCell>();

            _killList.OnMonsterCountChange += CheckShowButton;
            _openListButton.onClick.AddListener(OpenList);
            _closeButton.onClick.AddListener(CloseList);
            CheckShowButton();
        }

        private void CloseList()
        {
            Clear();
            
            CheckShowButton();
            gameObject.SetActive(false);
        }

        private void OpenList()
        {
            _openListButton.gameObject.SetActive(false);
            gameObject.SetActive(true);
            CreateCells();
        }

        public void CreateCells()
        {
            foreach (var model in _killList.CellList)
            {
                var cell = Instantiate(_quickCell, _container, false);
                cell.Initialize(this,model);
                _cells.Add(cell);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _cells.Count; i++) 
                Destroy(_cells[i].gameObject);

            _cells.Clear();
        }

        private void ShowButton(bool flag)
        {
            _openListButton.gameObject.SetActive(flag);
        }

        private void CheckShowButton()
        {
            if (_killList.CellList.Count > 0)
            {
                if(!_openListButton.isActiveAndEnabled)
                    ShowButton(true);
            }
            else
            {
                ShowButton(false);
            }
        }

        public void DeleteMonster(QuickCell cell)
        {
            _killList.CellList.Remove(cell.MonsterModel);
            Disable(cell);
            
            if(_cells.Count == 0) CloseList();
        }

        private void Disable(QuickCell cell)
        {
            _cells.Remove(cell);
            Destroy(cell.gameObject);
        }

        public void ResetList()
        {
            _killList.ResetList();
            CheckShowButton();
        }
    }
}
