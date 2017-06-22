using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour {

  private Ball _ball;
  private Vector2 _dragStartPosition;
  private float _dragStartTime;
  private const float XTolerance = 0.5f;


  private void Start() {
    _ball = FindObjectOfType<Ball>();
  }

  public void OnDragStart(BaseEventData eventData) {
    var pointerData = eventData as PointerEventData;
    if (pointerData != null) {
      _dragStartPosition = pointerData.position;
      _dragStartTime = Time.timeSinceLevelLoad;
    }
  }

  public void OnDragEnd(BaseEventData eventData) {
    var pointerData = eventData as PointerEventData;
    if (pointerData != null) {
      var dragEndPosition = pointerData.position;
      var dragEndTime = Time.timeSinceLevelLoad;
      var dragDuration = dragEndTime - _dragStartTime;
      var dragVector = _dragStartPosition - dragEndPosition;
      var translatedVector = new Vector3(-dragVector.x, Ball.DefaultYVelocity, -dragVector.y);
      _ball.Launch(LimitedVelocity(translatedVector / dragDuration));
    }
  }

  private static Vector3 LimitedVelocity(Vector3 velocity) {
    return new Vector3(
      XTolerance * velocity.x,
      velocity.y,
      Mathf.Clamp(velocity.z, 0, 1000)
    );
  }

}