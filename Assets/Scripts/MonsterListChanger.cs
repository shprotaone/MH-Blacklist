using System.Collections;
using Data;
using DefaultNamespace;
using UnityEngine;

public class MonsterListChanger : MonoBehaviour
{
    private UIController _uiController;
    private MonsterTierList _tierList;
    private CellFactory _cellFactory;
    private FindSystem _findSystem;
    private DesignChanger _designChanger;
    private ProgressSeeker _progressSeeker;
    private CurtainSystem _curtainSystem;
    private GlobalSystems _globalSystems;

    private StyleType _currentStyle;
    private Monsters _currentMonsterList;
    private Monsters _riseMonsters;
    private Monsters _worldMonsters;

    public void Initialize(UIController uiController,CellFactory cellFactory,FindSystem findSystem,
        DesignChanger designChanger,ProgressSeeker progressSeeker,CurtainSystem curtainSystem
        ,GlobalSystems globalSystems,MonsterTierList monsterTierList)
    {
        _uiController = uiController;
        _tierList = monsterTierList;
        _cellFactory = cellFactory;
        _findSystem = findSystem;
        _designChanger = designChanger;
        _progressSeeker = progressSeeker;
        _curtainSystem = curtainSystem;
        _globalSystems = globalSystems;
    }
    
    public void CreateRiseList()
    {
        _currentStyle = StyleType.RISE;
        _currentMonsterList = _riseMonsters;
        _uiController.SetScaledButton(StyleType.RISE);
        _uiController.ClearScrollList();
        StartCoroutine(ShowMonsters(_riseMonsters, StyleType.RISE));
    }

    public void CreateWorldList()
    {
        _currentStyle = StyleType.WORLD;
        _currentMonsterList = _worldMonsters;

        _uiController.SetScaledButton(StyleType.WORLD);
        _uiController.ClearScrollList();
        StartCoroutine(ShowMonsters(_worldMonsters,StyleType.WORLD));
    }
    
    private IEnumerator ShowMonsters(Monsters monsters,StyleType style)
    {
        _tierList.CreateLists(monsters,style);

        _cellFactory.CreateCells(_tierList.GetLowRankList());
        _cellFactory.CreateCells(_tierList.GetMasterRankList());
        _cellFactory.CreateCells(_tierList.GetTemperedlist());

        yield return new WaitForSeconds(1f);
        _findSystem.SetList();
        _globalSystems.ChangeStyle();
        _designChanger.ChangeStyle(style);

        _progressSeeker.Initialize(_uiController.MonsterScrollView.ContentContainer);
        _progressSeeker.UpdateSlider();

        _curtainSystem.Hide();
        yield break;
    }

    public void LoadMonsters()
    {
        StartCoroutine(ShowRoutine());
        Debug.Log("Try Load monsters");
    }

    private IEnumerator ShowRoutine()
    {
        yield return _curtainSystem.Show();
        yield return ShowMonsters(_currentMonsterList, _currentStyle);
    }

    public void SetCurrentMonsterList(StyleType styleType)
    {
        _currentStyle = styleType;
        if (styleType == StyleType.RISE) 
            _currentMonsterList = _riseMonsters;
        else if (styleType == StyleType.WORLD) 
            _currentMonsterList = _worldMonsters;
        
        _tierList.CreateLists(_currentMonsterList, _currentStyle);
    }

    public void SetMonsterList(Monsters monsters, StyleType type)
    {
        if (type == StyleType.RISE)
        {
            _riseMonsters = monsters;
        }

        if (type == StyleType.WORLD)
        {
            _worldMonsters = monsters;
        }

        if (type == StyleType.WORLD)
        {
            
        }
    }
}