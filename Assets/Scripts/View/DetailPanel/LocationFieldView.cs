using Data.JSON;
using UnityEngine;

namespace View.DetailPanel
{
    public class LocationFieldView : MonoBehaviour
    {
        public void Fill(MonsterModel model)
        {
            Debug.Log("Location " + model.locations.Length);
        }
    }
}