using Data.JSON;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Cell
{
    public class MonsterCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _type;

        [SerializeField] private Image _monsterImage;
        [SerializeField] private Image _defeatedImage;
        [SerializeField] private Image _rankImage;
        [SerializeField] private Image _backGround;
        [SerializeField] private Button _detailButton;

        [SerializeField] private MonsterCellClickHandler _handler;
        //[SerializeField] private AddToListClickHandler _addToListHandler;

        private GlobalSystems _globalSystems;
        private IObjectPool<MonsterCell> _pool;
        private MonsterModel _model;
        private bool _isDefeated;

        public bool IsDefeated => _isDefeated;
        public MonsterModel Model => _model;
        public string Name => _name.text;
        public string Type => _type.text;

        public void Initialize(GlobalSystems globalSystems, MonsterModel model)
        {
            gameObject.SetActive(true);
            _globalSystems = globalSystems;
            _model = model;
            _name.text = globalSystems.GetName(model.name);
            _type.text = globalSystems.GetMonsterTypeName(model.type);

            string imageName = model.imageName + " " + globalSystems.GetStyle(model.style);
            _monsterImage.sprite = globalSystems.GetSprite(imageName);
            _rankImage.sprite = globalSystems.GetSprite(model.rank);
            _detailButton.onClick.AddListener(ChangeState);
            _isDefeated = globalSystems.GetDefeated(model);
            _defeatedImage.sprite = GlobalSystems.Instance.GetDefeatSprite();
            _defeatedImage.gameObject.SetActive(_isDefeated);

            _handler.Initialize(this);
            //_addToListHandler.Initialize(this,_globalSystems.InputSystemHandler);
        }

        public void ChangeState()
        {
            _isDefeated = !_isDefeated;
            _defeatedImage.gameObject.SetActive(_isDefeated);
            _globalSystems.SetDefeatedState(_model, _isDefeated);
        }

        public void SetBackground(Sprite background)
        {
            _backGround.sprite = background;
        }

        public void Disable()
        {
            Destroy(this.gameObject);
        }

        public void AddToKillList()
        {
            _globalSystems.AddToKillList(Model);
        }

        public void CallDetailInfo()
        {
            GlobalSystems.Instance.CallDetail(Model);
        }
    }
}