using NUnit.Framework;
using Scrabble.Controller;
using System.Windows.Media;

namespace UnitTests.Controller
{
    public class GameControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameController_CanSwap_ExpectTrue()
        {
            Game game = new Game();
            Assert.IsTrue(game.CanSwap());
        }

        [Test]
        public void GameController_GameEnd_ExpectFalse()
        {
            Game game = new Game();
            Assert.IsFalse(game.GameEnd());
        }

        [Test]
        public void GameController_GameValidate_ExpectFalse()
        {
            Game game = new Game();
            char[,] b = { { 'a', 'b' } };
            Assert.IsFalse(game.Validate(b));
        }

    }
}
