using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class MonsterResourcesParser
{
    private const string riseResourceFileName = "SourceListRise";
    private const string worldResourceFileName = "SourceListWorldSourceListWorld";

    private List<MonsterResourceList> _riseLists = new ();
    private List<MonsterResourceList> _worldList = new ();
    //куда нибудь сохранить

    public List<MonsterResourceList> RiseList => _riseLists;
    public List<MonsterResourceList> WorldList => _worldList;
    public void Initialize(AssetProvider assetProvider)
    {
        InitRiseResources(assetProvider);
    }

    private void InitRiseResources(AssetProvider assetProvider)
    {
        var json = assetProvider.GetText(riseResourceFileName);
        var resourceListRaw = JsonUtility.FromJson<MonsterResources>(json);
        
        for (int i = 0; i < resourceListRaw.data.Length; i++)
        {
            var resource = resourceListRaw.data[i];
            MonsterResourceList resourceList = new MonsterResourceList(
                resource.name, RankType.LOW, Lang.RU,
                resource.lowRU.Split(";", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);

            resourceList = new MonsterResourceList(
                resource.name, RankType.HIGH, Lang.RU,
                resource.highRU.Split(";", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);

            resourceList = new MonsterResourceList(
                resource.name, RankType.MASTER, Lang.RU, resource.mrRU.Split(";", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);

            resourceList = new MonsterResourceList(resource.name, RankType.LOW, Lang.ENG,
                resource.lowENG.Split("", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);

            resourceList = new MonsterResourceList(
                resource.name, RankType.HIGH, Lang.ENG,
                resource.highENG.Split(";", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);

            resourceList = new MonsterResourceList(
                resource.name, RankType.MASTER, Lang.ENG, resource.mrENG.Split(";", StringSplitOptions.RemoveEmptyEntries));

            _riseLists.Add(resourceList);
        }
    }

    public List<string> GetResources(MonsterModel model, Lang lang)
    {
        if (model.style == StyleType.RISE)
        {
            var monsterList = RiseList.FindAll(x => x.Key == model.name);
            var rankList = monsterList.FindAll(x => x.RankType == model.rank);
            var langList = rankList.Find(x => x.Lang == lang);
            return langList.Resources;
        }
        
        if (model.style == StyleType.WORLD)
        {
            var monsterList = WorldList.FindAll(x => x.Key == model.name);
            var rankList = monsterList.FindAll(x => x.RankType == model.rank);
            var langList = rankList.Find(x => x.Lang == lang);
            return langList.Resources;
        }

        return null;
    }
}