using System;
using System.Data.Entity;
using System.Linq;
using MmaManager.Models;

namespace MmaManager.DAL
{
    public class GetLoadedStrategy
    {
        public static Func<int, T> GetStrategy<T>(IQueryable<T> allQuery,UfcContext context) where T : class
        {
            var type = typeof(T);
            if (type == typeof (Fighter))
            {
                var queryable = allQuery as IQueryable<Fighter>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }

                return id =>
                {
                    //Need to change this someway to not use the context
                   // using (var context = new UfcContext())
                   // {
                        var fightListings =
                            context.FightListings.Where(j => j.BlueFighterFighterID == id || j.RedFighterFighterID == id)
                                .ToList();
                        var fighter = queryable.Single(i => i.FighterId == id);
                        fighter.FightListings = fightListings;
                        return fighter as T;
                   // }
                };
            }
            if (type == typeof(Event))
            {
                var queryable = allQuery as IQueryable<Event>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Include("FightCard").Single(i => i.EventID == id) as T;
            }
            if (type == typeof(FightListing))
            {
                var queryable = allQuery as IQueryable<FightListing>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable
                    .Include("RedFighter")
                    .Include("BlueFighter")
                    .Include("Event").Single(i => i.FightListingID == id) as T;
            }
            if (type == typeof(Ownership))
            {
                var queryable = allQuery as IQueryable<Ownership>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable
                    .Include("Fighter")
                    .Include("Transaction")
                    .Single(i => i.OwnershipID == id) as T;
            }
            if (type == typeof(Transaction))
            {
                var queryable = allQuery as IQueryable<Transaction>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable
                    .Include("Fighter")
                    .Include("FightListing")
                    .Single(i => i.TransactionID == id) as T;
            }
            return null;
        }
    }
}