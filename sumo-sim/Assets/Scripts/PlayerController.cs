using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  Rigidbody rb;
  bool _onPowerUp;
  InputAction moveAction;
  GameObject focalPointGameObj;
  [SerializeField] float inputForce = 5f;
  [SerializeField] GameObject powerUpIndicator;
  private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5);
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    // "Player/Move" maps to the WS and arrow key bindings
    moveAction = InputSystem.actions.FindAction("Player/Move", true);
    focalPointGameObj = GameObject.Find("FocalPoint");
    powerUpIndicator.SetActive(false);
  }

  void Update()
  {
    Vector2 input = moveAction.ReadValue<Vector2>();
    rb.AddForce(input.y * inputForce * focalPointGameObj.transform.forward);
    powerUpIndicator.transform.position = transform.position;
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("OnTriggerEnter");
    if (other.CompareTag("Powerup"))
    {
      _onPowerUp = true;
      powerUpIndicator.SetActive(true);
      Destroy(other.gameObject);
      Debug.Log($"({_onPowerUp}) Powerup: ON");
      StartCoroutine(nameof(EndPowerUp));
    }
  }

  IEnumerator EndPowerUp()
  {
    yield return _waitForSeconds5;
    _onPowerUp = false;
    Debug.Log($"({_onPowerUp}) Powerup: OFF");
    powerUpIndicator.SetActive(false);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Enemy") && _onPowerUp)
    {
      float powerUpForce = 5f * inputForce;
      Vector3 enemyDir = (collision.gameObject.transform.position - transform.position).normalized;
      Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
      enemyRb.AddForce(enemyDir * powerUpForce, ForceMode.Impulse);
    }
  }

}
