using System;
using System.Collections;
using UnityEngine;

public class CurtainSystem : MonoBehaviour
{
    public event Action OnFullCurtain;
    [SerializeField] private ParticleSystem _particleSystem;

    public IEnumerator Show()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        while (_particleSystem.time < 0.2f)
        {
            yield return new WaitForSeconds(0.05f);
        }
        
        OnFullCurtain?.Invoke();
        
        yield break;
    }

    public void Hide()
    {
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}