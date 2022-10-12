using NUnit.Framework;
using Scrabble.Model;

namespace UnitTests
{
    public class AllTilesTest
    {
        // Global AllTiles object to use for all non-static method test cases. 
        private AllTiles _tiles;

        [SetUp]
        public void Setup()
        {
            // Arrange for all non-static method test cases.
            _tiles = new AllTiles();
        }

        [Test]
        public void Empty_ReturnFalse_ConstructorCallsMakeTiles()
        {
            // Act
            var result = _tiles.Empty();

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Empty_ReturnTrue_ManuallyEmptyListTiles()
        {
            // Act
            _tiles.ListTiles.Clear();
            var result = _tiles.Empty();

            // Assert
            Assert.AreEqual(true, result);
        }


        [Test]
        [TestCase('A', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('A')")]
        [TestCase('B', ExpectedResult = 3, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('B')")]
        [TestCase('C', ExpectedResult = 3, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('C')")]
        [TestCase('D', ExpectedResult = 2, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('D')")]
        [TestCase('E', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('E')")]
        [TestCase('F', ExpectedResult = 4, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('F')")]
        [TestCase('G', ExpectedResult = 2, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('G')")]
        [TestCase('H', ExpectedResult = 4, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('H')")]
        [TestCase('I', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('I')")]
        [TestCase('J', ExpectedResult = 8, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('J')")]
        [TestCase('K', ExpectedResult = 5, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('K')")]
        [TestCase('L', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('L')")]
        [TestCase('M', ExpectedResult = 3, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('M')")]
        [TestCase('N', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('N')")]
        [TestCase('O', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('O')")]
        [TestCase('P', ExpectedResult = 3, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('P')")]
        [TestCase('Q', ExpectedResult = 10, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('Q')")]
        [TestCase('R', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('R')")]
        [TestCase('S', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('S')")]
        [TestCase('T', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('T')")]
        [TestCase('U', ExpectedResult = 1, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('U')")]
        [TestCase('V', ExpectedResult = 4, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('V')")]
        [TestCase('W', ExpectedResult = 4, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('W')")]
        [TestCase('X', ExpectedResult = 8, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('X')")]
        [TestCase('Y', ExpectedResult = 4, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('Y')")]
        [TestCase('Z', ExpectedResult = 10, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('Z')")]
        public int ScoreOfLetter_ReturnCorrectScore_GivenValidLetter(char c)
        {
            return AllTiles.ScoreOfLetter(c);
        }

        [Test]
        [TestCase('-', ExpectedResult = 0, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('-')")]
        public int ScoreOfLetter_ReturnZero_GivenDash(char c)
        {
            return AllTiles.ScoreOfLetter(c);
        }

        [Test]
        [TestCase('&', ExpectedResult = 0, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('&')")]
        [TestCase('!', ExpectedResult = 0, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter('!')")]
        [TestCase(' ', ExpectedResult = 0, TestName = "scoreOfLetter_ReturnCorrectScore_GivenValidLetter(' ')")]
        public int ScoreOfLetter_ReturnZero_GivenInvalidChar(char c)
        {
            return AllTiles.ScoreOfLetter(c);
        }

        /// <summary>
        /// Indirectly tests NumOfLetters method.
        /// After MakeTiles is called, a total of 98 letter tiles + 2 dash tiles will be added to ListTiles.
        /// </summary>
        [Test]
        public void ListTiles_CountIs_100_ConstructorCallsMakeTiles()
        {
            // Act: total_letter_tiles = sum of { NumOfLetters * how_many_letters }
            var total_letter_tiles = 1 * 5 + 2 * 9 + 3 * 1 + 4 * 4 + 6 * 3 + 8 * 1 + 9 * 2 + 12 * 1;
            var total_dash_tiles = 2 * 1;
            var total = total_letter_tiles + total_dash_tiles;

            // Assert
            Assert.AreEqual(total, _tiles.ListTiles.Count);
        }
    }
}