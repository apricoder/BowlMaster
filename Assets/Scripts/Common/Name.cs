using System;

namespace Common {
  public class Name {

    public static string OfMethod(Action action) {
      return action.Method.Name;
    }

  }
}