using System;
using System.Collections.Generic;
using Cell;
using Enums;
using Storages;
using TMPro;
using UnityEngine;
using View.Search;

namespace Systems
{
    public class FindSystem : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _verticalInputField;
        [SerializeField] private ChangeSearchTypeButton changeSearchTypeOfFind;

        private GlobalSystems _globalSystems;
        private List<MonsterCell> _cells = new();

        public void Initialize(GlobalSystems globalSystems)
        {
            _globalSystems = globalSystems;
            _globalSystems.OnChangeStyle += FindCells;
            _verticalInputField.onValueChanged.AddListener(FindCells);
            changeSearchTypeOfFind.Initialize();
        }

        private void FindCells()
        {
            var input = _verticalInputField.text;

            if(input == string.Empty) return;
            FindCells(input);
        }

        public void SetList(List<MonsterCell> cells)
        {
            _cells.Clear();
            _cells.AddRange(cells);
        }

        private void FindCells(string val)
        {
            if (changeSearchTypeOfFind.FindType == FindType.MONSTER)
            {
                FindMonsterByName(val);
            }
            else if (changeSearchTypeOfFind.FindType == FindType.MATERIAL)
            {
                FindMonsterByMaterial(val);
            }
            else if (changeSearchTypeOfFind.FindType == FindType.MONSTER_TYPE)
            {
                FindMonsterByType(val);
            }
        
        }

        private void FindMonsterByType(string val)
        {
            foreach (var cell in _cells)
            {
                if (cell.Type.Contains(val,StringComparison.OrdinalIgnoreCase))
                {
                    cell.gameObject.SetActive(true);
                }
                else
                {
                    cell.gameObject.SetActive(false);
                }
            }
        }

        private void FindMonsterByName(string val)
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

        private void FindMonsterByMaterial(string val)
        {
            var dictNames = new List<string>();

            if (val == String.Empty)
            {
                foreach (var cell in _cells)
                {
                    cell.gameObject.SetActive(true);
                }

                Debug.Log("Empty find");
                return;
            }

            var resourceList = new List<MonsterResourceList>();

            if (_globalSystems.CurrentStyle == StyleType.RISE)
            {
                resourceList = _globalSystems.MosterResourcesParser.RiseList;
            }
            else if (_globalSystems.CurrentStyle == StyleType.WORLD)
            {
                resourceList = _globalSystems.MosterResourcesParser.WorldList;
            }

            foreach (var resourceName in resourceList)
            {
                foreach (var nameResource in resourceName.Resources)
                {
                    if (nameResource.Contains(val,StringComparison.OrdinalIgnoreCase))
                    {
                        dictNames.Add(resourceName.Key);
                        break;
                    }
                }
            }

            var cellListToOpen = new List<MonsterCell>();

            foreach (var dictName in dictNames)
            {
                foreach (var cell in _cells)
                {
                    if (cell.Model.name.Contains(dictName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!cellListToOpen.Contains(cell)) cellListToOpen.Add(cell);
                    }

                    cell.gameObject.SetActive(false);
                }
            }

            foreach (MonsterCell cell in cellListToOpen)
            {
                cell.gameObject.SetActive(true);
            }

            Debug.Log("Found " + dictNames.Count);
        }

        private void OnDestroy()
        {
            _globalSystems.OnChangeStyle -= FindCells;
        }
    }
}