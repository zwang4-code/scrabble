using NUnit.Framework;
using Scrabble.Model.Word;
using Scrabble.Model;
using System;

namespace ScrabbleTest
{
    public class MoveValidatorTests
    {
        // Set up a mock Board to be used in some unit tests
        private char[,] _mockBoard;

        // Set up a game state to be used in some unit tests
        private GameState _mockGameState;

        // Set up a mock move recorder to be used in some unit tests
        private MoveRecorder _mockMoveRecorder;

        [SetUp]
        public void Setup()
        {
            // Place 7 pieces on the mock board (G is placed at the center).
            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O
             *          D  I  N
             ***************************/
            _mockBoard = new char[15, 15];
            _mockBoard[7, 7] = 'G';
            _mockBoard[8, 7] = 'O';
            _mockBoard[9, 7] = 'O';
            _mockBoard[10, 7] = 'D';
            _mockBoard[8, 8] = 'K';
            _mockBoard[10, 8] = 'I';
            _mockBoard[10, 9] = 'N';

            // Fill the GameState.BoardChar() with the mockBoard 
            _mockGameState = new GameState();
            _mockGameState.BoardChar = (char[,])_mockBoard.Clone();
            _mockGameState.FirstMove = false;

            _mockMoveRecorder = new MoveRecorder();
        }

        [Test]
        [TestCase(6, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(6, 7)")]
        [TestCase(7, 6, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(7, 6)")]
        [TestCase(8, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_FirstMoveNotInCenter(8, 7)")]
        public static bool CorrectMove_ReturnFalse_FirstMoveNotInCenter(int firstMoveRow, int firstMoveCol)
        {
            // Arrange: Create a new game state object with an empty board 
            GameState gs = new GameState();

            // Make the first move OFF center
            MoveRecorder mr = new MoveRecorder();
            mr.Record(firstMoveRow, firstMoveCol);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        [TestCase(0, 0, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_HorizontalMoves(0, 0)")]
        [TestCase(5, 8, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_HorizontalMoves(5, 8)")]
        [TestCase(13, 7, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_HorizontalMoves(10, 7)")]
        public bool CorrectMove_ReturnTrue_Valid_Consecutive_HorizontalMoves(int startRow, int startCol)
        {
            // Arrange: Simulate VALID moves that DO NOT OVERLAP with current _mockBoard 
            MoveRecorder mr = new MoveRecorder();
            mr.Record(startRow, startCol);
            mr.Record(startRow, startCol += 1);
            mr.Record(startRow, startCol += 1);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, _mockGameState);
        }

        [Test]
        [TestCase(7, 6, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_HorizontalMoves(7, 6)")]
        [TestCase(10, 8, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_HorizontalMoves(10, 8)")]
        [TestCase(8, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_HorizontalMoves(8, 7)")]
        public bool CorrectMove_ReturnFalse_InValid_Consecutive_HorizontalMoves(int startRow, int startCol)
        {
            // Arrange: Simulate INVALID moves that OVERLAPS with current _mockBoard 
            MoveRecorder mr = new MoveRecorder();
            mr.Record(startRow, startCol);
            mr.Record(startRow, startCol += 1);
            mr.Record(startRow, startCol += 1);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, _mockGameState);
        }

        [Test]
        [TestCase(0, 0, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_VerticalMoves(0, 0)")]
        [TestCase(7, 3, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_VerticalMoves(7, 3)")]
        [TestCase(6, 12, ExpectedResult = true, TestName = "CorrectMove_ReturnTrue_Valid_Consecutive_VerticalMoves(6, 12)")]
        public bool CorrectMove_ReturnTrue_Valid_Consecutive_VerticalMoves(int startRow, int startCol)
        {
            // Arrange: Simulate VALID moves that DO NOT OVERLAP with current _mockBoard 
            MoveRecorder mr = new MoveRecorder();
            mr.Record(startRow, startCol);
            mr.Record(startRow += 1, startCol);
            mr.Record(startRow += 1, startCol);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, _mockGameState);
        }

        [Test]
        [TestCase(7, 7, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_VerticallMoves(7, 7)")]
        [TestCase(10, 9, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_VerticallMoves(10, 9)")]
        [TestCase(8, 9, ExpectedResult = false, TestName = "CorrectMove_ReturnFalse_InValid_Consecutive_VerticallMoves(8, 9)")]
        public bool CorrectMove_ReturnFalse_InValid_Consecutive_VerticallMoves(int startRow, int startCol)
        {
            // Arrange: Simulate INVALID moves that OVERLAPS with current _mockBoard 
            MoveRecorder mr = new MoveRecorder();
            mr.Record(startRow, startCol);
            mr.Record(startRow += 1, startCol);
            mr.Record(startRow += 1, startCol);

            // Act and Assert
            return MoveValidator.CorrectMove(mr, _mockGameState);
        }

        [Test]
        public static void CorrectMove_ReturnTrue_MakeOneMove_InCenter_AsFirstMove()
        {
            // Arrange: make first and only move at center
            GameState gs = new GameState();
            MoveRecorder mr = new MoveRecorder();
            int centerRow = 7, centerCol = 7;
            mr.Record(centerRow, centerCol);

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
            mr.Record(startRow += skipFactor, startCol);     // Make a skip move verticlly

            // Act and Assert
            return MoveValidator.CorrectMove(mr, gs);
        }

        [Test]
        public void Validate_Return_Negative_1_GivenIncorrectMove()
        {
            // Arrange: Create a gamestate with an empty board
            GameState gs = new GameState();
            char[,] emptyBoard = new char[15, 15];

            // Make CorrectMove() method return false by making the first move off center
            MoveRecorder mr = new MoveRecorder();
            mr.Record(6, 6);

            // Act
            int result = MoveValidator.Validate(gs, emptyBoard, mr);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Validate_Return_Negative_1_GivenUnchangedBoard()
        {
            // Arrange: use _mockGameState and _mockMovementRecorder and do not make any change to the BoardChar

            // Act
            int result = MoveValidator.Validate(_mockGameState, _mockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Validate_Return_Sum_Given_Vertical_ValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "V";                     // simulate vertical movement
            _mockMoveRecorder.Fixed = 8;                           // col where tiles are placed
            _mockMoveRecorder.Index.Add(9);         // row where tiles are placed
            _mockMoveRecorder.Index.Add(11);

            // Set up an updated mockBoard to simulate that user has formed VALID WORDS in VERTICAL direction
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'N';
            _updateMockBoard[11, 8] = 'T';

            // Calculate expected score: [K + N + I + T] + [O + N] 
            int expected = (5 + 1 + 1 + 1) + (1 + 1);

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  N
             *          D  I  N
             *             T
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_Vertical_InValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "V";          // simulate vertical movement
            _mockMoveRecorder.Fixed = 8;                // col where tiles are placed 
            _mockMoveRecorder.Index.Add(9);             // row where tiles are placed
            _mockMoveRecorder.Index.Add(11);

            // Set up an updated mockBoard to simulate that user has formed INAVLID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'C';
            _updateMockBoard[11, 8] = 'Z';

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  C
             *          D  I  N
             *             Z
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void Validate_Return_Sum_Given_Horizontal_ValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "H";            // simulate horizontal movement
            _mockMoveRecorder.Fixed = 9;                  // row where tiles are placed
            _mockMoveRecorder.Index.Add(8);               // col where tiles are placed
            _mockMoveRecorder.Index.Add(9);

            // Set up an updated mockBoard to simulate that user has formed VALID WORDS in HORIZONTAL direction
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'H';
            _updateMockBoard[9, 9] = 'O';

            // Calculate expected score: [O + H + O * 3] + [K * 2 + H + I] + [O * 3 + N]
            int expected = (1 + 4 + 1 * 3) + (5 * 2 + 4 + 1) + (1 * 3 + 1);

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  H  O
             *          D  I  N
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_Horizontal_InValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "H";          // simulate horizontal movement
            _mockMoveRecorder.Fixed = 9;                // row where tiles are placed
            _mockMoveRecorder.Index.Add(8);             // col where tiles are placed
            _mockMoveRecorder.Index.Add(9);

            // Set up an updated mockBoard to simulate that user has formed INVALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'X';
            _updateMockBoard[9, 9] = 'Z';

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  X  Z
             *          D  I  N
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void Validate_Return_Sum_Given_SingleMove_No_Direction_ValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "N";                     // simulate NO direction as user adds ONLY ONE TILE
            Tuple<int, int> location = new Tuple<int, int>(9, 8);
            _mockMoveRecorder.Moves.Add(location);                 // position where tile is placed


            // Set up an updated mockBoard to simulate that user has formed VALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'H';

            // Calculate expected score: [O + H] + [K + H + I]
            int expected = (1 + 4) + (5 + 4 + 1);

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  H
             *          D  I  N
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Validate_Return_Negative_1_Given_SingleMove_InValidWord()
        {
            // Arrange
            // Set movement directions and locations
            _mockMoveRecorder.Direction = "N";                     // simulate NO direction as user adds ONLY ONE TILE
            Tuple<int, int> location = new Tuple<int, int>(9, 8);
            _mockMoveRecorder.Moves.Add(location);                 // position where tile is placed


            // Set up an updated mockBoard to simulate that user has formed INVALID WORDS
            char[,] _updateMockBoard = new char[15, 15];
            _updateMockBoard = (char[,])_mockBoard.Clone();
            _updateMockBoard[9, 8] = 'V';

            /* BOARD VIEW: 
             * *************************
             *          G
             *          O  K
             *          O  V
             *          D  I  N
             ***************************/

            // Act
            int actual = MoveValidator.Validate(_mockGameState, _updateMockBoard, _mockMoveRecorder);

            // Assert
            Assert.AreEqual(-1, actual);
        }
    }
}