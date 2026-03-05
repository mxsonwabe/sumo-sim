using UnityEngine;

public class EnemyController : MonoBehaviour
{
  GameObject player;
  Rigidbody rb;
  [SerializeField] float inputForce;
  public static int activeEnemyCount = 0;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    // called when the game object is created
    activeEnemyCount++;
    inputForce = 2.5f;
    rb = GetComponent<Rigidbody>();
    player = GameObject.FindWithTag("Player");
  }

  private void OnDestroy()
  {
    // called when the object is destroyed
    activeEnemyCount--;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 playerDir = (player.transform.position - transform.position).normalized;
    rb.AddForce(playerDir * inputForce);
    if (transform.position.y < -5f)
    {
      Destroy(gameObject);
    }
  }
}
