using System.Collections.Generic;
using Systems;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeaknessView : MonoBehaviour
{
    [SerializeField] private GameObject _leftBrace;
    [SerializeField] private GameObject _rightBrace;
    [SerializeField] private Transform _container;

    private List<GameObject> _powers = new ();

    public void Fill(KeyValuePair<Sprite, int> element)
    {
        Disable();
        gameObject.SetActive(true);
        //DisableAll();
        // _leftBrace.SetActive(true);
        // _rightBrace.SetActive(true);
        // _container.gameObject.SetActive(true);

        var prefab = GlobalSystems.Instance.AssetProvider.GetPrefab<Image>("starPrefab");

        if (element.Value == 0)
        {
            var image = Instantiate(prefab, _container, false);
            image.sprite = GlobalSystems.Instance.GetSprite("effectCross");
            _powers.Add(image.gameObject);
        }

        for (int i = 0; i < element.Value; i++)
        {
            var power = Instantiate(prefab, _container, false);
            power.sprite = GlobalSystems.Instance.GetSprite("effectStar");
            _powers.Add(power.gameObject);
        }
    }

    private void DisableAll()
    {
        _leftBrace.SetActive(false);
        _rightBrace.SetActive(false);
        _container.gameObject.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);

        foreach (var power in _powers)
        {
            Destroy(power.gameObject);
        }
        _powers.Clear();
    }
}