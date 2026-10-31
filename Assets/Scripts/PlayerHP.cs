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
        HealthItem.HealEvent+=Heal;
    }
    void OnTriggerEnter2D(Collider2D other){
        Enemy enemy=other.GetComponent<Enemy>();
        if(enemy){
            TakeDamage(enemy.damage);
            StartCoroutine(FlashRed());
        }
        Trap trap=other.GetComponent<Trap>();
        if(trap&&trap.damage>0){
            TakeDamage(trap.damage);
            StartCoroutine(FlashRed());
        }
    }
    public void TakeDamage(int damage){
        currentHP-=damage;
        healthUI.SetHP(currentHP);
        if(currentHP<=0){
            //Die
            PlayerDied?.Invoke();
        }
    }
    void Heal(int healAmount){
        currentHP+=healAmount;
        if(currentHP>maxHP){
            currentHP=maxHP;
        }
        healthUI.SetHP(currentHP);
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
        HealthItem.HealEvent-=Heal;
    }
}
