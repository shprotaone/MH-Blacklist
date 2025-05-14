using System;
using Enums;

namespace Data.JSON
{
    [Serializable]
    public class MonsterProgressData
    {
        public string name;
        public bool isDefeated;
        public RankType rank;
        public StyleType style;
    }
}