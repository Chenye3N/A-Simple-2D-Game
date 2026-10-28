using UnityEngine;

public class Bullet:MonoBehaviour{
    public int damage=1;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
