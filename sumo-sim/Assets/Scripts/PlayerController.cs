using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  Rigidbody rb;
  bool isOnGround;
  InputAction moveAction;
  InputAction fireAction;
  GameObject focalPointGameObj;
  [SerializeField] float inputForce = 3.5f;
  [SerializeField] GameObject rocketPrefab;
  [SerializeField] GameObject powerUpIndicator;
  PowerUpType currentPowerType = PowerUpType.None;
  private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5);
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    // "Player/Move" maps to the WS and arrow key bindings
    moveAction = InputSystem.actions.FindAction("Player/Move", true);
    fireAction = InputSystem.actions.FindAction("Player/Fire", true);
    focalPointGameObj = GameObject.Find("FocalPoint");
    powerUpIndicator.SetActive(false);
  }

  void Update()
  {
    Vector2 input = moveAction.ReadValue<Vector2>();
    rb.AddForce(input.y * inputForce * focalPointGameObj.transform.forward);
    powerUpIndicator.transform.position = transform.position;

    // displace the player once it falls into the 'void'
    if (transform.position.y < -5)
    {
      transform.position = new Vector3(0f, 0.5f, 0f);
      rb.linearVelocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
    }

    // check if the player fire's missile while on 'power'
    if (currentPowerType == PowerUpType.Rocket && fireAction.WasPressedThisFrame())
    {
      LaunchRocket();
    }
    if (currentPowerType == PowerUpType.Smash && fireAction.WasPressedThisFrame())
    {
      SmashEnemies();
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("OnTriggerEnter");
    if (other.CompareTag("Powerup"))
    {
      currentPowerType = other.gameObject.GetComponent<PowerUp>().powerUpType;
      powerUpIndicator.SetActive(true);
      Destroy(other.gameObject);
      Debug.Log($"({currentPowerType}) Powerup: ON");
      StartCoroutine(nameof(EndPowerUp));
    }
  }

  void LaunchRocket()
  {
    // instantiate a rocket to be fired at each enemy
    foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    {
      Vector3 spawnPos = transform.position;
      spawnPos.y = rocketPrefab.transform.position.y;
      GameObject tmpRocket = Instantiate(rocketPrefab, spawnPos, rocketPrefab.transform.rotation);
      tmpRocket.GetComponent<RocketController>().Fire(enemy.transform);
    }
  }
  IEnumerator EndPowerUp()
  {
    yield return _waitForSeconds5;
    currentPowerType = PowerUpType.None;
    Debug.Log($"({currentPowerType}) Powerup: OFF");
    powerUpIndicator.SetActive(false);
  }

  private void OnCollisionEnter(Collision collision)
  {
    Debug.Log("OnCollisionEnter");
    GameObject gmObject = collision.gameObject;
    if (gmObject.CompareTag("Enemy") && currentPowerType == PowerUpType.PushBack)
    {
      PushEnemy(collision.gameObject);
    }
    else if (gmObject.CompareTag("Ground"))
    {
      isOnGround = true;
    }
  }

  void SmashEnemies()
  {
    int currentEnemies = EnemyController.activeEnemyCount;
    rb.AddForce(Vector3.up * 20, ForceMode.Impulse);
    rb.AddForce(Vector3.up * -10, ForceMode.Impulse);
    isOnGround = false;
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    if (!isOnGround)
    {
      foreach (var enemy in enemies)
      {
        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        Vector3 away = (enemy.transform.position - transform.position).normalized;
        enemyRb.AddForce(away * 10, ForceMode.Impulse);
      }
    }
  }
  void PushEnemy(GameObject gmObj)
  {

    int currentEnemies = EnemyController.activeEnemyCount;
    int powerUpOffset = currentEnemies / 5;
    float baseForce = (5.5f * inputForce);
    float powerUpForce = baseForce * (1 + powerUpOffset);
    Vector3 enemyDir = (gmObj.transform.position - transform.position).normalized;
    Rigidbody enemyRb = gmObj.GetComponent<Rigidbody>();
    enemyRb.AddForce(enemyDir * powerUpForce, ForceMode.Impulse);
  }
}
