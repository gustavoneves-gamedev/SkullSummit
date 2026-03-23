using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    //public float playerSpeed = 10f;
    public float defaultBulletSpeed;
    public float bulletSpeed;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // rb.linearVelocity = transform.forward * bulletSpeed;

        //rb.linearVelocity = transform.forward * (bulletSpeed + defaultBulletSpeed) * Time.deltaTime;

       //rb.linearVelocity = GameController.gameController.playerRoot.gameObject.transform.forward
       //     * (playerSpeed + defaultBulletSpeed) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    
}
