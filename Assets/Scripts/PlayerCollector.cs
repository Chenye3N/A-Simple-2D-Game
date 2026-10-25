using UnityEngine;

public class PlayerCollector:MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        Item item=collision.GetComponent<Item>();
        if(item!=null){
            item.Collect();
        }
    }
}
