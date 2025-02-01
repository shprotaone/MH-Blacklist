using UnityEngine;

public class DetailedView : MonoBehaviour
{
    private DetailedViewController _controller;

    public DetailedViewController Controller => _controller;

    public void Initialize(DetailedViewController controller)
    {
        _controller = controller;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}