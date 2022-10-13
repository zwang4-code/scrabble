using NUnit.Framework;
using Scrabble.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class PlayerManagerTest
    {
        private PlayerManager _playerManager;

        [SetUp]
        public void Setup()
        {
            // Arrange for all non-static method test cases.
            _playerManager = new PlayerManager();
        }

        [Test]
        [TestCase(2, ExpectedResult = 2, TestName = "CreatePlayers_ReturnCorrectNumberOfPlayers_GivenValidNumber(2)")]
        [TestCase(3, ExpectedResult = 3, TestName = "CreatePlayers_ReturnCorrectNumberOfPlayers_GivenValidNumber(3)")]
        [TestCase(10, ExpectedResult = 10, TestName = "CreatePlayers_ReturnCorrectNumberOfPlayers_GivenValidNumber(10)")]
        public int CreatePlayers_ReturnCorrectNumberOfPlayers_GivenValidNumber(int numOfPlayers)
        {
            GameState gs = new GameState();
            gs.NumOfPlayers = numOfPlayers;

            // Act
            _playerManager.CreatePlayers(gs);

            return gs.ListOfPlayers.Count;
        }

        [Test]
        [TestCase(250, ExpectedResult = 250, TestName = "AddScoresToPlayer_ReturnCorrectScore_GivenValidScore(250)")]
        [TestCase(124, ExpectedResult = 124, TestName = "AddScoresToPlayer_ReturnCorrectScore_GivenValidScore(124)")]
        [TestCase(380, ExpectedResult = 380, TestName = "AddScoresToPlayer_ReturnCorrectScore_GivenValidScore(380)")]
        public int AddScoresToPlayer_ReturnCorrectScore_GivenValidScore(int score)
        {
            Player p = new Player();

            // Act
            _playerManager.AddScoresToPlayer(p, score);

            return p.Score;
        }

        [Test]
        [TestCase('B', 10, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('B', 10)")]
        [TestCase('Z', 50, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('Z', 50)")]
        [TestCase('L', 18, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('L', 18)")]
        public void GetNewTiles_ReturnRandomCharacter_GivenValidCharacter(char letter, int tileValue)
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tileValue);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            Tile tile2 = new Tile(letter, tileValue);
            p2.PlayingTiles.Add(tile2);

            gameState.ListOfPlayers.Add(p1);
            gameState.ListOfPlayers.Add(p2);

            List<char> LoC = new List<char>();
            LoC.Add('L');
            LoC.Add('Z');
            LoC.Add('B');
            LoC.Add('D');

            // Act
            _playerManager.GetNewTiles(gameState, LoC, 0);

            // Assert
            Assert.AreNotEqual(letter, gameState.ListOfPlayers[0].PlayingTiles[0].TileChar);
        }

        [Test]
        [TestCase('c', 10, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('c')")]
        [TestCase('A', 1, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('A')")]
        [TestCase('Z', 26, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('Z')")]
        public void Swap_ReturnRandomCharacter_GivenValidCharacter(char letter, int tileValue)
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tileValue);
            p1.PlayingTiles.Add(tile);

            GameState.GSInstance.ListOfPlayers.Add(p1);
            GameState.GSInstance.TilesBag.ListTiles.Add(tile);

            // Act
            char swapedChar = _playerManager.Swap(letter);

            // Assert
            Assert.AreNotEqual(letter, swapedChar);
        }
    }
}
