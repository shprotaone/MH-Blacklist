using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using Systems;
using UnityEngine;

public class WeaknessStatusView : MonoBehaviour
{
    [SerializeField] private List<StatusView> _slots;

    public async void Fill(MonsterModel model)
    {
        DisableSlots();
        var prefab = await GlobalSystems.Instance.AssetProvider.GetPrefabAsync("starPrefab");
        var weaknessSprites = CollectWeaknessStatusSprites(model);

        for (int i = 0; i < weaknessSprites.Count; i++)
        {
            _slots[i].Fill(prefab, weaknessSprites.ElementAt(i));
        }
    }

    private Dictionary<Sprite,int> CollectWeaknessStatusSprites(MonsterModel model)
    {
        Dictionary<Sprite,int> _weaknessSprites = new Dictionary<Sprite, int>();

        foreach (var type in model.weaknessStatusTypes)
        {
            _weaknessSprites.Add(GlobalSystems.Instance.GetSprite(type.Key, model.style),type.Value);
        }

        return _weaknessSprites;
    }

    private void DisableSlots()
    {
        foreach (var slotImage in _slots)
        {
            slotImage.Disable();
        }
    }
}