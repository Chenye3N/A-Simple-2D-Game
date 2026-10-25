using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public GameObject Player;
    public GameObject LoadCanvas;
    public Slider progressSlider;
    public List<GameObject>Levels;
    private int currentLevel=0;
    void Start(){
        progressAmount=0;
        progressSlider.value=0;
        Gem.OnGemCollection+=IncreaseProgressAmount;
        LoadLevel.OnHoldComplete+=LoadNextLevel;
        LoadCanvas.SetActive(false);
    }
    void IncreaseProgressAmount(int amount){
        progressAmount+=amount;
        progressSlider.value=progressAmount;
        if(progressAmount>=100){
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete!");
        }
    }
    void LoadNextLevel(){
        int nextLevel=(currentLevel+1==Levels.Count)?0:currentLevel+1;
        LoadCanvas.SetActive(false);
        Levels[currentLevel].gameObject.SetActive(false);
        Levels[nextLevel].gameObject.SetActive(true);
        currentLevel=nextLevel;
        progressAmount=0;
        progressSlider.value=0;
    }
    void OnDestroy(){
        Gem.OnGemCollection-=IncreaseProgressAmount;
        LoadLevel.OnHoldComplete-=LoadNextLevel;
        Player.transform.position=new Vector3(0,0,0);
    }
}
