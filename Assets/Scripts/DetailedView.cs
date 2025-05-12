using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailedView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _details;
    [SerializeField] private Button _resources;

    [SerializeField] private DetailsView _detailsView;
    [SerializeField] private ResourcesView _resourcesView;
    
    private DetailedViewController _controller;
    private MonsterResourcesParser _monsterResourcesParser;
    private GlobalSystems _globalSystems;

    public Button Close => _closeButton;
    public DetailedViewController Controller => _controller;

    public void Initialize(DetailedViewController controller, GameObject powerPrefab,GlobalSystems globalSystems)
    {
        _controller = controller;
        _detailsView.Initialize(powerPrefab);
        _globalSystems = globalSystems;
        _monsterResourcesParser = _globalSystems.MosterResourcesParser;
        _details.onClick.AddListener(CallDetails);
        _resources.onClick.AddListener(CallResourceList);
        _resourcesView.Hide();
    }

    private void CallResourceList()
    {
        _detailsView.Hide();
        _resourcesView.Show();
    }

    private void CallDetails()
    {
        _detailsView.Show();
        _resourcesView.Hide();
    }

    public void SetUp(MonsterModel model, List<Sprite> weaknessSprites, Dictionary<Sprite, int> weaknessStatusSprites)
    {
        var name = _globalSystems.GetName(model.name);
        var type = _globalSystems.GetMonsterTypeName(model.type);
        string spriteName = model.imageName + " " + _globalSystems.GetStyle(model.style);
        var sprite = _globalSystems.GetSprite(spriteName);
        
        _detailsView.Fill(name,type,sprite,weaknessSprites,weaknessStatusSprites);
        _resourcesView.Fill(_monsterResourcesParser.GetResources(model,_globalSystems.GetLang()));

    }
}