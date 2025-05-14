using System;
using System.Collections.Generic;
using Cell;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MonsterScrollView : MonoBehaviour
    {
        public event Action<string> OnChangeSize;

        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private Transform _contentContainer;

        private List<Vector2> _sizesVertical;
        private List<Vector2> _sizesHorizontal;

        private List<MonsterCell> _cells;
        private int _currentIndex = 1;

        public List<MonsterCell> Cells => _cells;
        public Transform ContentContainer => _contentContainer;

        public void Initialize(int savedIndexSize)
        {
            _currentIndex = savedIndexSize;
            _cells = new List<MonsterCell>();
            CreateSizes();
            ChangeSize();
            OnChangeSize?.Invoke(_currentIndex.ToString());
        }

        public void Clear()
        {
            for (int i = 0; i < _contentContainer.childCount; i++)
            {
                _cells[i].Disable();
            }

            _cells.Clear();
            Destroy(gameObject);
        }

        public void DecreaseCellSize()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ChangeSize();
            }
        }

        public void IncreaseCellSize()
        {
            if (_currentIndex < 2)
            {
                _currentIndex++;
                ChangeSize();
            }
        }

        private void ChangeSize()
        {
            _gridLayoutGroup.cellSize = _sizesVertical[_currentIndex];
        }

        public void AddToList(MonsterCell monsterCell)
        {
            _cells.Add(monsterCell);
        }

        private void CreateSizes()
        {
            _sizesVertical = new List<Vector2>();

            _sizesVertical.Add(new Vector2(220,320));
            _sizesVertical.Add(new Vector2(300,400));
            _sizesVertical.Add(new Vector2(460,560));

            // _sizesHorizontal = new List<Vector2>();
            //
            // _sizesHorizontal.Add(new Vector2(200,300));
            // _sizesHorizontal.Add(new Vector2(340,440));
            // _sizesHorizontal.Add(new Vector2(420,520));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
