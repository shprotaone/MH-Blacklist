using Data.JSON;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cell
{
    public class QuickCell : MonoBehaviour
    {
        [SerializeField] private Button _completeButton;
        [SerializeField] private Button _detailButton;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _rankImage;

        private QuickMonsterListView _listView;
        private MonsterModel _model;
        public MonsterModel MonsterModel => _model;

        public void Initialize(QuickMonsterListView listView, MonsterModel model)
        {
            _model = model;
            _rankImage.sprite = GlobalSystems.Instance.GetSprite(model.rank);
            _name.text = GlobalSystems.Instance.GetName(model.name);
            _listView = listView;
            _completeButton.onClick.AddListener(Complete);
            _detailButton.onClick.AddListener(() => GlobalSystems.Instance.CallDetail(_model));
        }

        private void Complete()
        {
            _listView.DeleteMonster(this);
        }
    }
}
