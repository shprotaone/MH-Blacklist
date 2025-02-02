using Cysharp.Threading.Tasks;

public class UIFactory
{
    private AssetProvider _assetProvider;
    private MonsterScrollView _monsterScrollView;
    private DetailedView _detailedView;
    private SettingsView _settingsView;

    public async UniTask Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var go = await _assetProvider.GetPrefabAsync("SettingsPanel");
        _settingsView = go.GetComponent<SettingsView>();

        go = await _assetProvider.GetPrefabAsync("DetailedPanel");
        _detailedView = go.GetComponent<DetailedView>();

        go = await _assetProvider.GetPrefabAsync("MonsterScrollView");
        _monsterScrollView = go.GetComponent<MonsterScrollView>();
    }

    public SettingsView GetSettingsView()
    {
        return _settingsView;
    }

    public DetailedView GetDetailedView()
    {
        return _detailedView;
    }

    public MonsterScrollView GetScrollView()
    {
        return _monsterScrollView;
    }
}
