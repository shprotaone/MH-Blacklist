using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaknessStatusView : MonoBehaviour
{
    [SerializeField] private List<StatusView> _slots;

    public void Fill(GameObject powerImagePrefab, Dictionary<Sprite,int> weaknessSprites)
    {
        DisableSlots();

        for (int i = 0; i < weaknessSprites.Count; i++)
        {
            _slots[i].Fill(powerImagePrefab, weaknessSprites.ElementAt(i));
        }
    }

    private void DisableSlots()
    {
        foreach (var slotImage in _slots)
        {
            slotImage.Disable();
        }
    }
}