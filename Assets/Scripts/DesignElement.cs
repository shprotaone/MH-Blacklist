using UnityEngine;
using UnityEngine.UI;

public class DesignElement : MonoBehaviour
{
    [SerializeField] private DesignElementType _type;
    [SerializeField] private Image _image;

    public void ChangeSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}