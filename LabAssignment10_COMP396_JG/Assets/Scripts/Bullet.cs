using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour{
    public int maxDamageInflicted = 20;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Ground") {
            if (collision.gameObject.tag == "Player") {
                var health = collision.gameObject.GetComponent<Health>();
                health.TakeDamage(maxDamageInflicted);
            }
            Destroy(gameObject);
        }
    }
}
