using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private bool shouldMove;

    private RectTransform rect;
    private Vector2 originalPosition;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.position;
    }

    private void OnEnable()
    {
        AudioController.audioController.SwitchMusicPlay(0, 1);
        shouldMove = true;
    }

    private void OnDisable()
    {
        AudioController.audioController.SwitchMusicPlay();
        shouldMove = false;
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldMove) return;

        Movement();
    }

    private void Movement()
    {
        rect.position += Vector3.up * speed * Time.deltaTime;
    }

    private void ResetPosition()
    {
        rect.position = originalPosition;
    }

}
