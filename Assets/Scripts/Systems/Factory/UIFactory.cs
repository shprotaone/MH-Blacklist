using Cysharp.Threading.Tasks;
using UnityEngine;
using View;
using View.DetailPanel;
using View.Settings;

namespace Systems.Factory
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private Transform _listViewContainer;
        
        private AssetProvider _assetProvider;
        private MonsterScrollView _monsterScrollView;
        private DetailPanelView detailPanelView;
        private SettingsView _settingsView;

        public async UniTask Initialize(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            var go = await _assetProvider.GetPrefabAsync("SettingsPanel");
            _settingsView = go.GetComponent<SettingsView>();

            go = await _assetProvider.GetPrefabAsync("DetailedPanel");
            detailPanelView = go.GetComponent<DetailPanelView>();

            go = await _assetProvider.GetPrefabAsync("MonsterScrollView");
            _monsterScrollView = go.GetComponent<MonsterScrollView>();
        }

        public SettingsView GetSettingsView()
        {
            return _settingsView;
        }

        public DetailPanelView  GetDetailedView()
        {
            return detailPanelView;
        }

        public MonsterScrollView GetScrollView()
        {
            return Instantiate(_monsterScrollView,_listViewContainer,false);
        }
    }
}
