using UnityEngine;
using UnityEngine.UI;

public class DetailedView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _details;
    [SerializeField] private Button _resources;

    private DetailedViewController _controller;

    public Button Close => _closeButton;
    public Button Details => _details;
    public Button Resources => _resources;
    public DetailedViewController Controller => _controller;

    public void Initialize(DetailedViewController controller)
    {
        _controller = controller;
        gameObject.SetActive(false);
    }
}