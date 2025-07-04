using System;
using System.Collections.Generic;
using Data.JSON;
using UnityEngine;

namespace Data
{
    public class KillList
    {
        public event Action OnMonsterCountChange;
        private List<MonsterModel> _cells;
        public List<MonsterModel> CellList => _cells;
        
        public KillList()
        {
            _cells = new List<MonsterModel>();
        }

        public bool TryAddToList(MonsterModel monsterCell)
        {
            if (!_cells.Contains(monsterCell))
            {
                _cells.Add(monsterCell);
                Debug.Log("Monster on list: " + _cells.Count);
                OnMonsterCountChange?.Invoke();
                return true;
            }

            Debug.Log("Already contains");
            //OnMonsterCountChange?.Invoke();
            return false;
        }

        public void ResetList()
        {
            _cells.Clear();
        }
    }
}