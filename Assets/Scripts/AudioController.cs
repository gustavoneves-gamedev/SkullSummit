using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;

    [Header("Audio Objects")]
    public int currentMusicCode = 0;
    public AudioSource mySoundBox;
    public AudioSource myVFXBox;
    public AudioMixer myMixer;
    public AudioClip[] menuMusics;
    public AudioClip[] characterSelectionMusics;
    public AudioClip[] levelSelectionMusics;
    public AudioClip[] runMusics;

    [Header("VFX Objects")]
    public AudioClip[] menuSfxs;
    public AudioClip[] statisticsSfxs;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider SFXVolume;

    private bool isRunning;
    private float currentMasterVolume = 1f;
    private float currentMusicVolume = 0.5f;
    private float currentSFXVolume = 0.5f;

    private float timeToChangeMusic;
    private bool isPlayingRunMusic;

    //private void Awake()
    //{

    //    //ISTO NÃO ESTÁ FUNCIONANDO!!
    //    if (audioController == null)
    //    {
    //        audioController = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(gameObject);

    //}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioController = this;
        //mySoundBox = GetComponent<AudioSource>();
        masterVolume.value = currentMasterVolume;
        musicVolume.value = currentMusicVolume;
        SFXVolume.value = currentSFXVolume;
        Initialize();
    }

    private void Update() //MEGA PROVISÓRIO!!
    {
        //if (isPlayingRunMusic) timeToChangeMusic -= Time.deltaTime;

        //if (isPlayingRunMusic && timeToChangeMusic <= 7f)
        //{
        //    mySoundBox.clip = menuMusics[2];
        //    mySoundBox.pitch = 1.1f;
        //    mySoundBox.Play();
        //    mySoundBox.loop = true;
        //    isPlayingRunMusic = false;
        //}
    }

    public void Initialize()
    {
        ChangeMasterVolume(currentMasterVolume);
        ChangeMusicVolume(currentMusicVolume);
        ChangeSFXVolume(currentSFXVolume);
    }

    #region Volume Control

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

    #endregion

    public void SwitchMusicPlay(int musicGroup = 0, int music = 0)
    {
        //currentMusicCode = music;
        if (musicGroup == 0)
        {
            mySoundBox.clip = menuMusics[music];            
            mySoundBox.loop = true;
            mySoundBox.Play();
        }
        else if (musicGroup == 1)
        {
            mySoundBox.clip = characterSelectionMusics[music];
            //isPlayingRunMusic = false;
            mySoundBox.loop = true;
            mySoundBox.Play();
        }
        else if (musicGroup == 2)
        {
            mySoundBox.clip = levelSelectionMusics[music];
            //isPlayingRunMusic = false;
            mySoundBox.loop = true;
            mySoundBox.Play();
        }

        
    }

    public void SwitchVFXPlay(int VFXGroup = 0, int music = 0)
    {
        
        if (VFXGroup == 0) //Open
        {
            myVFXBox.clip = menuSfxs[music];
            //mySoundBox.loop = true;
            myVFXBox.loop = false;
            myVFXBox.Play();
        }
        else if (VFXGroup == 1)
        {
            if(music >= statisticsSfxs.Length) return;
            myVFXBox.clip = statisticsSfxs[music];            

            if(music >= 3) myVFXBox.loop = false;
            else myVFXBox.loop = true;

            myVFXBox.Play();
        }
        else if (VFXGroup == 2)
        {
            myVFXBox.clip = menuSfxs[music];
            //isPlayingRunMusic = false;
            //myVFXBox.loop = true;
            myVFXBox.Play();
        }
    }

    public void StopVFXPlay()
    {
        myVFXBox.loop = false;
        myVFXBox.Stop();
    }


}
