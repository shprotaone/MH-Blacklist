using TMPro;
using UnityEngine;

public class TranslateObj : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _RUSResult;
    [SerializeField] private string _ENGResult;

    public string Key => gameObject.name;
    public string RUS => _RUSResult;
    public string ENG => _ENGResult;

    public void ChangeText(string currentLanguage)
    {
        if (currentLanguage == "RUS")
            _text.text = RUS;
        if (currentLanguage == "ENG")
            _text.text = ENG;
    }
}
