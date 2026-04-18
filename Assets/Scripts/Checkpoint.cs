using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool wasCrossed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wasCrossed)
        {
            transform.localScale -= Vector3.one * 2f * Time.deltaTime;

            if(transform.localScale.x <= .25f) wasCrossed = false;
        }
    }
}
