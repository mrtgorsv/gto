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

        public string ResultRankName
        {
            get
            {
                switch (ResultRank)
                {
                    case Enums.ResultRank.Gold:
                        return "золото";
                    case Enums.ResultRank.Serebro:
                        return "серебро";
                    case Enums.ResultRank.Bronze:
                        return "бронза";
                    default:
                        return "без медали";
                }
            }
        }
    }
}