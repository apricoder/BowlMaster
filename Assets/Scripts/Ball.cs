using UnityEngine;

public class Ball : MonoBehaviour {
  
  public static readonly float DefaultYVelocity = -0.3f;

  public bool IsLaunched;

  private Rigidbody _rigidbody;
  private AudioSource _audioSource;

  private void Start() {
    _rigidbody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();
    _rigidbody.useGravity = false;
    // Launch(InitialVelocity);
  }

  public void Launch(Vector3 velocity) {
    _rigidbody.useGravity = true;
    _rigidbody.velocity = velocity;
    _audioSource.Play();
    IsLaunched = true;
  }
  
}