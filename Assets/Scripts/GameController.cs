using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public GameObject Player;
    public GameObject LoadCanvas;
    public Slider progressSlider;
    public List<GameObject>Levels;
    private int currentLevel=0;
    public GameObject gameOverScreen;
    public TMP_Text survivedText;
    private int survivedLevels;
    public static event Action GameOverEvent;
    void Start(){
        progressAmount=0;
        progressSlider.value=0;
        Gem.OnGemCollection+=IncreaseProgressAmount;
        LoadLevel.OnHoldComplete+=LoadNextLevel;
        PlayerHP.PlayerDied+=GameOver;
        LoadCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
    }
    void GameOver(){
        gameOverScreen.SetActive(true);
        survivedText.text="You survived "+survivedLevels+" level(s)!";
        Time.timeScale=0;
    }
    public void ResetGame(){
        GameOverEvent?.Invoke();
        gameOverScreen.SetActive(false);
        survivedLevels=0;
        LoadCanvas.SetActive(false);
        Levels[currentLevel].gameObject.SetActive(false);
        currentLevel=0;
        Levels[currentLevel].gameObject.SetActive(true);
        progressAmount=0;
        progressSlider.value=0;
        Player.transform.position=new Vector3(0,0,0);
        Time.timeScale=1;
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
        ++survivedLevels;
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
        PlayerHP.PlayerDied-=GameOver;
    }
}
