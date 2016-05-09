using Domain.Models;

namespace Domain.Providers
{
    public static class FighterWorthProvider
    {
        private const decimal ChampValue = 20000;
        private const decimal UnrankedValue = 500;
        private const decimal TopRankedContenderValue = 16000;
        private const decimal ValueDropPerRanking = 1000;

        public static decimal GetWorth(Fighter fighter)
        {
            if (fighter.Ranking == null) return UnrankedValue;
            if (fighter.Ranking == 0) return ChampValue;
            return TopRankedContenderValue - (ValueDropPerRanking * fighter.Ranking.Value);
        }
    }
}
