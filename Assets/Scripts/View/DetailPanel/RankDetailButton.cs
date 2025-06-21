using Data.JSON;
using Enums;
using Systems;
using UnityEngine;
using UnityEngine.UI;
using View.DetailPanel;

public class RankDetailButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private RankType _rankType;
    private ResourcesView _resourcesView;
    private MonsterModel _monsterModel;

    public void Init(RankType rankType, MonsterModel monsterModel, ResourcesView resourcesView)
    {
        gameObject.SetActive(true);
        _monsterModel = monsterModel;
        _resourcesView = resourcesView;
        _rankType = rankType;
        _button.onClick.AddListener(ShowRankDetail);
        _image.sprite = GlobalSystems.Instance.GetSprite(rankType);
    }

    private void ShowRankDetail()
    {
        _resourcesView.Fill(GlobalSystems.Instance.MosterResourcesParser.GetResourcesWithOtherRank(_monsterModel,_rankType));
    }
}