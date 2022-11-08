using NUnit.Framework;
using Scrabble;
using Scrabble.Controller;
using Scrabble.Model;
using Scrabble.Model.Game;
using Scrabble.View;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ScrabbleTest.Controller
{
    public class GameControllerTest
    {
        // Set up a mock Board
        private char[,] _mockBoard = new char[15, 15];
        private const TileType __ = TileType.Default;
        private const TileType DL = TileType.LetterDouble;
        private const TileType TL = TileType.LetterTriple;
        private const TileType DW = TileType.WordDouble;
        private const TileType TW = TileType.WordTriple;
        private const TileType ST = TileType.Start;

        public IView view;
        public IView view1;
        public IView view2;

        [SetUp]
        public void Setup()
        {
            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O
             *          D  I  N
             ***************************/
            _mockBoard[7, 7] = 'G';
            _mockBoard[8, 7] = 'O';
            _mockBoard[9, 7] = 'O';
            _mockBoard[10, 7] = 'D';
            _mockBoard[8, 8] = 'K';
            _mockBoard[10, 8] = 'I';
            _mockBoard[10, 9] = 'N';
        }

        [Test]
        public void GameController_CanSwap_ExpectTrue()
        {
            Game game = new Game();
            Assert.IsTrue(game.CanSwap());
        }

        [Test]
        public void ClearMovement_AssertEquals_GivenValidMoves()
        {
            //Arrange
            Game game = new Game();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(0, 1);  // make first move
            mr.Record(0, 2);  // make second move

            game.moveRecorder = mr;
            game.ClearMovement();

            Assert.AreEqual(0, game.moveRecorder.Moves.Count);
        }

        [Test]
        public void UpdateState_ReturnCorrectAction_GivenValidBoard()
        {
            //Arrange
            Game game = new Game();
            char[,] b = { { 'a', 'b' } };
            game.gs.Initialise(1);
            
            game.UpdateState(b);
            var result = game.gs.LastAction;

            Assert.AreEqual("play", result);
        }

        [Test]
        [TestCase('B', 10, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('B', 10)")]
        [TestCase('Z', 50, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('Z', 50)")]
        [TestCase('L', 18, TestName = "GetNewTiles_ReturnRandomCharacter_GivenValidCharacter('L', 18)")]
        public void GetNewTiles_ReturnRandomCharacter_GivenValidCharacter(char letter, int tileValue)
        {
            Game game = new Game();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tileValue);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            Tile tile2 = new Tile(letter, tileValue);
            p2.PlayingTiles.Add(tile2);

            game.gs.ListOfPlayers.Add(p1);
            game.gs.ListOfPlayers.Add(p2);

            List<char> LoC = new List<char>();
            LoC.Add('L');
            LoC.Add('Z');
            LoC.Add('B');
            LoC.Add('D');

            // Act
            game.GetNewTiles(LoC, 0);

            // Assert
            Assert.AreNotEqual(letter, game.gs.ListOfPlayers[0].PlayingTiles[0].TileChar);
        }

        [Test]
        [TestCase('c', 10, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('c')")]
        [TestCase('B', 2, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('B')")]
        [TestCase('Z', 26, TestName = "Swap_ReturnRandomCharacter_GivenValidCharacter('Z')")]
        public void Swap_ReturnRandomCharacter_GivenValidCharacter(char letter, int tileValue)
        {
            Game game = new Game();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tileValue);
            p1.PlayingTiles.Add(tile);

            game.gs.ListOfPlayers.Add(p1);
            game.gs.TilesBag.ListTiles.Add(tile);

            // Act
            char swapedChar = game.SwapChar(letter);

            // Assert
            Assert.AreNotEqual(letter, swapedChar);
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

        [Test]
        public void Validate_ReturnTrue_GivenValidMoves()
        {
            Game game = new Game();
            char[,] b = { { 'a', 'b' } };
            game.gs.FirstMove = false; // Make this move NOT the first move

            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = (char[,])_mockBoard.Clone();
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "V";                     // simulate vertical movement
            mr.Fixed = 8;                           // col where tiles are placed
            mr.Index.Add(9); mr.Index.Add(11);      // row where tiles are placed

            // Set up an updated mockBoard to simulate that user has formed VALID WORDS in VERTICAL direction
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'N';
            _updateMockBoard[11, 8] = 'T';

            game.moveRecorder = mr;

            Assert.IsTrue(game.Validate(_updateMockBoard));
        }

        [Test]
        [TestCase(0, 0, TW, TestName = "GameController_UpdateColor_GivenPosition(0,0)")]
        [TestCase(1, 1, DW, TestName = "GameController_UpdateColor_GivenPosition(1, 1)")]
        [TestCase(0, 3, DL, TestName = "GameController_UpdateColor_GivenPosition(0, 3)")]
        [TestCase(1, 5, TL, TestName = "GameController_UpdateColor_GivenPosition(1, 5)")]
        [TestCase(7, 7, ST, TestName = "GameController_UpdateColor_GivenPosition(7, 7)")]
        [TestCase(0, 1, __, TestName = "GameController_UpdateColor_GivenPosition(0, 1)")]
        public void UpdateColor_ReturnColor_GivenValidPosition(int row, int col, TileType type)
        {
            // Arrange
            SolidColorBrush expectedColor;
            switch (type)
            {
                case TileType.WordTriple:
                    expectedColor = Brushes.OrangeRed;
                    break;
                case TileType.WordDouble:
                    expectedColor = Brushes.Coral;
                    break;
                case TileType.LetterDouble:
                    expectedColor = Brushes.LightSkyBlue;
                    break;
                case TileType.LetterTriple:
                    expectedColor = Brushes.MediumBlue;
                    break;
                case TileType.Start:
                    expectedColor = Brushes.Gold;
                    break;
                default:
                    expectedColor = Brushes.Bisque;
                    break;
            }

            Game game = new Game();
            SolidColorBrush actualColor = game.UpdateColor(row, col);
            Assert.AreEqual(expectedColor, actualColor);
        }
    }
}
