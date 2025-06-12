using System.Collections;
using Cysharp.Threading.Tasks;
using Parser;
using Systems;
using Systems.Factory;
using Systems.UI;
using UnityEngine;
using View.Curtain;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AssetProvider _assetProvider;
    [SerializeField] private FindSystem _findSystem;
    [SerializeField] private UIController _uiController;

    [SerializeField] private DesignChanger _designChanger;
    [SerializeField] private CellFactory _cellFactory;

    private CurtainSystem _curtainSystem;
    private MonsterResourcesParser _monsterResourcesParser;
    private PlayerDataParser _playerDataParser;
    private MonsterListChanger _monsterListChanger;

    private async void Start()
    {
        GlobalSystems.Instance.Initialize(_cellFactory, _findSystem, _designChanger,_assetProvider, _uiController);
        _curtainSystem = GlobalSystems.Instance.CurtainSystem;
        await InitCurtain();
        _monsterResourcesParser = GlobalSystems.Instance.MosterResourcesParser;
        _playerDataParser = GlobalSystems.Instance.PlayerDataParser;
        _monsterListChanger = GlobalSystems.Instance.MonsterListChanger;

        await _uiController.Initialize(_assetProvider, GlobalSystems.Instance);
        GlobalSystems.Instance.SettingsController.BindButtons();
        
        await _cellFactory.Initialize(_assetProvider, GlobalSystems.Instance, _uiController);
        await Loading();
        _designChanger.Initialize(_assetProvider);
        Application.targetFrameRate = 60;
    }

    private async UniTask InitCurtain()
    {
        var go = await _assetProvider.GetPrefabAsync("LoadingCurtain");
        var instance = Instantiate(go, _uiController.CurtainPanel, false);
        _curtainSystem.Initialize(instance.GetComponent<CurtainView>());
    }

    private IEnumerator Loading()
    {
        _monsterResourcesParser.Initialize(_assetProvider);
        GlobalSystems.Instance.SetLanguage(_playerDataParser.AppData.lastLang);
        
        _monsterListChanger.SetMonsterList();

        _findSystem.Initialize(GlobalSystems.Instance);
        GlobalSystems.Instance.SetMonsterList();
        _monsterListChanger.ShowMonsters();
        yield break;

    }
}