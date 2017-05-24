﻿namespace GTO.Model.Context
{
    public partial class GtoEventPlayerRecord
    {
        public string EventTestName
        {
            get { return GtoEventTest != null ? GtoEventTest.TestName : string.Empty; }
        }
        public string EventTestJudgeName
        {
            get { return GtoEventTest != null ? GtoEventTest.JudgeName : string.Empty; }
        }
        public string EventTestPlayerName
        {
            get { return GtoEventPlayer != null ? GtoEventPlayer.PlayerName : string.Empty; }
        }
    }
}