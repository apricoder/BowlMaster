using NUnit.Framework;

namespace Tests {

  [TestFixture]
  public class ActionMasterTest {

    private ActionMaster _actionMaster;
    private const ActionMaster.Action EndTurn = ActionMaster.Action.EndTurn;
    private const ActionMaster.Action Tidy = ActionMaster.Action.Tidy;
    private const ActionMaster.Action Reset = ActionMaster.Action.Reset;
    private const ActionMaster.Action EndGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup() {
      _actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest() {
      Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
      Assert.AreEqual(EndTurn, _actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() {
      Assert.AreEqual(Tidy, _actionMaster.Bowl(8));
    }

    [Test]
    public void T04Bowl28SpareReturnsEndTurn() {
      _actionMaster.Bowl(8);
      Assert.AreEqual(EndTurn, _actionMaster.Bowl(2));
    }

    [Test]
    public void T05CheckResetAtStrikeInLastFrame() {
      int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
      foreach (var roll in rolls) {
        _actionMaster.Bowl(roll);
      }
      Assert.AreEqual(Reset, _actionMaster.Bowl(10));
    }

    [Test]
    public void T06CheckResetAtSpareInLastFrame() {
      int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
      foreach (var roll in rolls) {
        _actionMaster.Bowl(roll);
      }
      _actionMaster.Bowl(1);
      Assert.AreEqual(Reset, _actionMaster.Bowl(9));
    }

    [Test]
    public void T07YouTubeRollsEndInEndGame() {
      int[] rolls = {8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2};
      foreach (var roll in rolls) {
        _actionMaster.Bowl(roll);
      }
      Assert.AreEqual(EndGame, _actionMaster.Bowl(9));
    }

    [Test]
    public void T08GameEndsAtBowl20() {
      int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
      foreach (var roll in rolls) {
        _actionMaster.Bowl(roll);
      }
      Assert.AreEqual(EndGame, _actionMaster.Bowl(1));
    }

  }
}