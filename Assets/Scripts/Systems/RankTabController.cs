using System.Collections.Generic;
using Enums;
using UnityEngine;
using View;

namespace Systems
{
    public class RankTabController : MonoBehaviour
    {
        [SerializeField] private Transform _tabButtonsContainer;

        private List<RankTabButton> _buttons = new();
        private RankTabButton _currentButton;
        private RankTabButton _tabButtonPrefab;
        private AssetProvider _assetProvider;

        public void Initialize(AssetProvider assetProvider)
        {
            _tabButtonPrefab = assetProvider.GetPrefab("ButtonGroup").GetComponent<RankTabButton>();
            _assetProvider = assetProvider;
        }

        public void CreateTab(RankType rank, MonsterScrollView scrollView)
        {
            var button = Instantiate(_tabButtonPrefab, _tabButtonsContainer, false);
            button.Initialize(rank,_assetProvider.GetRankSprite(rank),scrollView,this);
            _buttons.Add(button);
        }

        private void DisablePrev()
        {
            _currentButton?.Disable();
        }

        public void Clear()
        {
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons.Clear();
        }

        public void Show(RankType rank)
        {
            DisablePrev();

            foreach (var button in _buttons)
            {
                if (button.Rank == rank)
                {
                    button.Show();
                    _currentButton = button;
                }
            }
        }
    }
}
