using Space_Invaders;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DesktopGameTests
{
    private DesktopGame _game;

    [TestInitialize]
    public void SetUp()
    {
        try
        {
            _game = new DesktopGame();
        }
        catch (System.BadImageFormatException ex)
        {
            Assert.Inconclusive($"Platform mismatch: {ex.Message}");
        }
    }

    [TestMethod]
    public void TestPlayerMovesLeft()
    {
        var keyEventArgs = new KeyEventArgs(Keys.Left);
        _game.KeyIsDown(this, keyEventArgs);
        Assert.IsTrue(_game.GoLeft);
    }

    [TestMethod]
    public void TestPlayerMovesRight()
    {
        var keyEventArgs = new KeyEventArgs(Keys.Right);
        _game.KeyIsDown(this, keyEventArgs);
        Assert.IsTrue(_game.GoRight);
    }

    [TestMethod]
    public void TestPlayerStopsWhenKeyIsUp()
    {
        // Press left
        var keyDownEventArgs = new KeyEventArgs(Keys.Left);
        _game.KeyIsDown(this, keyDownEventArgs);

        // Release left
        var keyUpEventArgs = new KeyEventArgs(Keys.Left);
        _game.KeyIsUp(this, keyUpEventArgs);

        Assert.IsFalse(_game.GoLeft);
    }
}