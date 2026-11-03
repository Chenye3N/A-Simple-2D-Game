using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static SoundEffectManager instance;
    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private Slider soundSlider;
    private void Awake(){
        if(instance==null){
            instance=this;
            audioSource=GetComponent<AudioSource>();
            soundEffectLibrary=GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        soundSlider.onValueChanged.AddListener(delegate{onVolumeChanged();});
    }
    public static void Play(string name){
        AudioClip clip=soundEffectLibrary.GetRamdomSoundEffect(name);
        if(clip!=null){
            audioSource.PlayOneShot(clip);
        }
    }
    public static void SetVolume(float volume){
        audioSource.volume=volume;
    }
    public void onVolumeChanged(){
        SetVolume(soundSlider.value);
    }
}
