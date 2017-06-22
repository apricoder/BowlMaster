using UnityEngine;

public class Ball : MonoBehaviour {

  public Vector3 InitialVelocity = new Vector3(0.0f, -0.3f, 700.0f);
  
  private Rigidbody _rigidbody;
  private AudioSource _audioSource;


  private void Start() {
    _rigidbody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();

    _rigidbody.velocity = InitialVelocity;
    _audioSource.Play();
  }

}