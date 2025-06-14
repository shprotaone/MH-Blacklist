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

        private QuickMonsterListView _listView;
        private MonsterModel _model;
        public MonsterModel MonsterModel => _model;

        public void Initialize(QuickMonsterListView listView, MonsterModel model)
        {
            _model = model;
            _name.text = GlobalSystems.Instance.GetName(model.name);
            this._listView = listView;
            _completeButton.onClick.AddListener(Complete);
            _detailButton.onClick.AddListener(() => GlobalSystems.Instance.CallDetail(_model));
        }

        private void Complete()
        {
            _listView.DeleteMonster(this);
        }
    }
}
