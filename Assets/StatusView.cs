using System;
using System.Collections.Generic;
using System.Linq;
using Data.JSON;
using Systems;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Transform _powerContainer;
    [SerializeField] private SpecialWeaknessView _specialWeaknessView;

    private List<GameObject> _powers = new List<GameObject>();

    public void Fill(KeyValuePair<Sprite, int> element)
    {
        _image.sprite = element.Key;
        var prefab = GlobalSystems.Instance.AssetProvider.GetPrefab<Image>("starPrefab");

        if (element.Value == 0)
        {
            var image = Instantiate(prefab, _powerContainer, false);
            image.sprite = GlobalSystems.Instance.GetSprite("effectCross");
            _powers.Add(image.gameObject);
        }

        for (int i = 0; i < element.Value; i++)
        {
            var power = Instantiate(prefab, _powerContainer, false);
            power.sprite = GlobalSystems.Instance.GetSprite("effectStar");
            _powers.Add(power.gameObject);
        }
    }

    public void Fill(KeyValuePair<Sprite, int> element, Dictionary<Sprite,int> special)
    {
        _image.sprite = element.Key;
        var prefab = GlobalSystems.Instance.AssetProvider.GetPrefab<Image>("starPrefab");

        if (_specialWeaknessView != null && special != null)
        {
            if (special.ContainsKey(element.Key))
            {
                var pair = special.First(x => x.Key == element.Key);
                _specialWeaknessView.Fill(pair);
            }
            else
            {
                _specialWeaknessView.Disable();
            }
        }
        else
        {
            _specialWeaknessView.Disable();
        }


        if (element.Value == 0)
        {
            var image = Instantiate(prefab, _powerContainer, false);
            image.sprite = GlobalSystems.Instance.GetSprite("effectCross");
            _powers.Add(image.gameObject);
        }

        for (int i = 0; i < element.Value; i++)
        {
            var power = Instantiate(prefab, _powerContainer, false);
            power.sprite = GlobalSystems.Instance.GetSprite("effectStar");
            _powers.Add(power.gameObject);
        }
    }


    public void Disable()
    {
        foreach (var power in _powers)
        {
            Destroy(power);
        }

        //transform.gameObject.SetActive(false);
        _powers.Clear();
    }
}