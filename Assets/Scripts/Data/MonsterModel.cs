using System;
using Data;

[Serializable]
public class MonsterModel
{
    public string name;
    public string imageName;
    public MonsterType type;
    public WeaknessType[] weaknessTypes;
    public RankType rank;
    public StyleType style;
}