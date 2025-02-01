public class DetailedViewController
{
    private DetailedView _view;
    public DetailedViewController(DetailedView detailedView)
    {
        _view = detailedView;
    }

    public void Fill(MonsterModel model)
    {
        _view.Show();
    }
}