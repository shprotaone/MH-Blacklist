using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterListButton : MonoBehaviour
{
    public Action OnShow;
    [SerializeField] private Button _button;
    [SerializeField] private Image _spriteImage;

    private MonsterScrollView _scrollView;
    private RankType _rankType;
    private bool _isSelected;

    public RankType Rank => _rankType;

    public void Initialize(RankType rank, Sprite image,MonsterScrollView scrollView,RankTabController rankTabController)
    {
        _spriteImage.sprite = image;
        _rankType = rank;
        _scrollView = scrollView;

        _button.onClick.AddListener(() => rankTabController.Show(_rankType));
    }

    private void ChangeColor()
    {
        if(_button == null) return;

        if (_isSelected) _button.image.color = Color.gray;
        else _button.image.color = Color.white;
    }

    public void Disable()
    {
        _isSelected = false;
        if(_scrollView == null) return;
        _scrollView.gameObject.SetActive(false);
        ChangeColor();
    }

    public void Show()
    {
        _isSelected = true;
        _scrollView.gameObject.SetActive(true);
        ChangeColor();
        OnShow?.Invoke();
    }
}
