using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinID = 0;
    private MeshRenderer meshRenderer;
    [SerializeField] private float normalRotateSpeed = 50f;
    [SerializeField] private float boostedRotateSpeed = 500f;
    private float rotationSpeed;
    [SerializeField] private GameObject coinAura;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem multiplierVFX;
    [SerializeField] private ParticleSystem collectVFX;
    private Quaternion defaultRotation = Quaternion.identity;

    
    private void OnEnable()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        meshRenderer.enabled = true;

        if (defaultRotation == Quaternion.identity) defaultRotation = transform.rotation;
        else transform.rotation = defaultRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameController.isRunning) return;

        if (GameController.gameController.playerPowers.isCoinMultiplierOn)
        {
            if (meshRenderer.enabled == false) return;

            rotationSpeed = boostedRotateSpeed;
            if (!multiplierVFX.isEmitting) multiplierVFX.Play();

            if (meshRenderer.enabled == true) coinAura.SetActive(true);
            else coinAura.SetActive(false);

        }
        else
        {
            rotationSpeed = normalRotateSpeed;
            multiplierVFX.Stop();
            coinAura.SetActive(false);
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (coinID == 0) GameController.gameController.UpdateRunCoins(1, 0);
            if (coinID == 1) GameController.gameController.UpdateRunCoins(0, 1);

            meshRenderer.enabled = false;
            coinAura.SetActive(false);
            audioSource.Play();
            collectVFX.Play();
            //Destroy(gameObject, 2f);
        }
    }

}
