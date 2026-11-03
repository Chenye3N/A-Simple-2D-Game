using UnityEngine;
using System;

public class Gem:MonoBehaviour,Item{
    static public event Action<int>OnGemCollection;
    public int worth=5;
    public void Collect(){
        OnGemCollection.Invoke(worth);
        SoundEffectManager.Play("Gem");
        Destroy(gameObject);
    }
}
