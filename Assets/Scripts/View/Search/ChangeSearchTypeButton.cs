using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Search
{
    public class ChangeSearchTypeButton : MonoBehaviour
    {
        [SerializeField] private Button _changeButton;
    
        private TMP_Text _text;
        private bool _isMonsterFind;
        public bool IsMonsterFind => _isMonsterFind;

        public void Initialize()
        {
            _text = _changeButton.GetComponentInChildren<TMP_Text>();
            _changeButton.onClick.AddListener(Change);
            Change();
        }

        private void Change()
        {
            if (_isMonsterFind)
            {
                _isMonsterFind = false;
                _text.text = "MAT";
            }
            else
            {
                _isMonsterFind = true;
                _text.text = "MON";
            }
        }
    }
}
