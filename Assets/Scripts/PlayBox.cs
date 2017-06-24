using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayBox : MonoBehaviour {

	private BowlingPin[] _pins;
	private PinsCountText _standingPinsCountText;
	private int _standingPinsCount;
	private float _standingPinsCountChangeTime;
	private bool _countingScore;
	private const float ChangeTimeTolerance = 3.0f;

	private void Start() {
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
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Ball>()) {
			_standingPinsCountText.GetComponent<Text>().color = Color.red;
			_countingScore = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		var parentGo = other.gameObject.transform.parent.gameObject;
		if (parentGo.GetComponent<BowlingPin>()) {
			Destroy(parentGo);
		}
	}

}
