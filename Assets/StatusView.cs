using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Transform _powerContainer;

    private List<GameObject> _powers = new List<GameObject>();
    public void Fill(GameObject powerImagePrefab, KeyValuePair<Sprite, int> element)
    {
        _image.sprite = element.Key;
        
        if(element.Value > 0) transform.gameObject.SetActive(true);
        
        for (int i = 0; i < element.Value; i++)
        {
            _powers.Add(Instantiate(powerImagePrefab, _powerContainer, false));
        }
    }

    public void Disable()
    {
        foreach (var power in _powers)
        {
            Destroy(power);
        }

        transform.gameObject.SetActive(false);
        _powers.Clear();
    }
}