using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [SerializeField] float moveSpeed = 5f;
  InputAction moveAction;
  Rigidbody rb;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    // "Player/Move" maps to the WS and arrow key bindings
    moveAction = InputSystem.actions.FindAction("Player/Move", true);
  }

  void Update()
  {
    Vector2 input = moveAction.ReadValue<Vector2>();
    rb.AddForce(input.y * moveSpeed * Vector3.forward);
  }
}