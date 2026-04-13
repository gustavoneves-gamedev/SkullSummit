using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinID = 0;
    private MeshRenderer meshRenderer;
    [SerializeField] private float normalRotateSpeed = 50f;
    [SerializeField] private float boostedRotateSpeed = 500f;
    private float rotationSpeed;
    private bool isEmiting;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem multiplierVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 30f);
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameController.playerPowers.isCoinMultiplierOn)
        {
            rotationSpeed = boostedRotateSpeed;
            if (!multiplierVFX.isEmitting) multiplierVFX.Play();
        }
        else
        {
            rotationSpeed = normalRotateSpeed;
            multiplierVFX.Stop();
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
            audioSource.Play();
            Destroy(gameObject, 2f);
        }
    }

}
