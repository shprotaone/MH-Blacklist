using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _engButton;
    [SerializeField] private Button _rusButton;
    [SerializeField] private Button _increaseCellSizeButton;
    [SerializeField] private Button _decreaseCellSizeButton;
    [SerializeField] private Button _resetProgressButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_InputField _sizeField;

    public Button EngButton => _engButton;
    public Button RusButton => _rusButton;
    public Button IncreaseCellSizeButton => _increaseCellSizeButton;
    public Button DecreaseCellSizeButton => _decreaseCellSizeButton;
    public Button ResetProgressButton => _resetProgressButton;
    public Button CloseButton => _closeButton;
    public TMP_InputField SizeField => _sizeField;

}
