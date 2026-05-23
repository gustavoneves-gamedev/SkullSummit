using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private float cameraSpeed = 7.5f;
    [SerializeField] private float specialCameraSpeed = 3f;
    private Quaternion defaultRotation;
    private bool hasCameraChanged;

    [Header("Camera Position")]
    [SerializeField] private float y = 19.01f;
    [SerializeField] private float z = 3.54f;
    private float x;

    private PlayerRoot player;
    private Transform targetToFollow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Invoke("InitializeCamera", .2f);
    }

    private void InitializeCamera()
    {
        player = GameController.gameController.playerRoot;
        targetToFollow = player.transform;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || player == null) return;

        //currentPosition = defaultPosition;
        if (GameController.gameController.isRunning) SpecialCameraTransition();

        x = Mathf.Lerp(transform.position.x,targetToFollow.position.x + positionOffset.x,cameraSpeed * Time.deltaTime);
        

        //transform.position = new Vector3(x, transform.position.y, targetToFollow.position.z + positionOffset.z);
        
        transform.position = new Vector3(x, targetToFollow.position.y + y, 
            targetToFollow.position.z + z);
    }

    private void SpecialCameraTransition()
    {
        if (player.playerPowers.isSpecialOn)
        {
            //positionOffset.y += 4f;
            y = Mathf.Lerp(y, 22f, specialCameraSpeed * Time.deltaTime);

            //positionOffset.z -= 3f;
            z = Mathf.Lerp(z, .5f, specialCameraSpeed * Time.deltaTime);
            //currentPosition = Vector3.MoveTowards(transform.position, specialDestination, cameraSpeed);
        }
        else
        {
            //positionOffset.y -= 4f;
            y = Mathf.Lerp(y, 19.01f, specialCameraSpeed * Time.deltaTime);
            //positionOffset.z += 3f;
            z = Mathf.Lerp(z, 3.54f, specialCameraSpeed * Time.deltaTime);
            //currentPosition = Vector3.MoveTowards(transform.position, defaultPosition, cameraSpeed);
        }

        //transform.position = currentPosition;
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
