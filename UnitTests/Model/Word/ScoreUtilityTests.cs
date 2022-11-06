using NUnit.Framework;
using Scrabble.Model.Word;
using Scrabble.Model;

namespace UnitTests
{
    public class ScoreUtilityTests
    {
        [Test]
        [TestCase(0,0, ExpectedResult = 9, TestName = "ScoreCalc_ReturnValidScore_GivenWordVertically(0,0)")]
        [TestCase(1,1, ExpectedResult = 6, TestName = "ScoreCalc_ReturnValidScore_GivenWordVertically(1,1)")]
        public static int ScoreCalc_ReturnValidScore_GivenWordVertically(int i, int j)
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            bc[i, j] = 'G';
            bc[i + 1, j] = 'O';
            bc[i + 2, j] = 'O';
            bc[i + 3, j] = 'D';

            int result = ScoreUtility.ScoreCalc(j, i, i+1, "v", bc, gs.boardTiles);
            return result;
        }
               
        [Test]
        [TestCase(0, 0, ExpectedResult = 9, TestName = "ScoreCalc_ReturnValidScore_GivenWordHorizontally(0,0)")]
        [TestCase(1, 1, ExpectedResult = 6, TestName = "ScoreCalc_ReturnValidScore_GivenWordHorizontally(1,1)")]
        public static int ScoreCalc_ReturnValidScore_GivenWordHorizontally(int i, int j)
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            bc[i, j] = 'G';
            bc[i , j + 1] = 'O';
            bc[i, j + 2] = 'O';
            bc[i, j + 3] = 'D';

            int result = ScoreUtility.ScoreCalc(j, i, i + 1, "h", bc, gs.boardTiles);
            return result;
        }
    }
}