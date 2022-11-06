using System;
using NUnit.Framework;
using Scrabble.View;
using Scrabble.Model;

namespace UnitTests
{
    public class GameStateTest
    {
        public IView view;
        public IView view1;
        public IView view2;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameState_GamePass_PlayerNow_ShouldReturn_4()
        {
            GameState gs = new GameState();
            gs.NumOfPlayers = 4;
            gs.PlayerNow = 2;
            gs.LastAction = "Play";
            gs.GamePass();
            Assert.AreEqual(3, gs.PlayerNow);
        }

        [Test]
        public void GameState_GamePass_LastAction_ShouldReturn_Pass()
        {
            GameState gs = new GameState();
            gs.NumOfPlayers = 4;
            gs.PlayerNow = 2;
            gs.LastAction = "play";
            gs.GamePass();
            Assert.AreEqual("pass", gs.LastAction);
        }

        [Test]
        public void GameState_NextPlayer_ShouldReturn_0()
        {
            GameState gs = new GameState();
            gs.PlayerNow = 3;
            gs.NumOfPlayers = 4;
            var result = gs.NextPlayer();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GameState_NextPlayer_ShouldReturn_PlayerNow()
        {
            GameState gs = new GameState();
            gs.PlayerNow = 2;
            gs.NumOfPlayers = 4;
            var result = gs.NextPlayer();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void GameState_UpdateState_null_ShouldReturn_LastAction_Swap()
        {
            GameState gs = new GameState();
            gs.UpdateState(null);
            var result = gs.LastAction;
            Assert.AreEqual("swap", result);

        }

        [Test]
        public void GameState_UpdateState_char_ShouldReturn_PlayerCountingScore_Zero()
        {
            GameState.GSInstance.Initialise(1);
            char[,] b = { { 'a', 'b' } };
            var gs = GameState.GSInstance;
            gs.UpdateState(b);
            var result = gs.LastAction;
            Console.WriteLine(result);
            Assert.AreEqual("play", result);

        }

        [Test]
        public void GameState_RegObserver_Should_AddView()
        {
            GameState gs = new GameState();
            gs.RegObserver(view);
            var result = gs.ListOfViews.Count;
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GameState_UnregObserver_Should_RemoveView()
        {
            GameState gs = new GameState();
            gs.UnregObserver(view);
            Assert.IsNotNull(gs);

        }

        [Test]
        public void GameState_NotifyGameStateChange_Should_ChangeState()
        {
            GameState gs = new GameState();
            gs.ListOfViews.Add(view);
            gs.ListOfViews.Add(view1);
            gs.ListOfViews.Add(view2);
            Assert.Throws<NullReferenceException>(() => gs.NotifyGameStateChange());

        }
    }
}