using System;
using System.Collections.Generic;
using Data.JSON;
using Enums;
using Systems;
using UnityEditor;
using UnityEngine;
using View.DetailPanel;

public class RankField : MonoBehaviour
{
    [SerializeField] private List<RankDetailButton> _buttons;

    private MonsterModel _model;

    public void Init(MonsterModel model, ResourcesView resourcesView)
    {
        DisableAll();
        _model = model;

        List<RankType> types = GlobalSystems.Instance.GetRanks(_model);

        for (int i = 0; i < types.Count; i++)
        {
            _buttons[i].Init(types[i], _model , resourcesView);
        }
    }

    private void DisableAll()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}