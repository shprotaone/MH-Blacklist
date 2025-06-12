using Enums;
using UnityEngine;
using UnityEngine.UI;

public class RankDetailButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private RankType _rankType;

    public void Init()
    {
        _button.onClick.AddListener(ShowRankDetail);
        //TODO: заполнить картиночку
        //TODO: заполнить ранк
    }

    private void ShowRankDetail()
    {
        //TODO: получить список ресурсов
        //TODO: отобразить ресурсы
    }
}
