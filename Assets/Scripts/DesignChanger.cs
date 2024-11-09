using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class DesignChanger : MonoBehaviour
{
   [SerializeField] private List<DesignElement> _elememts;

   private AssetProvider _assetProvider;

   public void Initialize()
   {
      _elememts = GetComponentsInChildren<DesignElement>().ToList();
   }

   public void ChangeStyle(StyleType style)
   {
      //TODO: Get from assetProvider
      //TODO: Set All designElement
   }
}