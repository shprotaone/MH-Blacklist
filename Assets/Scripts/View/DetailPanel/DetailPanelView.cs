using System.Collections.Generic;
using Data.JSON;
using Enums;
using Parser;
using Systems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class DetailPanelView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _details;
        [SerializeField] private Button _resources;

        [FormerlySerializedAs("_detailsView")] [SerializeField] private DetailView detailView;
        [SerializeField] private ResourcesView _resourcesView;
    
    
        private MonsterResourcesParser _monsterResourcesParser;
        private GlobalSystems _globalSystems;

        public Button Close => _closeButton;
        public GameObject PowerPrefab { get; private set; }
        public void Initialize(GameObject powerPrefab,GlobalSystems globalSystems)
        {
            PowerPrefab = powerPrefab;
            detailView.Initialize(powerPrefab);
            _globalSystems = globalSystems;
            _monsterResourcesParser = _globalSystems.MosterResourcesParser;
            _details.onClick.AddListener(CallDetails);
            _resources.onClick.AddListener(CallResourceList);
            _resourcesView.Hide();
        }

        private void CallResourceList()
        {
            detailView.Hide();
            _resourcesView.Show();
        }

        private void CallDetails()
        {
            detailView.Show();
            _resourcesView.Hide();
        }

        public void SetUp(MonsterModel model, List<Sprite> weaknessSprites, Dictionary<Sprite, int> weaknessStatusSprites)
        {
            var name = _globalSystems.GetName(model.name);
            var type = _globalSystems.GetMonsterTypeName(model.type);
            string spriteName = model.imageName + " " + _globalSystems.GetStyle(model.style);
            var sprite = _globalSystems.GetSprite(spriteName);
        
            detailView.Fill(name,type,sprite,weaknessSprites,weaknessStatusSprites);
            _resourcesView.Fill(_monsterResourcesParser.GetResources(model,_globalSystems.GetLang()));

        }

        public void Fill(MonsterModel model,GameObject powerPrefab)
        {
            PowerPrefab = powerPrefab;
            Show();

            var _weaknessSprites = CollectWeaknessSprites(model);
            var _weaknessStatusSprites = CollectWeaknessStatusSprites(model);
            SetUp(model,_weaknessSprites,
                _weaknessStatusSprites);
            Close.onClick.AddListener(Hide);
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
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}