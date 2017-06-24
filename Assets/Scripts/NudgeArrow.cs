using UnityEngine;

public class NudgeArrow : MonoBehaviour {

  public float FrameOffset = 1;
  public float MaxOffset = 40;

  private Ball _ball;
  private bool _touching;

  private void Start() {
    _ball = FindObjectOfType<Ball>();
  }

  public void OnTouchStart() {
    _touching = true;
  }

  public void OnTouchEnd() {
    _touching = false;
  }

  private void Update() {
    var frameOffset = FrameOffset * DirectionOf(transform.localScale.x);
    var doesntOverflow = Mathf.Abs(_ball.transform.position.x + frameOffset) <= MaxOffset;
    if (_touching && !_ball.IsLaunched && doesntOverflow) {
      _ball.transform.Translate(Vector3.right * frameOffset);
    }
  }

  private static int DirectionOf(float value) {
    return value >= 0 ? 1 : -1;
  }

}