using NUnit.Framework;
using Scrabble.Model;
using System;

namespace UnitTests
{
    public class TilesTest
    {
        // Global tile object used for test cases
        private Tile _tile;

        /// <summary>
        /// Set up: Arrange a default Tile object to be used in some test cases.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Default tile char and score are arbitrarily set to be invalid
            _tile = new Tile('*', 1000);
        }

        [Test]
        [TestCase('-', 0, ExpectedResult = '-', TestName = "TileChar_Get_ReturnCorrectChar_GivenValidCharAndScore('-', 0)")]
        [TestCase('E', 1, ExpectedResult = 'E', TestName = "TileChar_Get_ReturnCorrectChar_GivenValidCharAndScore('E', 1)")]
        [TestCase('Z', 10, ExpectedResult = 'Z', TestName = "TileChar_Get_ReturnCorrectChar_GivenValidCharAndScore('Z', 10)")]
        public char TileChar_Get_ReturnCorrectChar_GivenValidCharAndScore(char c, int s)
        {
            // Arrange (Also indiretly testing constructor)
            Tile tile = new Tile(c, s);

            // Act and Assert
            return tile.TileChar;
        }

        [Test]
        [TestCase('-', ExpectedResult = '-', TestName = "TileChar_Set_ReturnCorrectChar_GivenValidChar('-')")]
        [TestCase('E', ExpectedResult = 'E', TestName = "TileChar_Set_ReturnCorrectChar_GivenValidChar('E')")]
        [TestCase('Z', ExpectedResult = 'Z', TestName = "TileChar_Set_ReturnCorrectChar_GivenValidChar('Z')")]
        public char TileChar_Set_ReturnCorrectChar_GivenValidChar(char c)
        {
            // Act (using tile created in Setup())
            _tile.TileChar = c;

            // Assert
            return _tile.TileChar;
        }

        [Test]
        [TestCase('-', 0, ExpectedResult = 0, TestName = "TileScore_Get_ReturnCorrectScore_GivenValidCharAndScore('-', 0)")]
        [TestCase('E', 1, ExpectedResult = 1, TestName = "TileScore_Get_ReturnCorrectScore_GivenValidCharAndScore('E', 1)")]
        [TestCase('Z', 10, ExpectedResult = 10, TestName = "TileScore_Get_ReturnCorrectScore_GivenValidCharAndScore('Z', 10)")]
        public int TileScore_Get_ReturnCorrectScore_GivenValidCharAndScore(char c, int s)
        {
            // Arrange (Also indiretly testing constructor)
            Tile tile = new Tile(c, s);

            // Act and Assert
            return tile.TileScore;
        }

        [Test]
        [TestCase(0, ExpectedResult = 0, TestName = "TileScore_Set_ReturnCorrectScore_GivenValidScore(0)")]
        [TestCase(1, ExpectedResult = 1, TestName = "TileScore_Set_ReturnCorrectScore_GivenValidScore(1)")]
        [TestCase(10, ExpectedResult = 10, TestName = "TileScore_Set_ReturnCorrectScore_GivenValidScore(10)")]
        public int TileScore_Set_ReturnCorrectScore_GivenValidScore(int s)
        {
            // Act (using tile created in Setup())
            _tile.TileScore = s;

            // Assert
            return _tile.TileScore;
        }

        [Test]
        public void CompareTo_Return_1_GivenNullObj()
        {
            // Act
            int result = _tile.CompareTo(null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        [TestCase('A', 'A', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('A','A')")]
        [TestCase('B', 'B', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('B','B')")]
        [TestCase('Z', 'Z', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('Z','Z')")]
        [TestCase('A', 'Z', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('A','Z')")]
        [TestCase('Z', 'A', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('Z','A')")]
        [TestCase('O', 'Q', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('O','Q')")]
        [TestCase('Q', 'O', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('Q','O')")]
        [TestCase('E', 'F', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('E','F')")]
        [TestCase('F', 'E', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj('F','E')")]
        public void CompareTo_ReturnCharSubtractionResult_GivenValidTileObj(char originalChar, char otherChar)
        {
            // Arrange (score parameter does not matter here)
            Tile originalTile = new Tile(originalChar, 777);
            Tile otherTile = new Tile(otherChar, 999);

            // Act
            int result = originalTile.CompareTo(otherTile);

            // Assert
            Assert.AreEqual(originalChar - otherChar, result);
        }

        [Test]
        [TestCase("a String", TestName = "CompareTo_ThrowException_GivenNonTileObject('a String')")]
        [TestCase('r', TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj(')")]
        [TestCase(999, TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj(999)")]
        [TestCase(-3.14, TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj(-3.14)")]
        [TestCase(true, TestName = "CompareTo_ReturnCharSubtractionResult_GivenValidTileObj(true)")]
        public void CompareTo_ThrowException_GivenNonTileObject(object nonTileObj)
        {
            Assert.Throws<ArgumentException>(() => _tile.CompareTo(nonTileObj), "Tiles Comparison Exception");
        }
    }
}