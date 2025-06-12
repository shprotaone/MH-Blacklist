using Data.JSON;
using UnityEngine;

public class AttackFieldView : MonoBehaviour
{
    public void Fill(MonsterModel model)
    {
        Debug.Log("Attack" + model.attackTypes.Length);
    }
}
