using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public List<Image>hearts=new List<Image>();
    public LayerMask enemyLayer;
    public void SetMaxHP(int maxHP){
        foreach(Image heart in hearts){
            Destroy(heart.gameObject);
        }
        hearts.Clear();
        for(int i=1;i<=maxHP;++i){
            Image heart=Instantiate(heartPrefab,transform);
            heart.sprite=fullHeart;
            heart.color=Color.red;
            hearts.Add(heart);
        }
    }
    public void SetHP(int currentHP){
        for(int i=0;i<hearts.Count;++i){
            if(i<currentHP){
                hearts[i].sprite=fullHeart;
                hearts[i].color=Color.red;
            }
            else{
                hearts[i].sprite=emptyHeart;
                hearts[i].color=Color.gray;
            }
        }
    }
}
