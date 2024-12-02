using System;
using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : MonoBehaviour
{

    private MonsterCell _monsterCellPrefab;

    public MonsterCell GetMonsterCell()
    {
        return _monsterCellPrefab;
    }

    public IEnumerator LoadMonsterCell()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("MonsterCell");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _monsterCellPrefab = handle.Result.GetComponent<MonsterCell>();
        }
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
            return GetSprite("");
        }
        throw new NotImplementedException();
    }

    public Sprite GetSliderFrame(StyleType style)
    {
        throw new NotImplementedException();
    }

    public Sprite GetSliderFill(StyleType style)
    {
        throw new NotImplementedException();
    }

    public Sprite GetBorderUp(StyleType style)
    {
        throw new NotImplementedException();
    }

    public Sprite GetBorderDown(StyleType style)
    {
        throw new NotImplementedException();
    }
}
