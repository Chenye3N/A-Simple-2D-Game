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
        audioSource=GetComponent<AudioSource>();
        if(backgroundMusic!=null){
            PlayBackgroundMusic(false,backgroundMusic);
        }
        slider.onValueChanged.AddListener(delegate{SetVolume(slider.value);});
    }
    public void SetVolume(float volume){
        Instance.audioSource.volume=volume;
    }
    
    public static void PlayBackgroundMusic(bool resetSong,AudioClip backgroundMusic=null){
        if(backgroundMusic!=null){
            Instance.audioSource.clip=backgroundMusic;
        }
        if(Instance.audioSource.clip!=null){
            if(resetSong){
                Instance.audioSource.Stop();
            }
            Instance.audioSource.Play();
        }
    }
    public static void PauseBackgroundMusic(){
        Instance.audioSource.Pause();
    }
}
