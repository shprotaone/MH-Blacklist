using System;
using Systems;

public class SettingsController
{
    private MonsterScrollView _monsterScrollView;
    private LanguageProvider _languageProvider;
    private SettingsView _settingsView;
    private SaveLoadSystem _saveLoadSystem;

    public void Initialize(SettingsView settingsView, MonsterScrollView monsterScrollView,LanguageProvider languageProvider
    ,SaveLoadSystem saveLoadSystem)
    {
        _settingsView = settingsView;
        _monsterScrollView = monsterScrollView;
        _languageProvider = languageProvider;
        _saveLoadSystem = saveLoadSystem;

        settingsView.EngButton.onClick.AddListener(SetEng);
        settingsView.RusButton.onClick.AddListener(SetRus);
        settingsView.ResetProgressButton.onClick.AddListener(ResetProgress);
        settingsView.IncreaseCellSizeButton.onClick.AddListener(IncreaseSizeCell);
        settingsView.DecreaseCellSizeButton.onClick.AddListener(DecreaseSizeCell);
        _monsterScrollView.OnChangeSize += ChangeValueText;
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
