using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    //public float playerSpeed = 10f;
    //public float defaultBulletSpeed;
    public float bulletSpeed;
    private Rigidbody rb;

    [SerializeField] private float lifetime = 2f;
    private float timer;

    private void OnEnable()
    {
        timer = 0;
        if(rb == null) rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
       // Destroy(gameObject, 2f);
        
    } 

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
        Timer();
    }

    private void Timer()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= lifetime)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        timer = 0;
    }

}
