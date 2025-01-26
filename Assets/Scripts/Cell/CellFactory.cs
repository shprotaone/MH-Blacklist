using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class CellFactory : MonoBehaviour
{
    [SerializeField] private Transform _poolContainer;
    private Transform _content;
    private MonsterCell _monsterCellPrefab;
    private List<Sprite> _cellSprites;
    private AssetProvider _assetProvider;
    private MonsterScrollView _monstersView;

    private GlobalSystems _globalSystems;

    public async UniTask Initialize(GlobalSystems globalSystems,AssetProvider assetProvider,UIController uiController)
    {
        _cellSprites = new List<Sprite>();
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround1"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround2"));
        _cellSprites.Add(assetProvider.GetSprite("CardBackGround3"));
        _assetProvider = assetProvider;
        _monsterCellPrefab = await assetProvider.LoadMonsterCell();
        _content = uiController.MonsterScrollView.ContentContainer;
        _globalSystems = globalSystems;
        _monstersView = uiController.MonsterScrollView;
    }

    public async UniTask CreateCells(List<MonsterModel> monsters)
    {
        foreach (var data in monsters)
        {
            var monsterCell = Instantiate(_monsterCellPrefab,_content,false);
            monsterCell.Initialize(_globalSystems,data);
            monsterCell.SetBackground(GetRandomBackGround());
            _monstersView.AddToList(monsterCell);
        }

        await UniTask.Yield();
    }

    private Sprite GetRandomBackGround()
    {
        int index = Random.Range(0, _cellSprites.Count - 1);
        return _cellSprites[index];
    }
}
