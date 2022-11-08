using NUnit.Framework;
using Scrabble;

namespace ScrabbleTest

{
    public class BoardTileTest
    {
        private const TileType __ = TileType.Default;
        private const TileType DL = TileType.LetterDouble;
        private const TileType TL = TileType.LetterTriple;
        private const TileType DW = TileType.WordDouble;
        private const TileType TW = TileType.WordTriple;
        private const TileType ST = TileType.Start;

        private BoardTiles _boardTile;

        [SetUp]
        public void Setup()
        {
            _boardTile = new BoardTiles();
        }

        [Test]
        public void PlaceInUse_ReturnCorrectPlacements_UponContructorCall()
        {
            // Arrange (Also indiretly testing constructor)
            TileType[,] expectedPlacements =
            {
                {TW,__,__,DL,__,__,__,TW,__,__,__,DL,__,__,TW},
                {__,DW,__,__,__,TL,__,__,__,TL,__,__,__,DW,__},
                {__,__,DW,__,__,__,DL,__,DL,__,__,__,DW,__,__},
                {DL,__,__,DW,__,__,__,DL,__,__,__,DW,__,__,DL},
                {__,__,__,__,DW,__,__,__,__,__,DW,__,__,__,__ },
                {__,TL,__,__,__,TL,__,__,__,TL,__,__,__,TL,__},
                {__,__,DL,__,__,__,DL,__,DL,__,__,__,DL,__,__},
                //midDLe
                {TW,__,__,DL,__,__,__,ST,__,__,__,DL,__,__,TL},
                //midDLe
                {__,__,DL,__,__,__,DL,__,DL,__,__,__,DL,__,__},
                {__,TL,__,__,__,TL,__,__,__,TL,__,__,__,TL,__},
                {__,__,__,__,DW,__,__,__,__,__,DW,__,__,__,__ },
                {DL,__,__,DW,__,__,__,DL,__,__,__,DW,__,__,DL},
                {__,__,DW,__,__,__,DL,__,DL,__,__,__,DW,__,__},
                { __,DW,__,__,__,TL,__,__,__,TL,__,__,__,DW,__},
                {TW,__,__,DL,__,__,__,TW,__,__,__,DL,__,__,TW}
            };

            // Act 
            TileType[,] actualPlacements = _boardTile.PlaceInUse;

            // Assert
            Assert.AreEqual(expectedPlacements, actualPlacements);
        }

        /// <summary>
        /// Test random positions on the board to return correct word multiplier number.
        /// </summary>
        /// <param name="i">The row number of the position under test.</param>
        /// <param name="j">The col number of the position under test.</param>
        /// <returns>Word multiplier number.</returns>
        [Test]
        [TestCase(0, 0, ExpectedResult = 3, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(0, 0)")]
        [TestCase(0, 7, ExpectedResult = 3, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(0, 7)")]
        [TestCase(14, 14, ExpectedResult = 3, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(14, 14)")]
        [TestCase(1, 1, ExpectedResult = 2, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(1, 1)")]
        [TestCase(4, 4, ExpectedResult = 2, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(4, 4)")]
        [TestCase(13, 1, ExpectedResult = 2, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(13, 1)")]
        [TestCase(7, 7, ExpectedResult = 1, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(7, 7)")]
        [TestCase(5, 0, ExpectedResult = 1, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(5, 0)")]
        [TestCase(12, 10, ExpectedResult = 1, TestName = "WordMultiplier_ReturnCorrectNum_GivenValidPosition(12, 10)")]
        public int WordMultiplier_ReturnCorrectNum_GivenValidPosition(int i, int j)
        {
            return _boardTile.WordMultiplier(i, j);
        }

        [Test]
        [TestCase(1, 5, ExpectedResult = 3, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(1, 5)")]
        [TestCase(5, 9, ExpectedResult = 3, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(5, 9)")]
        [TestCase(13, 9, ExpectedResult = 3, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(13, 9)")]
        [TestCase(0, 3, ExpectedResult = 2, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(0, 3)")]
        [TestCase(8, 6, ExpectedResult = 2, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(8, 6)")]
        [TestCase(14, 11, ExpectedResult = 2, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(14, 11)")]
        [TestCase(7, 7, ExpectedResult = 1, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(7, 7)")]
        [TestCase(5, 0, ExpectedResult = 1, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(5, 0)")]
        [TestCase(12, 10, ExpectedResult = 1, TestName = "LetterMultiplier_ReturnCorrectNum_GivenValidPosition(12, 10)")]
        public int LetterMultiplier_ReturnCorrectNum_GivenValidPosition(int i, int j)
        {
            return _boardTile.LetterMultiplier(i, j);
        }

        /// <summary>
        /// This is a special unit test. The method that is being tested (ApplyVisited) is void, so we cannot verify it 
        /// except the fact that we made it through the method successfully. The goal of this unit test is to 
        /// call the method: if we get through the unit test, it means we made it through the method successfully.
        /// </summary>
        [Test]
        public void ApplyVisited_ShouldPass_WhenCalled()
        {
            // Arrange

            // Act
            _boardTile.ApplyVisited();

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// This is a special unit test. The method that is being tested (CleanVisited) is void, so we cannot verify it 
        /// except the fact that we made it through the method successfully. The goal of this unit test is to 
        /// call the method: if we get through the unit test, it means we made it through the method successfully.
        /// </summary>
        [Test]
        public void CleanVisited_ShouldPass_WhenCalled()
        {
            // Arrange

            // Act
            _boardTile.CleanVisited();

            // Assert
            Assert.IsTrue(true);
        }
    }
}