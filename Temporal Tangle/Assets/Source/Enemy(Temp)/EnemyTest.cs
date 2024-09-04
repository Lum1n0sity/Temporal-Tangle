using UnityEngine;

public class EnemyTest : MonoBehaviour {
  public GameObject projectilePrefab;
  public float projectileSpeed = 10f;
  public float fireRate = 1f;
  public float shootingRange = 10f;
  public float maxProjectileDistance = 15f;

  private Transform player;
  private float nextFireTime;

  void Start() {
    GameObject playerObj = GameObject.Find("Player");

    if (playerObj != null) {
      player = playerObj.transform;
    } else {
      Debug.LogError("Player not found in scene!");
    }

    nextFireTime = Time.time;
  }

  void Update() {
    if (player == null) return;
    
    if (Vector2.Distance(transform.position, player.position) <= shootingRange) {
      if (Time.time > nextFireTime) {
        ShootAtPlayer();
        nextFireTime = Time.time + 1f / fireRate;
      }
    }
  }
  
  void ShootAtPlayer() {
    Vector2 direction = (player.position - transform.position).normalized;

    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    if (rb != null) {
      rb.linearVelocity = direction * projectileSpeed;
    }

    Destroy(projectile, maxProjectileDistance / projectileSpeed);
  }
}
