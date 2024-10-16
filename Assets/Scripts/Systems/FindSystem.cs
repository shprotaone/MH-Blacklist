using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FindSystem : MonoBehaviour
{
   [SerializeField] private TMP_InputField _horizontalInputField;
   [SerializeField] private TMP_InputField _verticalInputField;
   [SerializeField] private Transform _content;

   private List<MonsterCell> _cells;

   public void Initialize(Transform content)
   {
      _content = content;
      _verticalInputField.onValueChanged.AddListener(FindCells);
      _horizontalInputField.onValueChanged.AddListener(FindCells);
   }

   public void SetList()
   {
      _cells = _content.GetComponentsInChildren<MonsterCell>().ToList();
   }

   private void FindCells(string val)
   {
      foreach (var cell in _cells)
      {
         if (cell.Name.Contains(val,StringComparison.OrdinalIgnoreCase))
         {
            cell.gameObject.SetActive(true);
         }
         else
         {
            cell.gameObject.SetActive(false);
         }
      }
   }
}
