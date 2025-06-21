using System;
using System.Collections.Generic;
using Data.JSON;
using Enums;
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
            var cells = _cells.FindAll(x => x.name == monsterCell.name);
            var exist = false;

            if (cells.Count > 0)
            {
                if (cells.Exists(x => x.rank == monsterCell.rank))
                {
                    exist = true;
                }
            }

            if (!exist)
            {
                _cells.Add(monsterCell);
                Debug.Log("Monster on list: " + _cells.Count);
                OnMonsterCountChange?.Invoke();
                return true;
            }
            return false;
        }

        public void ResetList()
        {
            _cells.Clear();
        }
    }
}