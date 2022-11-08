using NUnit.Framework;
using Scrabble.Model;
using System.Collections.Generic;

namespace ScrabbleTest
{
    public class GameEndVerifyTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameEndVerify_TilebagLessThanSeven_ShouldPass()
        {
            GameState gs = new GameState();
            gs.TilesBag.MakeTiles();

            bool result = GameEndVerify.TilebagLessThanSeven(gs);
            Assert.IsFalse(result);
        }

        private static IEnumerable<TestCaseData> AddTilesConfig()
        {
            yield return new TestCaseData('A', 12);
            yield return new TestCaseData('B', 11);
        }

        [Test, TestCaseSource("AddTilesConfig")]
        public void GameEndVerify_ExistsPlayerNoTiles_AllPlayersHaveTiles_ExpectFail(char letter, int tilevalue)
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tilevalue);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            Tile tile2 = new Tile(letter, tilevalue);
            p2.PlayingTiles.Add(tile2);

            gameState.ListOfPlayers.Add(p1);
            gameState.ListOfPlayers.Add(p2);

            bool result = GameEndVerify.ExistsPlayerNoTiles(gameState);
            Assert.IsFalse(result);
        }

        [Test, TestCaseSource("AddTilesConfig")]
        public void GameEndVerify_ExistsPlayerNoTiles_OnePlayersHaveNoTiles_ExpectFail(char letter, int tilevalue)
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile(letter, tilevalue);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();

            gameState.ListOfPlayers.Add(p1);
            gameState.ListOfPlayers.Add(p2);

            bool result = GameEndVerify.ExistsPlayerNoTiles(gameState);
            Assert.IsTrue(result);
        }

        [Test]
        public void GameEndVerify_GameEndScoring_ExpectPass()
        {
            GameState gs = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            gs.ListOfPlayers.Add(p1);
            gs.ListOfPlayers.Add(p2);


            foreach (Player p in gs.ListOfPlayers)
            {

                foreach (Tile t in p.PlayingTiles)
                {
                    p1.Score += AllTiles.ScoreOfLetter(t.TileChar);
                    p2.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                }

            }

            gs.TilesBag.ListTiles.RemoveRange(5, 94);

            bool result = GameEndVerify.GameEndScoring(gs);
            Assert.IsTrue(result);
        }

        [Test]
        public void GameEndVerify_GameEndScoring_TilebagLessThanSeven_And_ExistsPlayerNoTiles_ExpectPass()
        {
            GameState gs = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            Player p3 = new Player();


            gs.ListOfPlayers.Add(p1);
            gs.ListOfPlayers.Add(p2);
            gs.ListOfPlayers.Add(p3);

            foreach (Player p in gs.ListOfPlayers)
            {

                foreach (Tile t in p.PlayingTiles)
                {
                    p1.Score += AllTiles.ScoreOfLetter(t.TileChar);
                    p2.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                }

            }

            gs.TilesBag.ListTiles.RemoveRange(5, 94);

            bool result = GameEndVerify.GameEndScoring(gs);
            Assert.IsTrue(result);
        }

        [Test]
        public void GameEndVerify_GameEndScoring_ExpectFail()
        {
            GameState gs = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            Player p3 = new Player();


            gs.ListOfPlayers.Add(p1);
            gs.ListOfPlayers.Add(p2);
            gs.ListOfPlayers.Add(p3);

            foreach (Player p in gs.ListOfPlayers)
            {
                foreach (Tile t in p.PlayingTiles)
                {
                    p1.Score += AllTiles.ScoreOfLetter(t.TileChar);
                    p2.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                }

            }

            bool result = GameEndVerify.GameEndScoring(gs);
            Assert.IsFalse(result);
        }
    }
}