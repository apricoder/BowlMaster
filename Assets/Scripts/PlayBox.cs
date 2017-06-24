using System.Collections;
using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayBox : MonoBehaviour {

  public GameObject PinPrefab;

  private const float ChangeTimeTolerance = 3.0f;

  private Ball _ball;
  private BowlingPin[] _pins;
  private Elevator _elevator;
  private Vector3 _savedPosition;
  private GameObject _closet;
  private PinsCountText _standingPinsCountText;
  private float _standingPinsCountChangeTime;
  private int _standingPinsCount;
  private bool _countingScore;

  private void Start() {
    _ball = FindObjectOfType<Ball>();
    _elevator = FindObjectOfType<Elevator>();
    _closet = GameObject.Find("Closet");
    _pins = FindObjectsOfType<BowlingPin>();
    _standingPinsCountText = FindObjectOfType<PinsCountText>();
  }

  private void Update() {
    var standingCount = _pins.Count(p => p && p.IsStanding());
    if (_standingPinsCount != standingCount) {
      UpdateStandingPinsCount(standingCount);
    }

    var timeSinceLastChange = Time.timeSinceLevelLoad - _standingPinsCountChangeTime;
    if (_countingScore && timeSinceLastChange > ChangeTimeTolerance) {
      PinsHaveSettled();
    }
  }

  private void UpdateStandingPinsCount(int standingCount) {
    _standingPinsCount = standingCount;
    _standingPinsCountChangeTime = Time.timeSinceLevelLoad;
    _standingPinsCountText.GetComponent<Text>().text = _standingPinsCount.ToString();
  }

  private void PinsHaveSettled() {
    _countingScore = false;
    _standingPinsCountText.GetComponent<Text>().color = Color.green;
    _ball.Invoke(Name.OfMethod(_ball.ResetPosition), 2.0f);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.GetComponent<Ball>()) {
      _standingPinsCountText.GetComponent<Text>().color = Color.red;
      _standingPinsCountChangeTime = Time.timeSinceLevelLoad;
      _countingScore = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    var parent = other.gameObject.transform.parent;
    if (parent && parent.gameObject.GetComponent<BowlingPin>()) {
      Destroy(parent.gameObject);
    }
  }

  public void AttachStandingPinsToElevator() {
    foreach (var pin in _pins.Where(p => p && p.IsStanding())) {
      pin.GetComponent<Rigidbody>().useGravity = false;
      var pinHolder = pin.gameObject.transform.parent;
      pinHolder.transform.parent = _elevator.gameObject.transform;
    }
  }

  public void OnRiseEnded() {
    _closet.transform.position = _elevator.transform.position;
    _elevator.transform.SetParent(_closet.transform);
  }

  public void OnSwipeEnded() {
    _elevator.transform.parent = gameObject.transform;
  }

  public void OnLowerEnd() {
    foreach (Transform pinPosition in GetChildren(_elevator.transform)) {
      pinPosition.parent = FindObjectOfType<Pins>().transform;
      pinPosition.GetComponentInChildren<Rigidbody>().useGravity = true;
    }
  }

  public void OnRenewStart() {
    foreach (Transform pinsPosition in GetChildren(FindObjectOfType<Pins>().transform)) {
      pinsPosition.SetParent(_elevator.transform);
      var pin = Instantiate(PinPrefab, pinsPosition);
      pin.GetComponent<Rigidbody>().useGravity = false;
    }
    _pins = FindObjectsOfType<BowlingPin>();
  }

  private static ArrayList GetChildren(Transform parent) {
    var pinsPositions = new ArrayList();
    foreach (var child in parent) {
      pinsPositions.Add(child);
    }
    return pinsPositions;
  }

}