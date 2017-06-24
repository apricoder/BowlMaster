using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayBox : MonoBehaviour {

	private Ball _ball;
	private BowlingPin[] _pins;
	private PinsCountText _standingPinsCountText;
	private int _standingPinsCount;
	private float _standingPinsCountChangeTime;
	private bool _countingScore;
	
	private const float ChangeTimeTolerance = 3.0f;

	private void Start() {
		_ball = FindObjectOfType<Ball>();
		_pins = FindObjectsOfType<BowlingPin>();
		_standingPinsCountText = FindObjectOfType<PinsCountText>();
	}

	private void Update() {
		var standingCount = _pins.Count(p => p && p.IsStanding());
		if (_countingScore && _standingPinsCount != standingCount) {
			UpdateStandingPinsCount(standingCount);
		}

		var timeSinceLastChange = Time.timeSinceLevelLoad - _standingPinsCountChangeTime;
		if (_countingScore && timeSinceLastChange > ChangeTimeTolerance) {
			PinsHaveSettled();
		}
	}

	private void UpdateStandingPinsCount(int standingCount) {
		_standingPinsCount = standingCount;
		_standingPinsCountText.GetComponent<Text>().text = _standingPinsCount.ToString();
		_standingPinsCountChangeTime = Time.timeSinceLevelLoad;
	}

	private void PinsHaveSettled() {
		_standingPinsCountText.GetComponent<Text>().color = Color.green;
		_ball.Invoke(Name.OfMethod(_ball.ResetPosition), 2.0f);
		_countingScore = false;
	}

	private void OnTriggerEnter(Collider other) {
		
		Debug.Log(other.gameObject);
		
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

}
