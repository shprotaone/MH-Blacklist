using TMPro;
using UnityEngine;

namespace View.Curtain
{
    public class CurtainView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}