using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour{
    private static SoundEffectManager Instance;
    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private Slider soundSlider;
    private void Awake(){
        if(Instance==null){
            Instance=this;
            audioSource=GetComponent<AudioSource>();
            soundEffectLibrary=GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        soundSlider.onValueChanged.AddListener(delegate{OnVolumeChanged();});
    }
    public static void Play(string name){
        AudioClip clip=soundEffectLibrary.GetRamdomSOundEffect(name);
        if(clip!=null){
            audioSource.PlayOneShot(clip);
        }
    }
    public void SetVolume(float volume){
        audioSource.volume=volume;
    }
    public void OnVolumeChanged(){
        SetVolume(soundSlider.value);
    }
}
