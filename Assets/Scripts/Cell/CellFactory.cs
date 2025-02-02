using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class CellFactory : MonoBehaviour
{
    [SerializeField] private Transform _poolContainer;
    private MonsterCell _monsterCellPrefab;
    private List<Sprite> _cellSprites;

    private GlobalSystems _globalSystems;

    public async UniTask Initialize(GlobalSystems globalSystems,AssetProvider assetProvider,UIController uiController)
    {
        _cellSprites = new List<Sprite>();
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround1"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround2"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround3"));
        _monsterCellPrefab = await assetProvider.LoadMonsterCell();
        _globalSystems = globalSystems;
    }

    public async UniTask CreateCells(List<MonsterModel> monsters,MonsterScrollView scrollView)
    {
        foreach (var data in monsters)
        {
            var monsterCell = Instantiate(_monsterCellPrefab,scrollView.ContentContainer,false);
            monsterCell.Initialize(_globalSystems,data);
            monsterCell.SetBackground(GetRandomBackGround());
            scrollView.AddToList(monsterCell);
        }

        await UniTask.Yield();
    }

    private Sprite GetRandomBackGround()
    {
        int index = Random.Range(0, _cellSprites.Count - 1);
        return _cellSprites[index];
    }
}
