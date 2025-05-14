using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class DesignElement : MonoBehaviour
    {
        [SerializeField] private DesignElementType _type;
        [SerializeField] private Image _image;

        public DesignElementType Type => _type;
        public void ChangeSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}