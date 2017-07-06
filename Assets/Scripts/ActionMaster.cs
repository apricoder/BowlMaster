using UnityEngine;

public class ActionMaster {

  public enum Action {

    Tidy,
    Reset,
    EndTurn,
    EndGame

  }

  private readonly int[] _bowls = new int[21];
  private int _bowl = 1;

  public Action Bowl(int pins) {
    if (pins < 0 || pins > 10) {
      throw new UnityException("Invalid pins");
    }

    _bowls[_bowl - 1] = pins;

    if (_bowl == 21) {
      return Action.EndGame;
    }

    // Hanld last-frame special cases
    if (_bowl >= 19 && Bowl21Awarded()) {
      _bowl += 1;
      return Action.Reset;
    } else if (_bowl == 20 && !Bowl21Awarded()) {
      return Action.EndGame;
    }

    if (pins == 10) {
      _bowl += 2;
      return Action.EndTurn;
    }

    if (_bowl % 2 != 0) {
      // Mid frame (or last frame)
      _bowl += 1;
      return Action.Tidy;
    } else if (_bowl % 2 == 0) {
      // End of frame
      _bowl += 1;
      return Action.EndTurn;
    }

    throw new UnityException("Not sure what action to return!");
  }

  private bool Bowl21Awarded() {
    // Remember that arrays start counting at 0
    return (_bowls[19 - 1] + _bowls[20 - 1] >= 10);
  }

}