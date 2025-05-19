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

        public void Initialize(UIController uiController,LanguageProvider languageProvider
            ,SaveLoadSystem saveLoadSystem,MonsterListChanger monsterListChanger)
        {
            _settingsView = uiController.SettingsView;
            _monsterListChanger = monsterListChanger;
            _languageProvider = languageProvider;
            _saveLoadSystem = saveLoadSystem;

            _settingsView.EngButton.onClick.AddListener(SetEng);
            _settingsView.RusButton.onClick.AddListener(SetRus);
            _settingsView.ResetProgressButton.onClick.AddListener(ResetProgress);
            _settingsView.IncreaseCellSizeButton.onClick.AddListener(IncreaseSizeCell);
            _settingsView.DecreaseCellSizeButton.onClick.AddListener(DecreaseSizeCell);

            _settingsView.WorldGameButton.Button.onClick.AddListener(monsterListChanger.CreateWorldList);
            _settingsView.RiseGameButton.Button.onClick.AddListener(monsterListChanger.CreateRiseList);
            _settingsView.WildsGameButton.Button.onClick.AddListener(monsterListChanger.CreateWildsList);

            _settingsView.CloseButton.onClick.AddListener(() => uiController.CallSettings(false));
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
        }

        private void SetEng()
        {
            _languageProvider.SetLanguage(StaticData.ENGLanguageName);
        }
    }
}
