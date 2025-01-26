using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FindSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField _verticalInputField;
    [SerializeField] private Transform _content;

    private GlobalSystems _globalSystems;
    private List<MonsterCell> _cells = new();

    public void Initialize(GlobalSystems globalSystems, UIController uiController)
    {
        _globalSystems = globalSystems;
        _globalSystems.OnChangeStyle += FindCells;
        _content = uiController.MonsterScrollView.ContentContainer;
        _verticalInputField.onValueChanged.AddListener(FindCells);
    }

    private void FindCells()
    {
        var input = _verticalInputField.text;

        if(input == string.Empty) return;
        FindCells(input);
    }

    public void SetList()
    {
        _cells.Clear();
        _cells = _content.GetComponentsInChildren<MonsterCell>().ToList();
    }

    private void FindCells(string val)
    {
        foreach (var cell in _cells)
        {
            if (cell.Name.Contains(val, StringComparison.OrdinalIgnoreCase))
            {
                cell.gameObject.SetActive(true);
            }
            else
            {
                cell.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        _globalSystems.OnChangeStyle -= FindCells;
    }
}