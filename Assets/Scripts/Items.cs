using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private int itemCode = 0;

    [SerializeField] private ParticleSystem VFX;
    [SerializeField] private AudioSource SFX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayFX()
    {
        VFX.Play();
        SFX.Play();
    }
}
