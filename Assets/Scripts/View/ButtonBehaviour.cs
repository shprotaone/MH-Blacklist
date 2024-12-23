using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Button _button;
    private bool _isScaled;
    private bool _isMain;

    public Button Button => _button;

    public void SetMainScaled(bool b)
    {
        _isMain = b;
    }

    public void SetScaled(bool flag)
    {
        if (flag) SetBig();
        else SetSmall();
    }

    private void SetSmall()
    {
        if(_isMain) return;
        if(_isScaled)
        {
            transform.DOScale(new Vector2(1, 1), 0.2f).SetEase(Ease.InCubic);
            _isScaled = false;
        }
    }

    private void SetBig()
    {

        if (!_isScaled)
        {
            transform.DOScale(new Vector2(2, 2), 0.2f).SetEase(Ease.InCubic);
            _isScaled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_isScaled) return;
        SetBig();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetSmall();
    }
}
