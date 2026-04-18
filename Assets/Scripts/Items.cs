using UnityEngine;

public class Items : MonoBehaviour
{
    

    [SerializeField] private ParticleSystem VFX;
    [SerializeField] private AudioSource SFX;

    
    public void PlayFX()
    {
        if (VFX != null) VFX.Play();
        if (SFX != null) SFX.Play();

        GetComponent<MeshRenderer>().enabled = false;

        Destroy(gameObject, 1f);
    }
}
