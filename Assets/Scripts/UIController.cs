using DefaultNamespace.View;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private MainPanelView _portraitPanel;
    [SerializeField] private MainPanelView _landscapePanel;

    private bool _isPortrait;

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
}