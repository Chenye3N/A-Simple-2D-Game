using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
public class LoadLevel : MonoBehaviour
{
    public float holdDuration=1f;
    public Image fillCircle;
    private float timeHolder=0;
    private bool isHolding=false;
    public static event Action OnHoldComplete;
    void Start(){
        
    }

    
    void Update(){
        if(isHolding){
            timeHolder+=Time.deltaTime;
            fillCircle.fillAmount=timeHolder/holdDuration;
            if(timeHolder>=holdDuration){
                OnHoldComplete?.Invoke();
                isHolding=false;
                timeHolder=0;
                fillCircle.fillAmount=0;
            }
        }
    }
    public void OnHold(InputAction.CallbackContext context){
        if(context.started){
            isHolding=true;
        }
        else if(context.canceled){
            isHolding=false;
            timeHolder=0;
            fillCircle.fillAmount=0;
        }
    }
}
