using System;
using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using Enums;
using NUnit.Framework;
using Systems;
using UnityEngine;

namespace Storages
{
    public class MonsterTierListStorage
    {
        private LanguageProvider _languageProvider;

        private List<MonsterModel> _lowRankList = new();
        private List<MonsterModel> _highRankList = new();
        private List<MonsterModel> _masterRankList = new();
        private List<MonsterModel> _temperedRankList = new();

        public MonsterTierListStorage(LanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public void CreateLists(Monsters monsters,StyleType style)
        {
            _lowRankList.Clear();
            _highRankList.Clear();
            _masterRankList.Clear();
            _temperedRankList.Clear();

            foreach (var monsterData in monsters.data)
            {
                _languageProvider.AddPair(monsterData.name,"RUS",monsterData.RUtranslate);
                _languageProvider.AddPair(monsterData.name,"ENG",monsterData.name);

                if (monsterData.low)
                {
                    var model = ParseToModel(style, monsterData,RankType.LOW);
                    _lowRankList.Add(model);
                }
                
                if (monsterData.high)
                {
                    var model = ParseToModel(style, monsterData,RankType.HIGH);
                    _highRankList.Add(model);
                }

                if (monsterData.mr)
                {
                    var model = ParseToModel(style, monsterData,RankType.MASTER);
                    _masterRankList.Add(model);
                }

                if (monsterData.tempered)
                {
                    var model = ParseToModel(style, monsterData,RankType.TEMPERED);
                    _temperedRankList.Add(model);
                }
            }
        }

        private MonsterModel ParseToModel(StyleType style, MonsterData monsterData,RankType rank)
        {
            MonsterModel model = new MonsterModel();

            model.name = monsterData.name;
            model.weaknessTypes = ParseWeakness(monsterData);
            model.weaknessStatusTypes = ParseWeaknessStatusType(monsterData);
            model.imageName = monsterData.name;
            model.style = style;
            model.rank = rank;
            model.type = ParseMonsterType(monsterData.monsterType);
            model.attackTypes = ParseAttackTypes(monsterData);
            model.locations = ParseLocations(monsterData);
            return model;
        }

        private LocationType[] ParseLocations(MonsterData monsterData)
        {
            List<LocationType> locationTypes = new();
            var locationList = monsterData.location.Split(',');

            foreach (var locName in locationList)
            {
                switch (locName)
                {
                    case "af":
                        locationTypes.Add(LocationType.ANCIENT_FOREST);
                        break;
                    case "ww":
                        locationTypes.Add(LocationType.WILDSPIRE_WASTE);
                        break;
                    case "ch":
                        locationTypes.Add(LocationType.CORAL_HIGHLANDS);
                        break;
                    case "rv":
                        locationTypes.Add(LocationType.ROTTEN_VALE);
                        break;
                    case "hr":
                        locationTypes.Add(LocationType.HOARFROST_REACH);
                        break;
                    case "gl":
                        locationTypes.Add(LocationType.THE_GUILDING_LANDS);
                        break;
                    case "er":
                        locationTypes.Add(LocationType.ELDERS_RECESS);
                        break;
                    case "sv":
                        locationTypes.Add(LocationType.SECLUDED_VALLEY);
                        break;
                    case "cs":
                        locationTypes.Add(LocationType.CASTLE_SCHRADE);
                        break;
                    case "ced":
                        locationTypes.Add(LocationType.CAVERNS_OF_EL_DORADO);
                        break;
                    case "oi":
                        locationTypes.Add(LocationType.ORIGIN_ISLE);
                        break;
                    case "sr":
                        locationTypes.Add(LocationType.SHRINE_RUINS);
                        break;
                    case "fi":
                        locationTypes.Add(LocationType.FROST_ISLANDS);
                        break;
                    case "sp":
                        locationTypes.Add(LocationType.SANDY_PLAINS);
                        break;
                    case "ff":
                        locationTypes.Add(LocationType.FLOODED_FOREST);
                        break;
                    case "lc":
                        locationTypes.Add(LocationType.LAVA_CAVERNS);
                        break;
                    case "c":
                        locationTypes.Add(LocationType.THE_CITADEL);
                        break;
                    case "j":
                        locationTypes.Add(LocationType.THE_JUNGLE);
                        break;
                    case "rs":
                        locationTypes.Add(LocationType.RED_STRONGHOLD);
                        break;
                    case "is":
                        locationTypes.Add(LocationType.INFERNAL_SPRINGS);
                        break;
                    case "fa":
                        locationTypes.Add(LocationType.FORFLORN_ARENA);
                        break;
                    case "cp":
                        locationTypes.Add(LocationType.CORAL_PALACE);
                        break;
                    case "r":
                        locationTypes.Add(LocationType.RAMPAGE);
                        break;
                    case "s":
                        locationTypes.Add(LocationType.SPECIAL);
                        break;
                    case "wp":
                        locationTypes.Add(LocationType.WINDWARD_PLAINS);
                        break;
                    case "sf":
                        locationTypes.Add(LocationType.SCARLET_FOREST);
                        break;
                    case "ob":
                        locationTypes.Add(LocationType.OILWELL_BASIN);
                        break;
                    case "ic":
                        locationTypes.Add(LocationType.ICESHARD_CLIFFS);
                        break;
                    case "rw":
                        locationTypes.Add(LocationType.RUINS_OF_WYVERIA);
                        break;
                    default:
                        Debug.Log(locName + " not found");
                        break;
                }
            }

            return locationTypes.ToArray();
        }

        private AttackType[] ParseAttackTypes(MonsterData monsterData)
        {
            List<AttackType> attackTypes = new();
            var attackList = monsterData.attackType.Split(',');

            foreach (var attack in attackList)
            {
                if (attack == "water") attackTypes.Add(AttackType.WATER);
                else if (attack == "thunder") attackTypes.Add(AttackType.THUNDER);
                else if (attack == "frost") attackTypes.Add(AttackType.FROST);
                else if (attack == "fire") attackTypes.Add(AttackType.FIRE);
                else if (attack == "dragon") attackTypes.Add(AttackType.DRAGON);
                else if (attack == "poison") attackTypes.Add(AttackType.POISON);
                else if (attack == "sleep") attackTypes.Add(AttackType.SLEEP);
                else if (attack == "para") attackTypes.Add(AttackType.PARA);
                else if (attack == "blast") attackTypes.Add(AttackType.BLAST);
                else if (attack == "stun") attackTypes.Add(AttackType.STUN);
                else Debug.Log("Add " + attack);

            }

            return attackTypes.ToArray();
        }

        private Dictionary<WeaknessStatusType,int> ParseWeaknessStatusType(MonsterData monsterData)
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
                Debug.Log("Add weakness status exception " + monsterData.name);
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

        private MonsterType ParseMonsterType(string monsterType)
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
                if (monsterType == "Construct") return MonsterType.CONSTRUCT;
                if (monsterType == "Cephalopods") return MonsterType.CEPHALOPODS;

                return MonsterType.NONE;
        }

        private WeaknessType[] ParseWeakness(MonsterData monsterData)
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
            return _lowRankList;
        }

        public List<MonsterModel> GetHighRankList()
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