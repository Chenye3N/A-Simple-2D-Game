using UnityEngine;
using System;
public class SpeedItem:MonoBehaviour,Item{
    public float speedBoost=2f;
    public static event Action<float>OnSpeedBoost;
    public void Collect(){
        OnSpeedBoost?.Invoke(speedBoost);
        Destroy(gameObject);
    }
}
