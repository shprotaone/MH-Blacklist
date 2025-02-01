using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIFactory
{
    private AssetProvider _assetProvider;
    private DetailedView _detailedView;
    private SettingsView _settingsView;

    public async UniTask Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var go = await _assetProvider.GetPrefabAsync("SettingsPanel");
        _settingsView = go.GetComponent<SettingsView>();

        go = await _assetProvider.GetPrefabAsync("DetailedPanel");
        _detailedView = go.GetComponent<DetailedView>();
    }

    public SettingsView GetSettingsView()
    {
        return _settingsView;
    }

    public DetailedView GetDetailedView()
    {
        return _detailedView;
    }
}
