using System.Collections.Generic;
using Data.JSON;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class LocationFieldView : MonoBehaviour
    {
        [SerializeField] private List<Image> _slots;

        public void Fill(MonsterModel model)
        {
            DisableAll();

            for (int i = 0; i < model.locations.Length; i++)
            {
                _slots[i].sprite = GlobalSystems.Instance.GetSprite(model.locations[i]);
                _slots[i].gameObject.SetActive(true);
            }
        }

        private void DisableAll()
        {
            foreach (var slot in _slots)
            {
                slot.gameObject.SetActive(false);
            }
        }
    }
}