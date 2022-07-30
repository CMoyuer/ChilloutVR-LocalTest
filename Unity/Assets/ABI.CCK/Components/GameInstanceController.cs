using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABI.CCK.Components
{
    public class GameInstanceController: MonoBehaviour
    {
        public string referenceID;
        
        public List<Team> teams = new List<Team>();

        public bool balanceTeamJoin = false;
        public bool autoBalanceTeams = false;

        public float readyPercentage = 0.5f;
        public int readyTimer = 60;

        public enum GameControllerType
        {
            Default = 0,
            CombatSystemController = 128
        }

        public GameControllerType gameControllerType = GameControllerType.Default;
        
        public enum GameType
        {
            Single = 0,
            Rounds = 1,
        }

        public GameType gameType = GameType.Single;
        public int roundsToWin = 1;

        public enum EndCondition
        {
            Score = 0,
            Time = 1,
            TimeAndScore = 2,
        }

        public EndCondition endCondition = EndCondition.Score;
        public int endScore = 0;
        public int endTime = 0;

        public UnityEvent gameStarted = new UnityEvent();
        public UnityEvent roundStarted = new UnityEvent();
        public UnityEvent gameEnded = new UnityEvent();
        public UnityEvent roundEnded = new UnityEvent();
        
        private void Reset()
        {
            referenceID = Guid.NewGuid().ToString();
        }

        public void TryJoinTeam(int teamIndex)
        {
            
        }

        public void JoinTeamAutoBalance()
        {
            
        }

        public void LeaveTeam()
        {
            
        }

        public void StartGame()
        {
            
        }

        public void ToggleReady()
        {
            
        }

        public void OwnScore(int score)
        {
            
        }
        
        public void EnemyScore(int score)
        {
            
        }
    }

    [System.Serializable]
    public class Team
    {
        public int index = 0;
        public string name;
        public Color color;
        public int playerLimit = 0;
        public int startingScore = 0;
        public UnityEvent teamJoinedEvent = new UnityEvent();
        public UnityEvent teamLeaveEvent = new UnityEvent();
        public UnityEvent teamWinRoundEvent = new UnityEvent();
        public UnityEvent teamWinGameEvent = new UnityEvent();
    }
}