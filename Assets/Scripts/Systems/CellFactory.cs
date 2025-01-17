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

    private IObjectPool<MonsterCell> _cellPool;

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
        CreatePool();
    }

    public async void CreateCells(List<MonsterModel> monsters)
    {
        foreach (var data in monsters)
        {
            var monsterCell = _cellPool.Get();
            monsterCell.transform.SetParent(_content,false);
            monsterCell.Initialize(_globalSystems,data);
            monsterCell.SetBackground(GetRandomBackGround());
            _monstersView.AddToList(monsterCell);
        }
    }

    private void CreatePool()
    {
        _cellPool = new ObjectPool<MonsterCell>(CreatePooled, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
            true,50);
    }

    private MonsterCell CreatePooled()
    {
        var monsterCell = Instantiate(_monsterCellPrefab, _poolContainer);
        monsterCell.SetPool(_cellPool);
        return monsterCell;
    }

    private void OnTakeFromPool(MonsterCell obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(MonsterCell obj)
    {
        Debug.Log("Returned " + obj.Name);
        obj.transform.SetParent(_poolContainer);
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(MonsterCell obj)
    {
        Destroy(obj.gameObject);
    }

    private Sprite GetRandomBackGround()
    {
        int index = Random.Range(0, _cellSprites.Count - 1);
        return _cellSprites[index];
    }
}
