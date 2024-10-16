using System;

namespace Data
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