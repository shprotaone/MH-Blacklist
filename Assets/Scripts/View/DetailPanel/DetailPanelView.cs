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

        [SerializeField] private DetailView _detailView;
        [SerializeField] private ResourcesView _resourcesView;
        
        private MonsterResourcesParser _monsterResourcesParser;

        public void Initialize()
        {
            _monsterResourcesParser = GlobalSystems.Instance.MosterResourcesParser;
        }

        private void SetUp(MonsterModel model)
        {
            _detailView.Fill(model);
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