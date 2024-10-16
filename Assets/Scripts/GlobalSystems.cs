using System;
using Data;
using Systems;
using UnityEngine;


public class GlobalSystems
{
    private static GlobalSystems _instance;
    private static LanguageProvider _languageProvider;
    private static AssetProvider _assetProvider;
    private static PlayerData _playerData;

    private GlobalSystems(){}

    public static GlobalSystems Instance
    {
        get
        {
            _instance = new GlobalSystems();
            return _instance;
        }
    }

    public void Initialize(AssetProvider assetProvider,PlayerData playerData,LanguageProvider languageProvider)
    {
        _languageProvider = languageProvider;
        _assetProvider = assetProvider;
        _playerData = playerData;
    }

    public Sprite GetSprite(string imageName)
    {
        return _assetProvider.GetSprite(imageName);
    }

    public bool GetDefeated(MonsterModel model)
    {
        return _playerData.GetDefeated(model);
    }

    public string GetName(string dataName)
    {
        return _languageProvider.GetName(dataName);
    }

    public Sprite GetSprite(RankType imageName, StyleType dataStyle)
    {
        return _assetProvider.GetRankSprite(imageName, dataStyle);
    }

    public Sprite GetSprite(WeaknessType weaknessType, StyleType style)
    {
        return _assetProvider.GetWeaknessSprite(weaknessType, style);
    }

    public string GetMonsterTypeName(MonsterType dataType)
    {
        return _languageProvider.GetTypeName(dataType);
    }

    public void SetLanguage(string language)
    {
        _languageProvider.SetLanguage(language);
    }

    public void SetDefeatedState(string modelName, RankType rank, StyleType style, bool isDefeated)
    {
        _playerData.SetDefeated(modelName, rank, style,isDefeated);
    }

    public string GetStyle(StyleType modelStyle)
    {
        if (modelStyle == StyleType.RISE)
        {
            return "RISE";
        }

        if (modelStyle == StyleType.WORLD)
        {
            return "WORLD";
        }

        return " ";
    }
}