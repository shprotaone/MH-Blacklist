using System;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : MonoBehaviour
{
    public async UniTask<MonsterCell> LoadMonsterCell()
    {
        var handle = await Addressables.LoadAssetAsync<GameObject>("MonsterCell");
        return handle.GetComponent<MonsterCell>();
    }

    public Sprite GetSprite(string imageName)
    {
        var operationHandle = Addressables.LoadAssetAsync<Sprite>(imageName);
        Sprite sprite = operationHandle.WaitForCompletion();

        return sprite;
    }

    public string GetText(string name)
    {
        var operationHandle = Addressables.LoadAssetAsync<TextAsset>(name);
        var result = operationHandle.WaitForCompletion();
        return result.text;
    }

    public Sprite GetRankSprite(RankType rankType, StyleType dataStyle)
    {
        string loadingName = "";

        if (rankType == RankType.LOW)
        {
            loadingName = "standart";
        }
        else if (rankType == RankType.MASTER)
        {
            loadingName = "master";
        }
        else if (rankType == RankType.TEMPERED)
        {
            loadingName = "tempered";
        }

        if (dataStyle == StyleType.WORLD)
        {
            loadingName += "World";
        }

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetWeaknessSprite(WeaknessType weaknessType, StyleType style)
    {
        string loadingName = "";
        if (weaknessType == WeaknessType.FIRE)
        {
            loadingName = "Fire";
        }
        else if (weaknessType == WeaknessType.FROST)
        {
            loadingName = "Frost";
        }
        else if (weaknessType == WeaknessType.WATER)
        {
            loadingName = "Water";
        }
        else if (weaknessType == WeaknessType.DRAGON)
        {
            loadingName = "Dragon";
        }
        else if (weaknessType == WeaknessType.THUNDER)
        {
            loadingName = "Thunder";
        }

        if (style == StyleType.WORLD)
        {
            loadingName += "World";
        }

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetBackground(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("BACKGROUNDWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("BACKGROUNDRise");
        }

        return null;
    }

    public Sprite GetSliderFrame(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("ProgressBorderWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("ProgressBorderRise");
        }

        return null;
    }

    public Sprite GetSliderFill(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("ProgressFillerWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("ProgressFillerRise");
        }

        return null;
    }

    public Sprite GetBorderUp(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("BorderUpWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("BorderUpRise");
        }

        return null;
    }

    public Sprite GetBorderDown(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("BorderDownWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("BorderDownRise");
        }

        return null;
    }

    public Sprite GetSettingsBackground(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("SettingsBackgroundWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("SettingsBackgroundRise");
        }

        return null;
    }

    public Sprite GetSettingsIcon(StyleType style)
    {
        if (style == StyleType.WORLD)
        {
            return GetSprite("OptionButtonWorld");
        }
        else if (style == StyleType.RISE)
        {
            return GetSprite("OptionButtonRise");
        }

        return null;
    }
}
