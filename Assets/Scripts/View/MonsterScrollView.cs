using UnityEngine;

public class MonsterScrollView : MonoBehaviour
{
    [SerializeField] private Transform _contentContainer;

    public Transform ContentContainer => _contentContainer;

    public void Clear()
    {
        for (int i = 0; i < _contentContainer.childCount; i++)
        {
            Destroy(_contentContainer.GetChild(i).gameObject);
        }
    }
}
