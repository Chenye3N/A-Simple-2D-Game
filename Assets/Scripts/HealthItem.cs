using UnityEngine;
using System;

public class HealthItem:MonoBehaviour,Item{
    public static event Action<int>HealEvent;
    public void Collect(){
        HealEvent?.Invoke(1);
        Destroy(gameObject);
    }
}
