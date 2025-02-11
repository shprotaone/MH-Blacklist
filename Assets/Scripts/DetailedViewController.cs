public class DetailedViewController
{
    private DetailedView _view;
    public DetailedViewController(DetailedView detailedView)
    {
        _view = detailedView;

    }

    public void Fill(MonsterModel model)
    {
        Show();
        _view.Close.onClick.AddListener(Hide);
    }

    public void Show()
    {
        _view.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _view.gameObject.SetActive(false);
    }
}