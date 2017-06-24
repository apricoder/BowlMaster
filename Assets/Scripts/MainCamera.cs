using UnityEngine;

public class MainCamera : MonoBehaviour {

  public Ball Ball;
  public int StopOffset = 100;
  public GameObject StopObject;

  private Vector3 _offset;

  private void Start() {
    _offset = transform.position - Ball.transform.position;
  }

  private void Update() {
    if (Ball.transform.position.z < StopObject.transform.position.z - StopOffset) {
      transform.position = Ball.transform.position + _offset;
    }
  }

}