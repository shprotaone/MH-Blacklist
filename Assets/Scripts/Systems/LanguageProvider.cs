using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using UnityEngine;

namespace Systems
{
    public class LanguageProvider
    {
        public event Action OnLanguageChange;
        private string _currentLanguage;
        private List<LanguagePair> _languageDict;
        private List<TranslateObj> _translateObjects;

        public LanguageProvider()
        {
            _languageDict = new List<LanguagePair>();
            _translateObjects = GameObject.FindObjectsOfType<TranslateObj>().ToList();  //TODO: WEAK
            CreateLangs();
        }

        public void SetLanguage(string language)
        {
            _currentLanguage = language;
            OnLanguageChange?.Invoke();
        }

        public string GetName(string dataName)
        {
            return _languageDict.First(x => x.name == dataName &&
                                     x.lang == _currentLanguage).translate;
        }

        public string GetTypeName(MonsterType dataType)
        {
            string result;

                    switch (dataType)
                    {
                        case MonsterType.AMPHIBIANS:
                            result = GetName("AMPHIBIANS");
                            break;
                        case MonsterType.BIRDWYVERNS:
                            result = GetName("BIRDWYVERNS");
                            break;
                        case MonsterType.BRUTEWYVERNS:
                            result = GetName("BRUTEWYVERNS");
                            break;
                        case MonsterType.FANGEDBEASTS:
                            result = GetName("FANGEDBEASTS");
                            break;
                        case MonsterType.FANGEDWYVERNS:
                            result = GetName("FANGEDWYVERNS");
                            break;
                        case MonsterType.FLYINGWYVERNS:
                            result = GetName("FLYINGWYVERNS");
                            break;
                        case MonsterType.LEVIATHANS:
                            result = GetName("LEVIATHANS");
                            break;
                        case MonsterType.PISCINEWYVERNS:
                            result = GetName("PISCINEWYVERNS");
                            break;
                        case MonsterType.TEMNOCERANS:
                            result = GetName("TEMNOCERANS");;
                            break;
                        case MonsterType.ELDERDRAGONS:
                            result = GetName("ELDERDRAGONS");;
                            break;
                        case MonsterType.HERMITAUR:
                            result = GetName("HERMITAUR");;
                            break;
                        case MonsterType.RELICT:
                            result = GetName("RELICT");
                            break;
                        case MonsterType.NONE:
                            result = GetName("???");;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
                    }

                    return result;
        }

        public void AddPair(string dataName, string lang, string resultName)
        {
            LanguagePair pair = new LanguagePair();
            pair.lang = lang;
            pair.name = dataName;
            pair.translate = resultName;
            _languageDict.Add(pair);
        }

        private void CreateLangs()
        {
            AddPair("AMPHIBIANS", "ENG", "Amphibians");
            AddPair("AMPHIBIANS", "RUS", "Амфибии");
            AddPair("BIRDWYVERNS", "ENG", "Bird wyverns");
            AddPair("BIRDWYVERNS", "RUS", "Пернатая виверна");
            AddPair("BRUTEWYVERNS", "ENG", "Brute wyverns");
            AddPair("BRUTEWYVERNS", "RUS", "Свирепая виверна");
            AddPair("FANGEDBEASTS", "ENG", "Fanged beasts");
            AddPair("FANGEDBEASTS", "RUS", "Клыкастый зверь");
            AddPair("FANGEDWYVERNS","RUS","Клыкастая виверна");
            AddPair("FANGEDWYVERNS","ENG","Fanged wyverns");
            AddPair("FLYINGWYVERNS", "ENG", "Flying wyverns");
            AddPair("FLYINGWYVERNS", "RUS", "Летающая виверна");
            AddPair("LEVIATHANS", "ENG", "Leviathans");
            AddPair("LEVIATHANS", "RUS", "Левиафан");
            AddPair("PISCINEWYVERNS", "ENG", "Piscine wyverns");
            AddPair("PISCINEWYVERNS", "RUS", "Ихтиовиверна");
            AddPair("TEMNOCERANS", "ENG", "Temnocerans");
            AddPair("TEMNOCERANS", "RUS", "Темноцеран");
            AddPair("ELDERDRAGONS", "ENG", "Elder dragon");
            AddPair("ELDERDRAGONS", "RUS", "Древний дракон");
            AddPair("HERMITAUR", "ENG", "Hermitaur");
            AddPair("HERMITAUR", "RUS", "Панцирный");
            AddPair("RELICT","ENG","RELICT");
            AddPair("RELICT","RUS","Реликт");
            AddPair("NONE", "ENG", "???");
            AddPair("NONE", "RUS", "???");

            foreach (var translateObject in _translateObjects)
            {
                AddPair(translateObject.Key,"RUS",translateObject.RUS);
                AddPair(translateObject.Key,"ENG",translateObject.ENG);
            }

        }

        public void ChangeLanguageText()
        {
            foreach (var translateObj in _translateObjects)
            {
                translateObj.ChangeText(_currentLanguage);
            }
        }

        public Lang GetLanguage()
        {
            if (_currentLanguage == "RUS") return Lang.RU;
            
            return Lang.ENG;
        }
    }
}