using System;
using View.Curtain;

namespace Systems.UI
{
    public class CurtainSystem
    {
        public event Action OnFullCurtain;
        private CurtainView _curtainView;

        public void Initialize(CurtainView curtain)
        {
            _curtainView = curtain;
        }

        public void Show()
        {
            _curtainView.gameObject.SetActive(true);
            OnFullCurtain?.Invoke();
        }

        public void Hide()
        {
            _curtainView.gameObject.SetActive(false);
        }

    }
}