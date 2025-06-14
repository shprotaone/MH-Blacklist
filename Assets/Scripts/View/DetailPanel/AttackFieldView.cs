using System.Collections.Generic;
using Data.JSON;
using Systems;
using UnityEngine;
using UnityEngine.UI;

public class AttackFieldView : MonoBehaviour
{
    [SerializeField] private List<Image> _statusView;

    public void Fill(MonsterModel model)
    {
         DisableImages();

        for (int i = 0; i < model.attackTypes.Length; i++)
        {
            _statusView[i].sprite = GlobalSystems.Instance.GetSprite(model.attackTypes[i]);
            _statusView[i].enabled = true;
        }

    }

    private void DisableImages()
    {
        foreach (var image in _statusView)
        {
            image.enabled = false;
        }
    }
}
