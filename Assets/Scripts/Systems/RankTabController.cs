using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class RankTabController : MonoBehaviour
{
    [SerializeField] private Transform _tabButtonsContainer;

    private List<MonsterListButton> _buttons = new();
    private MonsterListButton _currentButton;
    private MonsterListButton _tabButtonPrefab;
    private AssetProvider _assetProvider;

    public void Initialize(AssetProvider assetProvider)
    {
        _tabButtonPrefab = assetProvider.GetPrefab("ButtonGroup").GetComponent<MonsterListButton>();
        _assetProvider = assetProvider;
    }

    public void CreateTab(RankType rank,StyleType styleType, MonsterScrollView scrollView)
    {
        var button = Instantiate(_tabButtonPrefab, _tabButtonsContainer, false);
        button.Initialize(rank,_assetProvider.GetRankSprite(rank,styleType),scrollView,this);
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
