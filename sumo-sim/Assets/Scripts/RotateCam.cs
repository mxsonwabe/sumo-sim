using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCam : MonoBehaviour
{
  [SerializeField] float rotSpeed;
  InputAction rotAction;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rotSpeed = 5f;
    rotAction = InputSystem.actions.FindAction("Camera/Move", true);
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 move = rotAction.ReadValue<Vector2>();
    Debug.Log("move: " + move.ToString());
    transform.Rotate(Vector3.up, rotSpeed * move.x * Time.deltaTime);
  }

  //void OnMove(InputValue inputMove)
  //{
  //  Vector2 move = inputMove.Get<Vector2>();
  //  transform.Rotate(Vector3.up * rotSpeed * move.x);
  //}
}
