using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayBox : MonoBehaviour {

	private BowlingPin[] _pins;
	private PinsCountText _standingPinsCountText;
	private int _standingPinsCount;
	private bool _countingScore;

	private void Start() {
		_pins = FindObjectsOfType<BowlingPin>();
		_standingPinsCountText = FindObjectOfType<PinsCountText>();
	}

	private void Update() {
		if (_countingScore) {
			_standingPinsCount = _pins.Count(p => p.IsStanding());
			_standingPinsCountText.GetComponent<Text>().text = _standingPinsCount.ToString();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Ball>()) {
			_countingScore = true;
		}
	}

}
