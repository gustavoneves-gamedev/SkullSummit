using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;
    public AudioSource mySoundBox;
    public AudioMixer myMixer;
    public AudioClip[] musics;

    private float currentMasterVolume = 0.5f;
    private float currentMusicVolume = 0.5f;
    private float currentSFXVolume = 0.5f;

    private void Awake()
    {
        if (audioController == null)
        {
            audioController = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySoundBox = GetComponent<AudioSource>();
        Initialize();
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

    public void SwitchMusic(string music)
    {
        switch (music)
        {
            case "Menu":
                mySoundBox.clip = musics[0];
                break;
            case "Jogo":
                mySoundBox.clip = musics[1];
                break;
            default:
                break;
        }
        mySoundBox.loop = true;
        mySoundBox.Play();
    }

}
