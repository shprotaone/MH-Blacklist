using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class DetailView : MonoBehaviour
    {
        [SerializeField] private Image _monsterIcon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _typeText;

        [SerializeField] private WeaknessView _weaknessView;
        [SerializeField] private WeaknessStatusView _weaknessStatusView;

        private GameObject _powerPrefab;
    
        public void Initialize(GameObject powerPrefab)
        {
            _powerPrefab = powerPrefab;
        }

        public void Fill(string monsterName, string monsterTypeName, Sprite iconSprite, List<Sprite> weaknessSprites, Dictionary<Sprite,int> weaknessStatusSprites)
        {
            _monsterIcon.sprite = iconSprite;
            _nameText.text = monsterName;
            _typeText.text = monsterTypeName;
            _weaknessView.Fill(weaknessSprites);
            _weaknessStatusView.Fill(_powerPrefab,weaknessStatusSprites);
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
