using System.Collections.Generic;
using Enums;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class WeaknessView : MonoBehaviour
    {
        [SerializeField] private List<Image> _slots;

        public void Fill(GlobalSystems globalSystems, WeaknessType[] dataWeaknessTypes,StyleType type)
        {
            DisableSlots();

            for (int i = 0; i < dataWeaknessTypes.Length; i++)
            {
                _slots[i].sprite = globalSystems.GetSprite(dataWeaknessTypes[i],type);
                _slots[i].gameObject.SetActive(true);
            }
        }

        public void Fill(List<Sprite> weaknessSprites)
        {
            DisableSlots();
        
            for (int i = 0; i < weaknessSprites.Count; i++)
            {
                _slots[i].sprite = weaknessSprites[i];
                _slots[i].gameObject.SetActive(true);
            }
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