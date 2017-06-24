using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour {

  public float StandingThreshold = 3.0f;
  public float MoveThreshold = 3.0f;

  public bool IsStanding() {
    return
      transform.rotation.eulerAngles.x <= StandingThreshold &&
      transform.rotation.eulerAngles.z <= StandingThreshold;
  }

}