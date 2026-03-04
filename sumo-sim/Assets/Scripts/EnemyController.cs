using UnityEngine;

public class EnemyController : MonoBehaviour
{
  GameObject player;
  Rigidbody rb;
  [SerializeField] float inputForce;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    inputForce = 2.5f;
    rb = GetComponent<Rigidbody>();
    player = GameObject.FindWithTag("Player");
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
