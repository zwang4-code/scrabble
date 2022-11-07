using NUnit.Framework;
using Scrabble.Model.Word;
using Scrabble.Model;
using System;

namespace UnitTests
{
    public class MoveValidatorTests
    {
        // Set up a mock Board
        private char[,] _mockBoard = new char[15, 15];

        [SetUp]
        public void Setup()
        {
            // Place 5 pieces on the board
            _mockBoard[7, 7] = 'G';
            _mockBoard[8, 7] = 'O';
            _mockBoard[9, 7] = 'O';
            _mockBoard[10, 7] = 'D';
            _mockBoard[10, 8] = 'O';
        }

        [Test]
        [TestCase(6, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(6, 7)")]
        [TestCase(7, 6, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(7, 6)")]
        [TestCase(8, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(8, 7)")]
        public static bool CorrectMove_ReturnFalse_FirstMoveNotInCenter(int row, int col)
        {
            // Arrange
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(row, col);  // make first move

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        [TestCase(1, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveHorizontalMoves(1)")]
        [TestCase(-1, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveHorizontalMoves(-1)")]
        public static bool CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveHorizontalMoves(int increment)
        {
            // Arrange: make first move at center
            int row = 7, col = 7;
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(row, col);

            // Arrange: make consecutive horizontal moves from center
            // If increment == 1, means moving to the right
            // If increment == -1, means moving to the left
            mr.Record(row, col += increment); 
            mr.Record(row, col += increment);
            mr.Record(row, col += increment);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        [TestCase(1, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveVerticallMoves(1)")]
        [TestCase(-1, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveVerticallMoves(-1)")]
        public static bool CorrectMove_ReturnTrue_FirstMoveInCenter_ThenConsecutiveVerticallMoves(int increment)
        {
            // Arrange: make first move at center
            int row = 7, col = 7;
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(row, col);

            // Arrange: make consecutive vertical moves from center
            // If increment == 1, means moving down
            // If increment == -1, means moving up
            mr.Record(row += increment, col);
            mr.Record(row += increment, col);
            mr.Record(row += increment, col);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        public static void CorrectMove_ReturnTrue_MakeOneMove_InCenter_AsFirstMove()
        {
            // Arrange: make first and only move at center
            int row = 7, col = 7;
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(row, col);

            // Act
            bool result = MoveValidator.CorrectMove(mr, gs);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(0, 0, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_MakeOneMove_NotInCenter_NotAsFirstMove(0, 0)")]
        [TestCase(4, 8, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_MakeOneMove_NotInCenter_NotAsFirstMove(4, 8)")]
        [TestCase(14, 14, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_MakeOneMove_NotInCenter_NotAsFirstMove(14, 14)")]
        public static bool CorrectMove_ReturnTrue_MakeOneMove_NotInCenter_NotAsFirstMove(int row, int col)
        {
            // Arrange
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            gs.FirstMove = false;   // Make this move NOT the first move
            mr.Record(row, col);    // Make One move 

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        [TestCase(4, 10, -2, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(4, 10, -2)")]
        [TestCase(9, 2, 3, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(9, 2, 3)")]
        [TestCase(0, 0, 2, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(0, 0, 2)")]
        public static bool CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(int startRow, int startCol, int skipFactor)
        {
            // Arrange
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            gs.FirstMove = false;                           // Make this move NOT the first move
            mr.Record(startRow, startCol);                  // Make one move 
            mr.Record(startRow, startCol += skipFactor);    // Make a skip move horizontally

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        [TestCase(4, 10, -2, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(4, 10, -2)")]
        [TestCase(9, 2, 3, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(9, 2, 3)")]
        [TestCase(0, 0, 2, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_MakeNonConsecutiveHorizontalMoves(0, 0, 2)")]
        public static bool CorrectMove_ReturnFalse_MakeNonConsecutiveVerticallMoves(int startRow, int startCol, int skipFactor)
        {
            // Arrange
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            gs.FirstMove = false;                           // Make this move NOT the first move
            mr.Record(startRow, startCol);                  // Make one move 
            mr.Record(startRow +=skipFactor, startCol);     // Make a skip move verticlly

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        public static void Validate_Return_Negative_1_GivenIncorrectMove()
        {
            // Arrange: Make CorrectMove() method return false by making the first move off center
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            mr.Record(6, 6);  // make first move NOT in center
            char[,] emptyBoard = new char[15, 15];

            // Act
            int result = MoveValidator.Validate(gs, emptyBoard, mr);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Validate_Return_Negative_1_GivenUnchangedBoard()
        {
            // Arrange: Set up a GameState.BoardChar() using the mockBoard to simulate that user
            // did not make any change to the board
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            MoveRecorder mr = new MoveRecorder();  // does not matter here


            // Act
            int result = MoveValidator.Validate(gs, _mockBoard, mr);

            // Assert
            Assert.AreEqual(-1, result);
        }
         
        [Test]
        public void Validate_Return_Sum_Given_Vertical_ValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "V";                     // simulate vertical movement
            mr.Fixed = 8;                           // col where tiles are placed
            mr.Index.Add(9); mr.Index.Add(11);      // row where tiles are placed

            // Set up an updated mockBoard to simulate that user has formed VALID WORDS in VERTICAL direction
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'H';
            _updateMockBoard[11, 8] = 'T';

            // Calculate expected score: [H + O + T] + [O + H] 
            int expected = 4 + 1 + 1 + 1 + 4;

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_Vertical_InValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "V";                 // simulate vertical movement
            mr.Fixed = 8;                       // col where tiles are placed 
            mr.Index.Add(9); mr.Index.Add(11);  // row where tiles are placed

            // Set up an updated mockBoard to simulate that user has formed INAVLID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'X';
            _updateMockBoard[11, 8] = 'Z';

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void Validate_Return_Sum_Given_Horizontal_ValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "H";                     // simulate horizontal movement
            mr.Fixed = 9;                           // row where tiles are placed
            mr.Index.Add(8); mr.Index.Add(9);       // col where tiles are placed
            mr.Index.Add(10); mr.Index.Add(11);

            // Set up an updated mockBoard to simulate that user has formed VALID WORDS in HORIZONTAL direction
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'T';
            _updateMockBoard[9, 9] = 'T';
            _updateMockBoard[9, 10] = 'E';
            _updateMockBoard[9, 11] = 'R';

            // Calculate expected score: [O + T + T * 3 + E + R] + [T + O] 
            int expected = 1 + 1 + 3 + 1 + 1 + 1 + 1;

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_Horizontal_InValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "H";                     // simulate horizontal movement
            mr.Fixed = 9;                           // row where tiles are placed
            mr.Index.Add(8); mr.Index.Add(9);       // col where tiles are placed

            // Set up an updated mockBoard to simulate that user has formed INVALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'X';
            _updateMockBoard[9, 9] = 'Z';

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void Validate_Return_Sum_Given_SingleMove_No_Direction_ValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "N";                     // simulate NO direction as user adds ONLY ONE TILE
            Tuple<int, int> location = new Tuple<int, int>(9, 8);
            mr.Moves.Add(location);                 // position where tile is placed


            // Set up an updated mockBoard to simulate that user has formed VALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'N';

            // Calculate expected score: [N + O] + [O + N]
            int expected = 1 + 1 + 1 + 1;

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_SingleMove_InValidWord()
        {
            // Arrange
            // Set up a GameState.BoardChar() with the mockBoard 
            GameState gs = new GameState();
            gs.BoardChar = _mockBoard;
            gs.FirstMove = false;

            // Set movement directions and locations
            MoveRecorder mr = new MoveRecorder();
            mr.Direction = "N";                     // simulate NO direction as user adds ONLY ONE TILE
            Tuple<int, int> location = new Tuple<int, int>(9, 8);
            mr.Moves.Add(location);                 // position where tile is placed


            // Set up an updated mockBoard to simulate that user has formed INVALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'Z';

            // Act
            int actual = MoveValidator.Validate(gs, _updateMockBoard, mr);

            // Assert
            Assert.AreEqual(-1, actual);
        }
    }
}