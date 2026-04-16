using UnityEngine;

public class Items : MonoBehaviour
{
    //[SerializeField] private int itemCode = 0;

    [SerializeField] private ParticleSystem VFX;
    [SerializeField] private AudioSource SFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayFX()
    {
        if (VFX != null) VFX.Play();
        if (SFX != null) SFX.Play();
    }
}
