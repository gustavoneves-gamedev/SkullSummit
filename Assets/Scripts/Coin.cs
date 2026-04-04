using UnityEngine;
using UnityEngine.UIElements;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinID = 0;
    private MeshRenderer meshRenderer;
    [SerializeField] private float normalRotateSpeed = 50f;
    [SerializeField] private float boostedRotateSpeed = 500f;
    private float rotationSpeed;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,30f);
        meshRenderer = GetComponent<MeshRenderer>(); 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameController.playerPowers.isCoinMultiplierOn) rotationSpeed = boostedRotateSpeed;
        else rotationSpeed = normalRotateSpeed;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Revisar isto. Está diretamente no GameController, mas talvez eu deva puxar para
            //o Player ou para o Inventory - Ver com Professor
            if (coinID == 0) GameController.gameController.UpdateRunCoins(1, 0);
            if (coinID == 1) GameController.gameController.UpdateRunCoins(0, 1);

            meshRenderer.enabled = false;
            audioSource.Play();
            Destroy(gameObject, 2f);
        }
    }

}
