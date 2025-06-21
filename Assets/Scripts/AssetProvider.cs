using System;
using Cell;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : MonoBehaviour
{
    public async UniTask<MonsterCell> LoadMonsterCell()
    {
        var handle = await Addressables.InstantiateAsync("MonsterCell");
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

    public Sprite GetRankSprite(RankType rankType)
    {
        string loadingName = "";

        if (rankType == RankType.LOW)
        {
            loadingName = "low";
        }
        else if (rankType == RankType.HIGH)
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

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetWeaknessSprite(WeaknessType weaknessType)
    {
        string loadingName = "";
        if (weaknessType == WeaknessType.FIRE)
        {
            loadingName = "fire";
        }
        else if (weaknessType == WeaknessType.FROST)
        {
            loadingName = "frost";
        }
        else if (weaknessType == WeaknessType.WATER)
        {
            loadingName = "water";
        }
        else if (weaknessType == WeaknessType.DRAGON)
        {
            loadingName = "dragon";
        }
        else if (weaknessType == WeaknessType.THUNDER)
        {
            loadingName = "thunder";
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
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
        else if (style == StyleType.RISE || style == StyleType.WILDS)
        {
            return GetSprite("OptionButtonRise");
        }

        return null;
    }

    public QuickCell GetQuickCell()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("QuickCell");
        return handle.WaitForCompletion().GetComponent<QuickCell>();
    }

    public async UniTask<GameObject> GetPrefabAsync(string name)
    {
        return await Addressables.LoadAssetAsync<GameObject>(name);
    }

    public T GetPrefab<T>(string name) where T : MonoBehaviour
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(name);
        return handle.WaitForCompletion().GetComponent<T>();
    }

    public GameObject GetPrefab(string name)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(name);
        return handle.WaitForCompletion();
    }

    public Sprite GetWeaknessSprite(WeaknessStatusType weaknessType, StyleType style)
    {
        string loadingName = "";
        if (weaknessType == WeaknessStatusType.PARA)
        {
            loadingName = "para";
        }
        else if (weaknessType == WeaknessStatusType.STUN)
        {
            loadingName = "stun";
        }
        else if (weaknessType == WeaknessStatusType.BLAST)
        {
            loadingName = "blast";
        }
        else if (weaknessType == WeaknessStatusType.SLEEP)
        {
            loadingName = "sleep";
        }
        else if (weaknessType == WeaknessStatusType.POISON)
        {
            loadingName = "poison";
        }

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetAttackSprite(AttackType attackType)
    {
        string loadingName = "";
        if (attackType == AttackType.FIRE)
        {
            loadingName = "fire";
        }
        else if (attackType == AttackType.FROST)
        {
            loadingName = "frost";
        }
        else if (attackType == AttackType.WATER)
        {
            loadingName = "water";
        }
        else if (attackType == AttackType.DRAGON)
        {
            loadingName = "dragon";
        }
        else if (attackType == AttackType.THUNDER)
        {
            loadingName = "thunder";
        }
        else if (attackType == AttackType.PARA)
        {
            loadingName = "para";
        }
        else if (attackType == AttackType.STUN)
        {
            loadingName = "stun";
        }
        else if (attackType == AttackType.BLAST)
        {
            loadingName = "blast";
        }
        else if (attackType == AttackType.SLEEP)
        {
            loadingName = "sleep";
        }
        else if (attackType == AttackType.POISON)
        {
            loadingName = "poison";
        }
        else
        {
            loadingName = "unknown";
        }

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetSprite(LocationType location)
    {
        string loadingName = "";
        if (location == LocationType.ANCIENT_FOREST)
        {
            loadingName = "AncientForest";
        }
        else if (location == LocationType.ELDERS_RECESS)
        {
            loadingName = "EldersRecess";
        }
        else if (location == LocationType.HOARFROST_REACH)
        {
            loadingName = "HoarfrostReach";
        }
        else if (location == LocationType.THE_GUILDING_LANDS)
        {
            loadingName = "GuildingLands";
        }
        else if (location == LocationType.CORAL_HIGHLANDS)
        {
            loadingName = "CoralHighlands";
        }
        else if (location == LocationType.WILDSPIRE_WASTE)
        {
            loadingName = "WildspireWaste";
        }
        else if (location == LocationType.ROTTEN_VALE)
        {
            loadingName = "RottenVale";
        }
        else
        {
            loadingName = "unknown";
        }

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;

    }

    public Sprite GetDefeatSprite(StyleType currentStyle, bool isDefeated)
    {
        string loadingName = "";
        if (isDefeated)
        {
            if (currentStyle == StyleType.WORLD) loadingName += "worldMark";
            if (currentStyle == StyleType.RISE) loadingName += "riseMark";
            if (currentStyle == StyleType.WILDS) loadingName += "wildsMark";
        }
        else
        {
            if (currentStyle == StyleType.WORLD) loadingName += "worldMarkUnselect";
            if (currentStyle == StyleType.RISE) loadingName += "riseMarkUnselect";
            if (currentStyle == StyleType.WILDS) loadingName += "wildsMarkUnselect";
        }


        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetCellBackground(StyleType currentStyle)
    {
        string loadingName = "";
        if (currentStyle == StyleType.WORLD) loadingName += "pageWorld";
        if (currentStyle == StyleType.RISE) loadingName += "pageRise";
        if (currentStyle == StyleType.WILDS) loadingName += "pageWilds";

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }

    public Sprite GetDetailBackground(StyleType currentStyle)
    {
        string loadingName = "";
        if (currentStyle == StyleType.WORLD) loadingName += "LargePageWorld";
        if (currentStyle == StyleType.RISE) loadingName += "LargePageRise";
        if (currentStyle == StyleType.WILDS) loadingName += "LargePageWilds";

        var operationHandle = Addressables.LoadAssetAsync<Sprite>(loadingName);
        Sprite result = operationHandle.WaitForCompletion();

        return result;
    }
}
