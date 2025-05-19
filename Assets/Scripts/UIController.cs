using Cysharp.Threading.Tasks;
using Data.JSON;
using DG.Tweening;
using Systems;
using Systems.Factory;
using UnityEngine;
using View.DetailPanel;
using View.Settings;
using View.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _curtainPanel;
    [SerializeField] private MainPanelView _portraitPanel;
    [SerializeField] private RankTabController _rankTabController;

    [SerializeField] private RectTransform _openVerticalPos;
    [SerializeField] private RectTransform _closePosition;
    [SerializeField] private GameObject _powerPrefab;

    private DetailPanelView _detailPanelView;
    private SettingsView _settingsView;
    private RectTransform _settingsPanel;
    private UIFactory _uiFactory;
    private GlobalSystems _globalSystems;
    private bool _isPortrait;

    public RankTabController RankTabController => _rankTabController;
    public Transform CurtainPanel => _curtainPanel;
    public SettingsView SettingsView => _settingsView;
    public UIFactory UIFactory => _uiFactory;

    public async UniTask Initialize(AssetProvider assetProvider,SettingsController settingsController,GlobalSystems globalSystems)
    {
        _uiFactory = new UIFactory();
        _globalSystems = globalSystems;
        await _uiFactory.Initialize(assetProvider);
        _rankTabController.Initialize(assetProvider);
        CreateSettingsWindow(settingsController);
        CreateDetailWindow();

        _portraitPanel.SettingsButton.onClick.AddListener(() => CallSettings(true));
    }

    private void CreateDetailWindow()
    {
        _detailPanelView = Instantiate(_uiFactory.GetDetailedView(), _portraitPanel.transform, false);
        _detailPanelView.Initialize(_powerPrefab,_globalSystems);
        _detailPanelView.Hide();
        _globalSystems.LanguageProvider.Add(_detailPanelView.GetComponentsInChildren<TranslateObj>());
    }

    private void CreateSettingsWindow(SettingsController settingsController)
    {
        _settingsView = Instantiate(_uiFactory.GetSettingsView(), _portraitPanel.transform, false);
        _settingsView.Initialize(settingsController);
        _settingsPanel = _settingsView.GetComponent<RectTransform>();
        _globalSystems.LanguageProvider.Add(_settingsView.GetComponentsInChildren<TranslateObj>());
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

    public void FillDetailedList(MonsterModel model)
    {
        _detailPanelView.Fill(model,_powerPrefab);
        _detailPanelView.gameObject.SetActive(true);
    }
}