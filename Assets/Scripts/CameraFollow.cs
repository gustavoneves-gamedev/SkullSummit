using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 positionOffset;
    private Transform targetToFollow;
    [SerializeField] private float cameraSpeed = 10f;
    private Quaternion defaultRotation;
    private bool hasCameraChanged;
    private PlayerRoot player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Quaternion: " + transform.rotation);
        player = GameController.gameController.playerRoot;
        targetToFollow = player.transform;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        
        //if(transform == null) return;

        float x = Mathf.Lerp(transform.position.x,targetToFollow.position.x + positionOffset.x, cameraSpeed * Time.deltaTime);
        
        transform.position = new Vector3(x, transform.position.y, targetToFollow.position.z + positionOffset.z);
    }

    public void ChangeCamera()
    {
        if (!hasCameraChanged)
        {
            transform.rotation = new Quaternion(0.56763f, 0.11765f, 0.08238f, 0.81066f);
            positionOffset.x = -4.44f;
            //0.56763f, 0.11765f, 0.08238f, 0.81066
            hasCameraChanged = true;
        }
        else
        {
            transform.rotation = defaultRotation;
            positionOffset.x = 0f;
            hasCameraChanged = false;
        }

    }

}
