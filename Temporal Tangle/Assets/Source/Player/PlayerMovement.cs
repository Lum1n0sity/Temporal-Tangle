using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
  public float speed = 5.0f;
  private Vector2 movement;
  private Rigidbody2D rb;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnMovement(InputValue value) {
    movement = value.Get<Vector2>();
  }

  void Update() {
    transform.rotation = Quaternion.Euler(0, 0, 0);
  }

  private void FixedUpdate() {
    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
  }
}
