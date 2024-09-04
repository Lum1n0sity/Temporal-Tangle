using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour {
  void OnCollisionEnter2D (Collision2D collision) {
    if (collision.gameObject.name == "Player") {
      Destroy(gameObject);
    } else if (collision.gameObject.name == "Collision_Tilemap") {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D (Collider2D collision) {
    if (collision.GetComponent<Collider>().GetComponent<CompositeCollider2D>() != null) {
      Destroy(gameObject);
    }
  }
}
