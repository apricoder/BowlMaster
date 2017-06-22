using UnityEngine;

public class MainCamera : MonoBehaviour {

  public Ball Ball;
  public GameObject StopObject;
  
  private Vector3 _offset;

  private void Start() {
    _offset = transform.position - Ball.transform.position;
  }

  private void Update() {
    if (transform.position.z < StopObject.transform.position.z) {
      transform.position = Ball.transform.position + _offset;
    }
  }

}