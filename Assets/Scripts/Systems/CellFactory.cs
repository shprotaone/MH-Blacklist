using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    private Transform _content;
    private MonsterCell _monsterCellPrefab;
    private List<Sprite> _cellSprites;

    private GlobalSystems _globalSystems;
    public async UniTask Initialize(GlobalSystems globalSystems,AssetProvider assetProvider,UIController uiController)
    {
        _monsterCellPrefab = await assetProvider.LoadMonsterCell();
        _cellSprites = new List<Sprite>();
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround1"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround2"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround3"));
        
        _content = uiController.MonsterScrollView.ContentContainer;
        _globalSystems = globalSystems;
    }
    
    public void CreateCells(List<MonsterModel> monsters)
    {
        foreach (var data in monsters)
        {
            var monsterCell = Instantiate(_monsterCellPrefab, _content, false);
            monsterCell.Initialize(_globalSystems,data);
            monsterCell.SetBackground(GetRandomBackGround());
        }
    }

    private Sprite GetRandomBackGround()
    {
        int index = Random.Range(0, _cellSprites.Count - 1);
        return _cellSprites[index];
    }
}
