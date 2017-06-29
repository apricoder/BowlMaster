using UnityEngine;

public class ActionMaster {

  public enum Action {

    Tidy,
    EndTurn,
    EndGame

  }

  public Action Bowl(int pins) {
    if (pins < 0 || pins > 10) {
      throw new UnityException("Invalid pin count!");
    }

    if (pins == 10) {
      return Action.EndTurn;
    }

    throw new UnityException("Unhandled pin count!");
  }

}