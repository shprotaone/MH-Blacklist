using Data.JSON;
using Parser;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class DetailPanelView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _addButton;
        [SerializeField] private Image _background;
        [SerializeField] private DetailView _detailView;
        [SerializeField] private ResourcesView _resourcesView;
        [SerializeField] private RankField _rankField;
        
        private MonsterResourcesParser _monsterResourcesParser;
        private MonsterModel _model;

        public void Initialize()
        {
            _monsterResourcesParser = GlobalSystems.Instance.MosterResourcesParser;
        }

        private void AddToQuickList()
        {
            GlobalSystems.Instance.AddToKillList(_model);
        }

        private void SetUp(MonsterModel model)
        {
            _detailView.Fill(model);
            _rankField.Init(model,_resourcesView);

            _model = model;
            _addButton.onClick.AddListener(AddToQuickList);
            _background.sprite = GlobalSystems.Instance.GetDetailBackground();
            _resourcesView.Fill(_monsterResourcesParser.GetResources(model));
        }

        public void Fill(MonsterModel model,GameObject powerPrefab)
        {
            Show();
            SetUp(model);
            _closeButton.onClick.AddListener(Hide);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}