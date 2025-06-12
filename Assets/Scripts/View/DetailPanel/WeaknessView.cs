using System.Collections.Generic;
using Data.JSON;
using Enums;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class WeaknessView : MonoBehaviour
    {
        [SerializeField] private List<Image> _slots;

        public void Fill(MonsterModel model)
        {
            DisableSlots();

            var weaknessSprites = CollectWeaknessSprites(model);

            for (int i = 0; i < weaknessSprites.Count; i++)
            {
                _slots[i].sprite = weaknessSprites[i];
                _slots[i].gameObject.SetActive(true);
            }
        }

        private List<Sprite> CollectWeaknessSprites(MonsterModel model)
        {
            List<Sprite> _weaknessSprites = new List<Sprite>();

            foreach (WeaknessType type in model.weaknessTypes)
            {
                _weaknessSprites.Add(GlobalSystems.Instance.GetSprite(type, model.style));
            }

            return _weaknessSprites;
        }

        private void DisableSlots()
        {
            foreach (var slotImage in _slots)
            {
                slotImage.gameObject.SetActive(false);
            }
        }
    }
}