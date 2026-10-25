using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    void Start(){
        progressAmount=0;
        progressSlider.value=0;
        Gem.OnGemCollection+=IncreaseProgressAmount;
    }
    void IncreaseProgressAmount(int amount){
        progressAmount+=amount;
        progressSlider.value=progressAmount;
        if(progressAmount>=100){
            Debug.Log("Level Complete!");
        }
    }
    void OnDestroy(){
        Gem.OnGemCollection-=IncreaseProgressAmount;
    }
}
