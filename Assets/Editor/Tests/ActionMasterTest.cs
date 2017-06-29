using NUnit.Framework;

namespace Tests {

  [TestFixture]
  public class ActionMasterTest {

    private readonly ActionMaster _actionMaster = new ActionMaster();

    [Test]
    public void T00_SimplestTest() {
      Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_StrikeGivesEndTurn() {
      Assert.AreEqual(ActionMaster.Action.EndTurn, _actionMaster.Bowl(10));
    }

  }
}