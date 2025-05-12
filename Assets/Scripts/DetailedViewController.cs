using System.Collections.Generic;
using UnityEngine;

public class DetailedViewController
{
    private GlobalSystems _globalSystems;
    private DetailedView _view;
    
    public DetailedViewController (DetailedView detailedView, GlobalSystems globalSystems)
    {
        _view = detailedView;
        _globalSystems = globalSystems;
    }

    public GameObject PowerPrefab { get; set; }

    public void Fill(MonsterModel model,GameObject powerPrefab)
    {
        PowerPrefab = powerPrefab;
        Show();

        var _weaknessSprites = CollectWeaknessSprites(model);
        var _weaknessStatusSprites = CollectWeaknessStatusSprites(model);
        _view.SetUp(model,_weaknessSprites,
                            _weaknessStatusSprites);
        _view.Close.onClick.AddListener(Hide);
    }

    private Dictionary<Sprite,int> CollectWeaknessStatusSprites(MonsterModel model)
    {
        Dictionary<Sprite,int> _weaknessSprites = new Dictionary<Sprite, int>();

        foreach (var type in model.weaknessStatusTypes)
        {
            _weaknessSprites.Add(_globalSystems.GetSprite(type.Key, model.style),type.Value);
        }

        return _weaknessSprites;
    }

    private List<Sprite> CollectWeaknessSprites(MonsterModel model)
    {
        List<Sprite> _weaknessSprites = new List<Sprite>();

        foreach (WeaknessType type in model.weaknessTypes)
        {
            _weaknessSprites.Add(_globalSystems.GetSprite(type, model.style));
        }

        return _weaknessSprites;
    }

    public void Show()
    {
        _view.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _view.gameObject.SetActive(false);
    }
}