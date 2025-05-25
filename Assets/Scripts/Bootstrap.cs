using System.Collections;
using Cysharp.Threading.Tasks;
using Data;
using Data.JSON;
using Enums;
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

    private GlobalSystems _globalSystems;
    private CurtainSystem _curtainSystem;
    private MonsterResourcesParser _monsterResourcesParser;
    private PlayerDataParser _playerDataParser;
    private SaveLoadSystem _saveLoadSystem;
    private MonsterListChanger _monsterListChanger;

    private async void Start()
    {
        _globalSystems = new GlobalSystems(_cellFactory, _findSystem, _designChanger);
        _globalSystems.Initialize(_assetProvider, _uiController);
        _curtainSystem = _globalSystems.CurtainSystem;
        await InitCurtain();
        _monsterResourcesParser = _globalSystems.MosterResourcesParser;
        _playerDataParser = _globalSystems.PlayerDataParser;
        _saveLoadSystem = _globalSystems.SaveLoadSystem;
        _monsterListChanger = _globalSystems.MonsterListChanger;

        await _uiController.Initialize(_assetProvider, _globalSystems);
        _globalSystems.SettingsController.BindButtons();
        
        await _cellFactory.Initialize(_assetProvider, _globalSystems, _uiController);
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
        _globalSystems.SetLanguage(_playerDataParser.AppData.lastLang);
        
        _monsterListChanger.SetMonsterList();

        _findSystem.Initialize(_globalSystems, _uiController);
        _globalSystems.SetMonsterList();
        _monsterListChanger.ShowMonsters();
        yield break;

    }
}