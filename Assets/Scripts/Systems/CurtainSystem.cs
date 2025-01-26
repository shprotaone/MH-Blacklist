using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurtainSystem : MonoBehaviour
{
    public event Action OnFullCurtain;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Image _curtainImage;

    public IEnumerator Show()
    {
        ShowStaticCurtain();
        OnFullCurtain?.Invoke();
        
        yield break;
    }

    private void ShowStaticCurtain()
    {
        _curtainImage.gameObject.SetActive(true);
    }

    private void PlayParticle()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void Hide()
    {
        _curtainImage.gameObject.SetActive(false);
        // _particleSystem.Stop();
        // _particleSystem.gameObject.SetActive(false);
    }
}