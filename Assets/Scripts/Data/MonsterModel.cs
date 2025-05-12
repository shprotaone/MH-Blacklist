using System;
using System.Collections.Generic;
using Data;

[Serializable]
public class MonsterModel
{
    public string name;
    public string imageName;
    public MonsterType type;
    public WeaknessType[] weaknessTypes;
    public Dictionary<WeaknessStatusType,int> weaknessStatusTypes;
    public RankType rank;
    public StyleType style;
}