using UnityEngine;

public class RocketController : MonoBehaviour
{
  bool homing = false;
  float speed = 25.0f;
  float rocketStrength = 15.0f;
  float aliveTimer = 3.5f;

  Transform target;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (target != null && homing)
    {
      Vector3 moveDirection = (target.position - transform.position).normalized;
      // pepertually move the bullet in the direction of the enemy
      transform.position += moveDirection * speed * Time.deltaTime;
      // turn the bullet to face the target
      transform.LookAt(target);
    }

  }
  
  public void Fire(Transform newTarget)
  {
    // on fire set a new target object
    target = newTarget;
    homing = true;
    Destroy(gameObject, aliveTimer);
  }

  void OnCollisionEnter(Collision collision)
  {
    // if the bullet collides w/ an enemy
    if (target != null && collision.gameObject.CompareTag("Enemy"))
    {
      // once you get the enemy's rigid body, apply a force to push it away
      Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
      Vector3 away = (collision.gameObject.transform.position - transform.position).normalized;
      Vector3 force = away * rocketStrength;
      Debug.Log($"force: ({force})");
      enemyRb.AddForce(force, ForceMode.Impulse);
      // destroy bullet on impact
      Destroy(gameObject);
    }
  }
}
