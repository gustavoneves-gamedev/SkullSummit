using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float playerSpeed = 10f;
    [SerializeField] private float defaultBulletSpeed;
    private float bulletSpeed;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletSpeed = defaultBulletSpeed + playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = Vector3.forward * (bulletSpeed + defaultBulletSpeed) * Time.deltaTime;

        //rb.linearVelocity = transform.forward * (bulletSpeed + defaultBulletSpeed) * Time.deltaTime;

       rb.linearVelocity = GameController.gameController.playerRoot.gameObject.transform.forward
            * (bulletSpeed + defaultBulletSpeed) * Time.deltaTime;
    }
}
