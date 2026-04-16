using UnityEngine;

public class Items : MonoBehaviour
{
    //[SerializeField] private int itemCode = 0;
    //private ParticleSystem defaultVFX;

    [SerializeField] private ParticleSystem VFX;
    [SerializeField] private AudioSource SFX;

    private void Start()
    {
        //defaultVFX = GetComponent<ParticleSystem>();
        //defaultVFX.Play();
    }
    public void PlayFX()
    {
        if (VFX != null) VFX.Play();
        if (SFX != null) SFX.Play();
    }
}
