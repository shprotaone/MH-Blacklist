using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _type;

    [SerializeField] private Image _monsterImage;
    [SerializeField] private Image _defeatedImage;
    [SerializeField] private Image _rankImage;
    [SerializeField] private WeaknessView _weakness;

    [SerializeField] private ClickHandler _handler;

    private GlobalSystems _globalSystems;
    private MonsterModel _model;
    private bool _isDefeated;

    public MonsterModel Model => _model;
    public string Name => _name.text;

    public void Initialize(GlobalSystems globalSystems, MonsterModel model)
    {
        _globalSystems = globalSystems;
        _model = model;
        _name.text = globalSystems.GetName(model.name);
        _type.text = globalSystems.GetMonsterTypeName(model.type);

        string imageName = model.imageName + " " + globalSystems.GetStyle(model.style);
        _monsterImage.sprite = globalSystems.GetSprite(imageName);
        _rankImage.sprite = globalSystems.GetSprite(model.rank,model.style);
        _weakness.Fill(_globalSystems,model.weaknessTypes,model.style);
        _isDefeated = globalSystems.GetDefeated(model);
        _defeatedImage.gameObject.SetActive(_isDefeated);
        _handler.Initialize(this);
    }

    public void ChangeState()
    {
        _isDefeated = !_isDefeated;
        _defeatedImage.gameObject.SetActive(_isDefeated);
        _globalSystems.SetDefeatedState(_model.name,_model.rank,_model.style, _isDefeated);
    }
}