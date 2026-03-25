using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    //public float playerSpeed = 10f;
    //public float defaultBulletSpeed;
    public float bulletSpeed;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
        
    } 

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    
}
