using Data;
using DefaultNamespace.View;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private MainPanelView _portraitPanel;
    [SerializeField] private MainPanelView _landscapePanel;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private RectTransform _settingsPanel;

    [SerializeField] private RectTransform _openVerticalPos;

    private Vector3 _startPos;
    private bool _isPortrait;
    private bool _settingsIsOpen = false;

    public void Initialize(Bootstrap bootstrap)
    {
        _portraitPanel.WorldButton.Button.onClick.AddListener(bootstrap.CreateWorldList);
        _portraitPanel.RiseButton.Button.onClick.AddListener(bootstrap.CreateRiseList);
        _portraitPanel.SettingsButton.onClick.AddListener(CallSettings);
        _landscapePanel.WorldButton.Button.onClick.AddListener(bootstrap.CreateWorldList);
        _landscapePanel.RiseButton.Button.onClick.AddListener(bootstrap.CreateRiseList);
        _landscapePanel.SettingsButton.onClick.AddListener(CallSettings);
        _startPos = _settingsPanel.position;
    }

    public void SetPortrait()
    {
        if(_isPortrait) return;

        _isPortrait = true;
        _landscapePanel.gameObject.SetActive(false);
        _portraitPanel.gameObject.SetActive(true);
        _portraitPanel.ScrollRect.content = _contentTransform.GetComponent<RectTransform>();
        _contentTransform.SetParent(_portraitPanel.ViewPort,false);

    }

    public void SetLandscape()
    {
        if(!_isPortrait) return;
        _isPortrait = false;
        _landscapePanel.gameObject.SetActive(true);
        _landscapePanel.ScrollRect.content = _contentTransform.GetComponent<RectTransform>();
        _portraitPanel.gameObject.SetActive(false);
        _contentTransform.SetParent(_landscapePanel.ViewPort,false);
    }

    public void CallSettings()
    {
        if (!_settingsIsOpen)
        {
            _settingsIsOpen = true;
            _portraitPanel.SettingsButton.transform.DORotate(new Vector3(0, 0, -180), 0.5f).SetEase(Ease.InOutCubic);
            _settingsPanel.DOAnchorPos(_openVerticalPos.anchoredPosition, 0.5f).SetEase(Ease.InOutCubic);
        }
        else
        {
            _settingsIsOpen = false;
            _portraitPanel.SettingsButton.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutCubic);
            _settingsPanel.DOMove(_startPos, 0.5f).SetEase(Ease.InOutCubic);
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
}