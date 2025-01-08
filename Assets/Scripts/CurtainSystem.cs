using System.Collections;
using UnityEngine;

public class CurtainSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    public IEnumerator Show()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
        
        yield break;
    }

    public void Hide()
    {
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}