using Data;
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

    private MonsterModel _model;
    private bool _isDefeated;

    public MonsterModel Model => _model;
    public string Name => _name.text;

    public void Initialize(MonsterModel model)
    {
        _model = model;
        _name.text = GlobalSystems.Instance.GetName(model.name);
        _type.text = GlobalSystems.Instance.GetMonsterTypeName(model.type);

        string imageName = model.imageName + " " + GlobalSystems.Instance.GetStyle(model.style);
        _monsterImage.sprite = GlobalSystems.Instance.GetSprite(imageName);
        _rankImage.sprite = GlobalSystems.Instance.GetSprite(model.rank,model.style);
        _weakness.Fill(model.weaknessTypes,model.style);
        _isDefeated = GlobalSystems.Instance.GetDefeated(model);
        _defeatedImage.gameObject.SetActive(_isDefeated);
        _handler.Initialize(this);
    }

    public void ChangeState()
    {
        _isDefeated = !_isDefeated;
        _defeatedImage.gameObject.SetActive(_isDefeated);
        GlobalSystems.Instance.SetDefeatedState(_model.name,_model.rank,_model.style, _isDefeated);
    }
}