using UnityEngine;
using System.Collections;
using System;

public class PlayerHP : MonoBehaviour
{
    public int maxHP=3;
    private int currentHP;
    public HealthUI healthUI;
    public static event Action PlayerDied;

    void Start(){
        ResetHP();
        GameController.GameOverEvent+=ResetHP;
    }
    void OnTriggerEnter2D(Collider2D other){
        Enemy enemy=other.GetComponent<Enemy>();
        if(enemy){
            TakeDamage(enemy.damage);
            StartCoroutine(FlashRed());
        }
    }
    private void TakeDamage(int damage){
        currentHP-=damage;
        healthUI.SetHP(currentHP);
        if(currentHP<=0){
            //Die
            PlayerDied?.Invoke();
        }
    }
    void ResetHP(){
        currentHP=maxHP;
        healthUI.SetMaxHP(maxHP);
    }
    private IEnumerator FlashRed(){
        gameObject.GetComponent<SpriteRenderer>().color=Color.red;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().color=Color.white;
    }
    void OnDestroy(){
        GameController.GameOverEvent-=ResetHP;
    }
}
