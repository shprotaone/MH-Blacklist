using Data.JSON;
using Systems;
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
        [SerializeField] private AttackFieldView _attackFieldView;
        [SerializeField] private LocationFieldView _locationFieldView;

        public void Fill(MonsterModel model)
        {
            string spriteName = model.imageName + " " + model.style;

            _monsterIcon.sprite = GlobalSystems.Instance.GetSprite(spriteName);
            _nameText.text = GlobalSystems.Instance.GetName(model.name);
            _typeText.text = GlobalSystems.Instance.GetMonsterTypeName(model.type);
            _weaknessView.Fill(model);
            _weaknessStatusView.Fill(model);
            _attackFieldView.Fill(model);
            _locationFieldView.Fill(model);
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
