using System;
using System.Collections.Generic;
using Data;
using Systems;
using UnityEngine;

namespace DefaultNamespace
{
    public class MonsterTierList
    {
        private LanguageProvider _languageProvider;

        private List<MonsterModel> _lowRankList = new();
        private List<MonsterModel> _highRankList = new();
        private List<MonsterModel> _masterRankList = new();
        private List<MonsterModel> _temperedRankList = new();

        public MonsterTierList(LanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public void CreateLists(Monsters monsters,StyleType style)
        {
            _highRankList.Clear();
            _masterRankList.Clear();
            _temperedRankList.Clear();

            foreach (var monsterData in monsters.data)
            {
                _languageProvider.AddPair(monsterData.name,"RUS",monsterData.RUtranslate);
                _languageProvider.AddPair(monsterData.name,"ENG",monsterData.name);

                if (monsterData.low)
                {
                    MonsterModel model = new MonsterModel();

                    model.name = monsterData.name;
                    model.weaknessTypes = CreateWeakness(monsterData);
                    model.weaknessStatusTypes = CreateWeaknessStatusType(monsterData);
                    model.imageName = monsterData.name;
                    model.style = style;
                    model.rank = RankType.LOW;
                    model.type = GetMonsterType(monsterData.monsterType);
                    _highRankList.Add(model);
                }

                if (monsterData.mr)
                {
                    MonsterModel model = new MonsterModel();

                    model.name = monsterData.name;
                    model.weaknessTypes = CreateWeakness(monsterData);
                    model.weaknessStatusTypes = CreateWeaknessStatusType(monsterData);
                    model.imageName = monsterData.name;
                    model.style = style;
                    model.rank = RankType.MASTER;
                    model.type = GetMonsterType(monsterData.monsterType);
                    _masterRankList.Add(model);
                }

                if (monsterData.tempered)
                {
                    MonsterModel model = new MonsterModel();

                    model.name = monsterData.name;
                    model.weaknessTypes = CreateWeakness(monsterData);
                    model.weaknessStatusTypes = CreateWeaknessStatusType(monsterData);
                    model.imageName = monsterData.name;
                    model.style = style;
                    model.rank = RankType.TEMPERED;
                    model.type = GetMonsterType(monsterData.monsterType);
                    _temperedRankList.Add(model);
                }
            }
        }

        private Dictionary<WeaknessStatusType,int> CreateWeaknessStatusType(MonsterData monsterData)
        {
            Dictionary<WeaknessStatusType,int> weaknessTypes = new();
            var weaknessList = monsterData.weaknessStatus.Split(';',StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> weaknessDict = new Dictionary<string, int>();

            try
            {
                foreach (var weakness in weaknessList)
                {
                    var result = weakness.Split(",");
                    weaknessDict.Add(result[0],Convert.ToInt16(result[1]));
                }
            }
            catch (Exception e)
            {
                Debug.Log(monsterData.name);
            }
            
            foreach (var weakness in weaknessDict)
            {
                switch (weakness.Key)
                {
                        case "poison":
                            weaknessTypes.Add(WeaknessStatusType.POISON,weakness.Value);
                            break;
                        case "sleep":
                            weaknessTypes.Add(WeaknessStatusType.SLEEP,weakness.Value);
                            break;
                        case "para":
                            weaknessTypes.Add(WeaknessStatusType.PARA,weakness.Value);
                            break;
                        case "blast":
                            weaknessTypes.Add(WeaknessStatusType.BLAST,weakness.Value);
                            break;
                        case "stun":
                            weaknessTypes.Add(WeaknessStatusType.STUN,weakness.Value);
                            break;
                }
            }

            return weaknessTypes;
            
        }

        private MonsterType GetMonsterType(string monsterType)
        {
                if (monsterType == "Amphibians") return MonsterType.AMPHIBIANS;
                if (monsterType == "Bird wyverns") return MonsterType.BIRDWYVERNS;
                if (monsterType == "Brute wyverns") return MonsterType.BRUTEWYVERNS;
                if (monsterType == "Fanged beasts") return MonsterType.FANGEDBEASTS;
                if (monsterType == "Flying wyverns") return MonsterType.FLYINGWYVERNS;
                if (monsterType == "Leviathans") return MonsterType.LEVIATHANS;
                if (monsterType == "Piscine wyverns") return MonsterType.PISCINEWYVERNS;
                if (monsterType == "Temnocerans") return MonsterType.TEMNOCERANS;
                if (monsterType == "Elder dragon") return MonsterType.ELDERDRAGONS;
                if (monsterType == "Hermitaur") return MonsterType.HERMITAUR;
                if (monsterType == "Fanged wyverns") return MonsterType.FANGEDWYVERNS;
                if (monsterType == "Relict") return MonsterType.RELICT;

                return MonsterType.NONE;
        }

        private WeaknessType[] CreateWeakness(MonsterData monsterData)
        {
            List<WeaknessType> weaknessTypes = new();
            var weaknessList = monsterData.weakness.Split(',');

            foreach (var weakness in weaknessList)
            {
                if(weakness == "water") weaknessTypes.Add(WeaknessType.WATER);
                else if(weakness == "thunder") weaknessTypes.Add(WeaknessType.THUNDER);
                else if(weakness == "frost") weaknessTypes.Add(WeaknessType.FROST);
                else if(weakness == "fire") weaknessTypes.Add(WeaknessType.FIRE);
                else if(weakness == "dragon")weaknessTypes.Add(WeaknessType.DRAGON);
            }

            return weaknessTypes.ToArray();
        }

        public List<MonsterModel> GetLowRankList()
        {
            return _highRankList;
        }

        public List<MonsterModel> GetMasterRankList()
        {
            return _masterRankList;
        }

        public List<MonsterModel> GetTemperedlist()
        {
            return _temperedRankList;
        }
    }
}