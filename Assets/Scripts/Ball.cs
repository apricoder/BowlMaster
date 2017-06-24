using UnityEngine;

public class Ball : MonoBehaviour {

  public static readonly float DefaultYVelocity = -0.3f;

  public bool IsLaunched;

  private Rigidbody _rigidbody;
  private AudioSource _audioSource;
  private Vector3 _initialPosition;

  private void Start() {
    _initialPosition = transform.position;
    _rigidbody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();
    _rigidbody.useGravity = false;
  }

  public void Launch(Vector3 velocity) {
    _rigidbody.useGravity = true;
    _rigidbody.velocity = velocity;
    _audioSource.Play();
    IsLaunched = true;
  }

  public void ResetPosition() {
    transform.position = _initialPosition;
    _rigidbody.angularVelocity = Vector3.zero;
    _rigidbody.velocity = Vector3.zero;
    _rigidbody.useGravity = false;
    IsLaunched = false;
  }

}