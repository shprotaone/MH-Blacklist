using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FindSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField _horizontalInputField;
    [SerializeField] private TMP_InputField _verticalInputField;
    [SerializeField] private Transform _content;

    private GlobalSystems _globalSystems;
    private List<MonsterCell> _cells = new();

    public void Initialize(GlobalSystems globalSystems, Transform content)
    {
        _globalSystems = globalSystems;
        _globalSystems.OnChangeStyle += FindCells;
        _content = content;
        _verticalInputField.onValueChanged.AddListener(FindCells);
        _horizontalInputField.onValueChanged.AddListener(FindCells);
    }

    private void FindCells()
    {
        string val = null;
        if (_horizontalInputField.gameObject.activeInHierarchy)
        {
            val = _horizontalInputField.text;
        }
        else if (_verticalInputField.gameObject.activeInHierarchy)
        {
            val = _verticalInputField.text;
        }

        if(val == string.Empty) return;
        FindCells(val);
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