using Cysharp.Threading.Tasks;
using Data;
using DefaultNamespace.View;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _curtainPanel;
    [SerializeField] private MainPanelView _portraitPanel;
    [SerializeField] private MonsterScrollView monsterScrollView;

    [SerializeField] private RectTransform _openVerticalPos;
    [SerializeField] private RectTransform _closePosition;

    private DetailedView _detailedView;
    private SettingsView _settingsView;
    private RectTransform _settingsPanel;
    private UIFactory _uiFactory;
    private bool _isPortrait;


    public Transform CurtainPanel => _curtainPanel;
    public MonsterScrollView MonsterScrollView => monsterScrollView;
    public SettingsView SettingsView => _settingsView;

    public async UniTask Initialize(AssetProvider assetProvider,SettingsController settingsController)
    {
        monsterScrollView.Initialize(1);

        _uiFactory = new UIFactory();
        await _uiFactory.Initialize(assetProvider);
        CreateSettingsWindow(settingsController);
        CreateDetailWindow();

        _portraitPanel.SettingsButton.onClick.AddListener(() => CallSettings(true));
    }

    private void CreateDetailWindow()
    {
        _detailedView = Instantiate(_uiFactory.GetDetailedView(), _portraitPanel.transform, false);
        var controller = new DetailedViewController(_detailedView);
        _detailedView.Initialize(controller);
    }

    private void CreateSettingsWindow(SettingsController settingsController)
    {
        _settingsView = Instantiate(_uiFactory.GetSettingsView(), _portraitPanel.transform, false);
        _settingsView.Initialize(settingsController);
        _settingsPanel = _settingsView.GetComponent<RectTransform>();
    }

    public void CallSettings(bool isOpen)
    {
        if (isOpen)
        {
            _portraitPanel.SettingsButton.transform.DORotate(new Vector3(0, 0, -180), 0.5f).SetEase(Ease.InOutCubic);
            _settingsPanel.DOAnchorPos(_openVerticalPos.anchoredPosition, 0.5f).SetEase(Ease.InOutCubic);
        }
        else
        {
            _portraitPanel.SettingsButton.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutCubic);
            _settingsPanel.DOAnchorPos(_closePosition.anchoredPosition, 0.5f).SetEase(Ease.InOutCubic);
        }
    }

    public void ClearScrollList()
    {
        monsterScrollView.Clear();
    }

    public void FillDetailedList(MonsterModel model)
    {
        _detailedView.Controller.Fill(model);
    }
}