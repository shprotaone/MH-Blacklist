using System.Collections.Generic;
using Cell;
using Cysharp.Threading.Tasks;
using Data.JSON;
using UnityEngine;
using View;

namespace Systems.Factory
{
    public class CellFactory : MonoBehaviour
    {
        [SerializeField] private Transform _poolContainer;
        private MonsterCell _monsterCellPrefab;
        private List<Sprite> _cellSprites;

        private GlobalSystems _globalSystems;

        public async UniTask Initialize(AssetProvider assetProvider,GlobalSystems globalSystems, UIController uiController)
        {
            _cellSprites = new List<Sprite>();
            _cellSprites.Add(assetProvider.GetSprite("CardBackGround1"));
            _cellSprites.Add(assetProvider.GetSprite("CardBackGround2"));
            _cellSprites.Add(assetProvider.GetSprite("CardBackGround3"));
            _monsterCellPrefab = await assetProvider.LoadMonsterCell();
            _monsterCellPrefab.transform.SetParent(_poolContainer);
            _monsterCellPrefab.gameObject.SetActive(false);
            _globalSystems = globalSystems;
        }

        public async UniTask CreateCells(List<MonsterModel> monsters,MonsterScrollView scrollView)
        {
            foreach (var data in monsters)
            {
                var monsterCell = Instantiate(_monsterCellPrefab,scrollView.ContentContainer,false);
                monsterCell.Initialize(_globalSystems,data);
                monsterCell.SetBackground(GetBackground());
                scrollView.AddToList(monsterCell);
            }

            await UniTask.Yield();
        }

        private Sprite GetBackground()
        {
            return GlobalSystems.Instance.GetCellBackground();
        }
    }
}
