using Data;
using DefaultNamespace.View;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private MainPanelView _portraitPanel;

    [SerializeField] private MonsterScrollView monsterScrollView;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private RectTransform _settingsPanel;

    [SerializeField] private RectTransform _openVerticalPos;
    [SerializeField] private RectTransform _closePosition;
    
    private bool _isPortrait;
    public MonsterScrollView MonsterScrollView => monsterScrollView;
    public SettingsView SettingsView => _settingsView;

    public void Initialize(MonsterListChanger monsterListChanger)
    {
        monsterScrollView.Initialize(1);

        _portraitPanel.WorldButton.Button.onClick.AddListener(monsterListChanger.CreateWorldList);
        _portraitPanel.RiseButton.Button.onClick.AddListener(monsterListChanger.CreateRiseList);
        _portraitPanel.SettingsButton.onClick.AddListener(() => CallSettings(true));
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

    public void SetScaledButton(StyleType styleType)
    {
        if (styleType == StyleType.RISE)
        {
            _portraitPanel.WorldButton.SetMainScaled(false);
            _portraitPanel.WorldButton.SetScaled(false);
            _portraitPanel.RiseButton.SetMainScaled(true);
            _portraitPanel.RiseButton.SetScaled(true);
        }
        else if (styleType == StyleType.WORLD)
        {
            _portraitPanel.WorldButton.SetMainScaled(true);
            _portraitPanel.WorldButton.SetScaled(true);
            _portraitPanel.RiseButton.SetMainScaled(false);
            _portraitPanel.RiseButton.SetScaled(false);
        }
    }

    public void ClearScrollList()
    {
        monsterScrollView.Clear();
    }
}