using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private ButtonBehaviour _riseButton;
    [SerializeField] private ButtonBehaviour _worldButton;
    [SerializeField] private ButtonBehaviour _wildsButton;
    [SerializeField] private Button _engButton;
    [SerializeField] private Button _rusButton;
    [SerializeField] private Button _increaseCellSizeButton;
    [SerializeField] private Button _decreaseCellSizeButton;
    [SerializeField] private Button _resetProgressButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_InputField _sizeField;

    private SettingsController _settingsController;

    public ButtonBehaviour RiseButton => _riseButton;
    public ButtonBehaviour WorldButton => _worldButton;
    public ButtonBehaviour WildsButton => _wildsButton;
    public Button EngButton => _engButton;
    public Button RusButton => _rusButton;
    public Button IncreaseCellSizeButton => _increaseCellSizeButton;
    public Button DecreaseCellSizeButton => _decreaseCellSizeButton;
    public Button ResetProgressButton => _resetProgressButton;
    public Button CloseButton => _closeButton;
    public SettingsController Controller => _settingsController;
    public TMP_InputField SizeField => _sizeField;

    public void Initialize(SettingsController settingsController)
    {
        _settingsController = settingsController;
    }

}
