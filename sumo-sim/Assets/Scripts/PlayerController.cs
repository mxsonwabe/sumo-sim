using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  Rigidbody rb;
  bool _onPowerUp;
  InputAction moveAction;
  GameObject focalPointGameObj;
  [SerializeField] float inputForce = 3.5f;
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

    if (transform.position.y < -5)
    {
      transform.position = new Vector3(0f, 0.5f, 0f);
      rb.linearVelocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
    }
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
      int currentEnemies = EnemyController.activeEnemyCount;
      int powerUpOffset = currentEnemies / 5;
      float baseForce = (5.5f * inputForce);
      float powerUpForce = baseForce * (1 + powerUpOffset);
      Vector3 enemyDir = (collision.gameObject.transform.position - transform.position).normalized;
      Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
      enemyRb.AddForce(enemyDir * powerUpForce, ForceMode.Impulse);
    }
  }
 } 