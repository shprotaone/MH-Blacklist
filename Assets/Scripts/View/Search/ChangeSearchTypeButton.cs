using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Search
{
    public class ChangeSearchTypeButton : MonoBehaviour
    {
        [SerializeField] private Button _changeButton;
    
        private TMP_Text _text;
        private FindType _findType;
        public FindType FindType => _findType;

        public void Initialize()
        {
            _text = _changeButton.GetComponentInChildren<TMP_Text>();
            _changeButton.onClick.AddListener(Change);
            Change();
        }

        private void Change()
        {
            _findType++;
            if ((int)_findType >= 3) _findType = 0;

            if (_findType == FindType.MATERIAL)
            {
                _text.text = "MAT";
            }
            else if (_findType == FindType.MONSTER)
            {
                _text.text = "MON";
            }
            else if (_findType == FindType.MONSTER_TYPE)
            {
                _text.text = "TYP";
            }

            Debug.Log("Current find " + _findType);
        }
    }
}
