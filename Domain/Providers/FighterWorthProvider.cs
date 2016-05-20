using Domain.Models;

namespace Domain.Providers
{
    public static class FighterWorthProvider
    {
        private const int ChampValue = 20000;
        private const int UnrankedValue = 500;
        private const int TopRankedContenderValue = 16000;
        private const int ValueDropPerRanking = 1000;

        public static int GetWorth(Fighter fighter)
        {
            if (fighter.Ranking == null) return UnrankedValue;
            if (fighter.Ranking == 0) return ChampValue;
            return TopRankedContenderValue - (ValueDropPerRanking * fighter.Ranking.Value);
        }
    }
}
