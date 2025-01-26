using System;
using Data;
using Systems;
using UnityEngine;


public class GlobalSystems
{
    public event Action OnChangeStyle;

    private GlobalSystems _instance;
    private LanguageProvider _languageProvider;
    private AssetProvider _assetProvider;
    private PlayerData _playerData;
    private ProgressSeeker _progressSeeker;
    private KillList _killList;

    public void Initialize(AssetProvider assetProvider,PlayerData playerData,LanguageProvider languageProvider,ProgressSeeker progressSeeker,KillList killList)
    {
        _progressSeeker = progressSeeker;
        _languageProvider = languageProvider;
        _assetProvider = assetProvider;
        _playerData = playerData;
        _killList = killList;
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

    public void SetDefeatedState(MonsterModel model, bool isDefeated)
    {
        _playerData.SetDefeated(model,isDefeated);
        _progressSeeker.UpdateSlider();
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

    public void ChangeStyle()
    {
        OnChangeStyle?.Invoke();
    }

    public void AddToKillList(MonsterCell monsterCell)
    {
        _killList.TryAddToList(monsterCell.Model);
    }
}