using UnityEngine;

public enum PowerUpType
{
  None,
  Rocket,
  PushBack,
  Smash
}

public class PowerUp : MonoBehaviour
{
  public PowerUpType powerUpType;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() { }

  // Update is called once per frame
  void Update() { }
}
