using NUnit.Framework;
using Scrabble.Model;
using Scrabble.Model.Word;
using System;

namespace UnitTests
{
    public class PlayerTest
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
            // Arrange for all non-static method test cases.
            _player = new Player();
        }

        [Test]
        public void CompareTo_ReturnValidNumberAfterCompare_GivenSamePlayerScore()
        {
            Player newPlayer = new Player();

            int result = _player.CompareTo(newPlayer);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CompareTo_ReturnValidNumberAfterCompare_GivenGreaterPlayerScore()
        {
            Player newPlayer = new Player();
            newPlayer.Score = 10;

            int result = _player.CompareTo(newPlayer);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void CompareTo_ReturnValidNumberAfterCompare_GivenSmallerPlayerScore()
        {
            Player newPlayer = new Player();
            newPlayer.Score = -1;

            int result = _player.CompareTo(newPlayer);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CompareTo_ReturnValidNumberAfterCompare_GivenNoPlayer()
        {
            int result = _player.CompareTo(null);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CompareTo_ReturnException_GivenPlayer()
        {
            AllTiles tiles = new AllTiles();
            var ex = Assert.Throws<ArgumentException>(() => _player.CompareTo(tiles));
            Assert.That(ex.Message, Is.EqualTo("Players Comparison Exception"));

        }

        [Test]
        public void ToString_ReturnValidString_GivenValidPlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Score = 100;

            string result = newPlayer.ToString();
            Assert.AreEqual("Player 0 has scores 100 now!", result);
        }

        [Test]
        public void Id_GetsPlayerId_GivenValidPlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Id = 1001;

            int result = newPlayer.Id;
            Assert.AreEqual(1001, result);
        }

    }
}
