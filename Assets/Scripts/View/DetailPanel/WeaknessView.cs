using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using Enums;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class WeaknessView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private List<StatusView> _slots;
        [SerializeField] private TMP_Text _textStatus;

        public async void Fill(MonsterModel model)
        {
            DisableSlots();
            var weaknessSprites = CollectWeaknessSprites(model);
            var weaknessSpecial = CollectSpecialWeaknessSprites(model);
            FillTextStatus(model);

            for (int i = 0; i < weaknessSprites.Count; i++)
            {
                _slots[i].Fill(weaknessSprites.ElementAt(i),weaknessSpecial);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
        }

        private void FillTextStatus(MonsterModel model)
        {
            if (model.specialWeaknessState != SpecialWeaknessType.EMPTY)
            {
                _textStatus.gameObject.SetActive(true);
                _textStatus.text = GlobalSystems.Instance.GetTextSpecialStatus(model);
            }
        }

        private Dictionary<Sprite,int> CollectWeaknessSprites(MonsterModel model)
        {
            Dictionary<Sprite,int> _weaknessSprites = new ();

            foreach (var type in model.weaknessTypes)
            {
                _weaknessSprites.Add(GlobalSystems.Instance.GetSprite(type.Key),type.Value);
            }

            return _weaknessSprites;
        }

        private Dictionary<Sprite,int> CollectSpecialWeaknessSprites(MonsterModel model)
        {
            if (model.specialWeaknessTypes == null) return null;

            Dictionary<Sprite,int> _weaknessSprites = new ();
            foreach (var type in model.specialWeaknessTypes)
            {
                _weaknessSprites.Add(GlobalSystems.Instance.GetSprite(type.Key),type.Value);
            }

            return _weaknessSprites;
        }

        private void DisableSlots()
        {
            foreach (var slotImage in _slots)
            {
                slotImage.Disable();
            }

            _textStatus.gameObject.SetActive(false);
        }
    }
}