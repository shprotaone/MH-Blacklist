using Data;
using Enums;
using View.Settings;

namespace Systems
{
    public class SettingsController
    {
        private MonsterListChanger _monsterListChanger;
        private LanguageProvider _languageProvider;
        private SettingsView _settingsView;
        private SaveLoadSystem _saveLoadSystem;
        private GlobalSystems _globalSystems;
        private UIController _uiController;

        public void Initialize(GlobalSystems globalSystems,UIController uiController)
        {
            _monsterListChanger = globalSystems.MonsterListChanger;
            _languageProvider = globalSystems.LanguageProvider;
            _saveLoadSystem = globalSystems.SaveLoadSystem;
            _uiController = uiController;
            _globalSystems = globalSystems;
        }

        public void BindButtons()
        {
            _settingsView = _uiController.SettingsView;
            _settingsView.EngButton.onClick.AddListener(SetEng);
            _settingsView.RusButton.onClick.AddListener(SetRus);
            _settingsView.ResetProgressButton.onClick.AddListener(ResetProgress);
            _settingsView.IncreaseCellSizeButton.onClick.AddListener(IncreaseSizeCell);
            _settingsView.DecreaseCellSizeButton.onClick.AddListener(DecreaseSizeCell);

            _settingsView.WorldGameButton.Button.onClick.AddListener(_monsterListChanger.CreateWorldList);
            _settingsView.RiseGameButton.Button.onClick.AddListener(_monsterListChanger.CreateRiseList);
            _settingsView.WildsGameButton.Button.onClick.AddListener(_monsterListChanger.CreateWildsList);

            _settingsView.CloseButton.onClick.AddListener(() => _uiController.CallSettings(false));
        }

        public void SetScaledButton(StyleType styleType)
        {
            if (styleType == StyleType.RISE)
            {
                _settingsView.WildsGameButton.SetScaled(false);
                _settingsView.WorldGameButton.SetScaled(false);
                _settingsView.RiseGameButton.SetScaled(true);
            }
            else if (styleType == StyleType.WORLD)
            {
                _settingsView.RiseGameButton.SetScaled(false);
                _settingsView.WildsGameButton.SetScaled(false);
                _settingsView.WorldGameButton.SetScaled(true);
            }
            else if (styleType == StyleType.WILDS)
            {
                _settingsView.WorldGameButton.SetScaled(false);
                _settingsView.RiseGameButton.SetScaled(false);
                _settingsView.WildsGameButton.SetScaled(true);
            }
        }

        private void ChangeValueText(string valText)
        {
            _settingsView.SizeField.text = valText;
        }

        private void DecreaseSizeCell()
        {
            _monsterListChanger.DecreaseCellSize();
            ChangeValueText(_monsterListChanger.CellSize.ToString());
        }

        private void IncreaseSizeCell()
        {
            _monsterListChanger.IncreaseCellSize();
            ChangeValueText(_monsterListChanger.CellSize.ToString());
        }

        private void ResetProgress()
        {
            _saveLoadSystem.Delete(StaticData.PlayerSettingPath);
        }

        private void SetRus()
        {
            _languageProvider.SetLanguage(StaticData.RUSLanguageName);
            _globalSystems.PlayerDataParser.SaveAppData(_languageProvider.GetLanguageString());
            _globalSystems.CurtainSystem.Show();
        }

        private void SetEng()
        {
            _languageProvider.SetLanguage(StaticData.ENGLanguageName);
            _globalSystems.PlayerDataParser.SaveAppData(_languageProvider.GetLanguageString());
            _globalSystems.CurtainSystem.Show();
        }
    }
}
