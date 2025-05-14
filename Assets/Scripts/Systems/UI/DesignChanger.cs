using System.Collections.Generic;
using Enums;
using UnityEngine;
using View.UI;

namespace Systems.UI
{
   public class DesignChanger : MonoBehaviour
   {
      [SerializeField] private List<DesignElement> _elements;

      private AssetProvider _assetProvider;

      public void Initialize(AssetProvider assetProvider)
      {
         _assetProvider = assetProvider;
      }

      public void ChangeStyle(StyleType style)
      {
         foreach (var element in _elements)
         {
            if (element.Type == DesignElementType.BACKGROUND)
            {
               element.ChangeSprite(_assetProvider.GetBackground(style));
            }
            else if(element.Type == DesignElementType.SLIDER_FRAME)
            {
               element.ChangeSprite(_assetProvider.GetSliderFrame(style));
            }
            else if (element.Type == DesignElementType.SLIDER_FILL_BACKGROUND)
            {
               element.ChangeSprite(_assetProvider.GetSliderFill(style));
            }
            else if (element.Type == DesignElementType.BORDER_UP)
            {
               element.ChangeSprite(_assetProvider.GetBorderUp(style));
            }
            else if (element.Type == DesignElementType.BORDER_DOWN)
            {
               element.ChangeSprite(_assetProvider.GetBorderDown(style));
            }
            else if (element.Type == DesignElementType.SETTINGS_BACKGROUND)
            {
               element.ChangeSprite(_assetProvider.GetSettingsBackground(style));   
               if(style == StyleType.WORLD) element.SetColor(Color.white);
            }
            else if (element.Type == DesignElementType.OPTION_BUTTON)
            {
               element.ChangeSprite(_assetProvider.GetSettingsIcon(style));
            }
         }
      }
   }
}