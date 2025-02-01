using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickCell : MonoBehaviour
{
    [SerializeField] private Button _completeButton;
    [SerializeField] private TMP_Text _name;

    private QuickMonsterListController _listController;
    private MonsterModel _model;
    public void Initialize(QuickMonsterListController listController, MonsterModel model)
    {
        _model = model;
        _name.text = model.name;
        _listController = listController;
        _completeButton.onClick.AddListener(Complete);
    }

    private void Complete()
    {
        _listController.DeleteMonster(_model);
    }

    public void Disable()
    {
        Destroy(this.gameObject);
    }
}
