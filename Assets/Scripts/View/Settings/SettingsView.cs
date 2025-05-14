using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [FormerlySerializedAs("_riseButton")] [SerializeField] private GameButtonSizeController riseGameButton;
        [FormerlySerializedAs("_worldButton")] [SerializeField] private GameButtonSizeController worldGameButton;
        [FormerlySerializedAs("_wildsButton")] [SerializeField] private GameButtonSizeController wildsGameButton;
        [SerializeField] private Button _engButton;
        [SerializeField] private Button _rusButton;
        [SerializeField] private Button _increaseCellSizeButton;
        [SerializeField] private Button _decreaseCellSizeButton;
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_InputField _sizeField;

        private SettingsController _settingsController;

        public GameButtonSizeController RiseGameButton => riseGameButton;
        public GameButtonSizeController WorldGameButton => worldGameButton;
        public GameButtonSizeController WildsGameButton => wildsGameButton;
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
}
