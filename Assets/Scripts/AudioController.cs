using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;

    [Header("Audio Objects")]
    public AudioSource mySoundBox;
    public AudioMixer myMixer;
    public AudioClip[] musics;
    //0 - MenuCowboy
    //1 - Cowboy Level part 1
    //2 - Cowboy Level part 2

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider SFXVolume;

    private bool isRunning;
    private float currentMasterVolume = 1f;
    private float currentMusicVolume = 0.5f;
    private float currentSFXVolume = 0.5f;

    [SerializeField] private float timeToChangeMusic;
    private bool isPlayingRunMusic;

    private void Awake()
    {

        //ISTO NÃO ESTÁ FUNCIONANDO!!
        //if (audioController == null)
        //{
        //    audioController = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioController = this;
        mySoundBox = GetComponent<AudioSource>();
        masterVolume.value = currentMasterVolume;
        musicVolume.value = currentMusicVolume;
        SFXVolume.value = currentSFXVolume;
        Initialize();
    }

    private void Update() //MEGA PROVISÓRIO!!
    {
        if (isPlayingRunMusic) timeToChangeMusic -= Time.deltaTime;

        if (isPlayingRunMusic && timeToChangeMusic <= 7f)
        {
            mySoundBox.clip = musics[2];
            mySoundBox.pitch = 1.1f;
            mySoundBox.Play();
            mySoundBox.loop = true;
            isPlayingRunMusic = false;
        }
    }

    public void Initialize()
    {
        ChangeMasterVolume(currentMasterVolume);
        ChangeMusicVolume(currentMusicVolume);
        ChangeSFXVolume(currentSFXVolume);
    }

    private float LinearToDb(float linear)
    {
        // Evita -Infinity quando linear = 0
        if (linear <= 0.0001f)
            return -80f; // praticamente mudo no mixer

        return Mathf.Log10(linear) * 20f; // 1 → 0 dB, 0.5 → ~-6 dB, etc.
    }

    public void ToggleMute()
    {
        mySoundBox.mute = !mySoundBox.mute;
    }

    public void ChangeMasterVolume(float value)
    {
        currentMasterVolume = value;
        myMixer.SetFloat("MasterVolume", LinearToDb(value));

    }

    public void ChangeMusicVolume(float value)
    {
        currentMusicVolume = value;
        myMixer.SetFloat("MusicVolume", LinearToDb(value));
    }

    public void ChangeSFXVolume(float value)
    {
        currentSFXVolume = value;
        myMixer.SetFloat("SFXVolume", LinearToDb(value));
    }

    public void SwitchMusic(int music)
    {

        if (music == 0)
        {
            mySoundBox.clip = musics[0];
            isPlayingRunMusic = false;
            mySoundBox.loop = true;
            mySoundBox.Play();
        }
        if (music == 1)
        {
            mySoundBox.clip = musics[1];
            timeToChangeMusic = mySoundBox.clip.length;
            isPlayingRunMusic = true;
            mySoundBox.loop = false;
            mySoundBox.Play();
        }

        //mySoundBox.loop = true;
        //mySoundBox.Play();
    }

}
