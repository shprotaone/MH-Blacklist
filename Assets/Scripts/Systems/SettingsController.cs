using System;
using Data;
using Systems;

public class SettingsController
{
    private MonsterScrollView _monsterScrollView;
    private LanguageProvider _languageProvider;
    private SettingsView _settingsView;
    private SaveLoadSystem _saveLoadSystem;

    public void Initialize(UIController uiController,LanguageProvider languageProvider
    ,SaveLoadSystem saveLoadSystem,MonsterListChanger monsterListChanger)
    {
        _settingsView = uiController.SettingsView;
        _monsterScrollView = uiController.MonsterScrollView;
        _languageProvider = languageProvider;
        _saveLoadSystem = saveLoadSystem;

        _settingsView.EngButton.onClick.AddListener(SetEng);
        _settingsView.RusButton.onClick.AddListener(SetRus);
        _settingsView.ResetProgressButton.onClick.AddListener(ResetProgress);
        _settingsView.IncreaseCellSizeButton.onClick.AddListener(IncreaseSizeCell);
        _settingsView.DecreaseCellSizeButton.onClick.AddListener(DecreaseSizeCell);

        _settingsView.WorldButton.Button.onClick.AddListener(monsterListChanger.CreateWorldList);
        _settingsView.RiseButton.Button.onClick.AddListener(monsterListChanger.CreateRiseList);

        _settingsView.CloseButton.onClick.AddListener(() => uiController.CallSettings(false));
        _monsterScrollView.OnChangeSize += ChangeValueText;
    }

    public void SetScaledButton(StyleType styleType)
    {
        if (styleType == StyleType.RISE)
        {
            _settingsView.WorldButton.SetMainScaled(false);
            _settingsView.WorldButton.SetScaled(false);
            _settingsView.RiseButton.SetMainScaled(true);
            _settingsView.RiseButton.SetScaled(true);
        }
        else if (styleType == StyleType.WORLD)
        {
            _settingsView.WorldButton.SetMainScaled(true);
            _settingsView.WorldButton.SetScaled(true);
            _settingsView.RiseButton.SetMainScaled(false);
            _settingsView.RiseButton.SetScaled(false);
        }
    }

    private void ChangeValueText(string valText)
    {
        _settingsView.SizeField.text = valText;
    }

    private void DecreaseSizeCell()
    {
        _monsterScrollView.DecreaseCellSize();
    }

    private void IncreaseSizeCell()
    {
        _monsterScrollView.IncreaseCellSize();
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
