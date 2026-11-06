using UnityEngine;
using UnityEngine.UI;

public class MusicManager:MonoBehaviour{
    static private MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    [SerializeField] private Slider slider;
    private void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        if(backgroundMusic!=null){
            PlayBackgroundMusic(false,backgroundMusic);
        }
        slider.onValueChanged.AddListener(delegate{SetVolume(slider.value);});
    }
    public void SetVolume(float volume){
        Instance.audioSource.volume=volume;
    }
    
    private void PlayBackgroundMusic(bool resetSong,AudioClip backgroundMusic=null){
        if(backgroundMusic!=null){
            audioSource.clip=backgroundMusic;
        }
        if(audioSource.clip!=null){
            if(resetSong){
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
    private void PauseBackgroundMusic(){
        audioSource.Pause();
    }
}
