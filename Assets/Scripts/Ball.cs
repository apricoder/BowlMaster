using UnityEngine;

public class Ball : MonoBehaviour {

  public int InitialSpeed = 700;
  
  private Rigidbody _rigidbody;
  private AudioSource _audioSource;


  private void Start() {
    _rigidbody = GetComponent<Rigidbody>();
    _audioSource = GetComponent<AudioSource>();

    _rigidbody.velocity = new Vector3(0.0f, -0.3f, 1.0f) * InitialSpeed;
    _audioSource.Play();
  }

}