using Cysharp.Threading.Tasks;

public class UIFactory
{
    private AssetProvider _assetProvider;
    private SettingsView _settingsView;

    public async UniTask Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var go = await _assetProvider.GetPrefabAsync("SettingsPanel");
        _settingsView = go.GetComponent<SettingsView>();
    }

    public SettingsView GetSettingsView()
    {
        return _settingsView;
    }
}
