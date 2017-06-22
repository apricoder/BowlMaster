using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour {

  private Ball _ball;
  private Vector2 _position;


  private void Start() {
    _ball = FindObjectOfType<Ball>();
  }

  public void OnDragStart(BaseEventData eventData) {
    var pointerData = eventData as PointerEventData;
    if (pointerData != null) {
      _position = pointerData.position;
    }
    // remember position
  }

  public void OnDragEnd(BaseEventData eventData) {
    var pointerData = eventData as PointerEventData;
    if (pointerData != null) {
      var velocity = _position - pointerData.position;
      _ball.Launch(new Vector3(-1 * velocity.x, Ball.DefaultYVelocity, -1 * velocity.y));
    }
  }

}