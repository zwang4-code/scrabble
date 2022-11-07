using NUnit.Framework;
using Scrabble.Model.Word;
using Scrabble.Model;
using System;

namespace UnitTests
{
    public class MoveValidatorTests
    {
        [SetUp]
        public void Setup()
        {

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
    }
}