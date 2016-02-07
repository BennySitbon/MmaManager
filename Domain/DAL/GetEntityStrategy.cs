using System;
using System.Linq;
using Domain.Models;

namespace Domain.DAL
{
    public class GetEntityStrategy
    {
        public static Func<int, T> GetStrategy<T>(IQueryable<T> allQuery ) where T:class
        {
            var type = typeof (T);
            if (type == typeof (Fighter))
            {
                var queryable = allQuery as IQueryable<Fighter>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Single(i => i.FighterId == id) as T;
            }
            if (type == typeof (Event))
            {
                var queryable = allQuery as IQueryable<Event>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Single(i => i.EventID == id) as T;
            }
            if (type == typeof (FightListing))
            {
                var queryable = allQuery as IQueryable<FightListing>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Single(i => i.FightListingID == id) as T;
            }
            if (type == typeof (Ownership))
            {
                var queryable = allQuery as IQueryable<Ownership>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Single(i => i.OwnershipID == id) as T;
            }
            if (type == typeof (Transaction))
            {
                var queryable = allQuery as IQueryable<Transaction>;
                if (queryable == null)
                {
                    throw new InvalidCastException();
                }
                return id => queryable.Single(i => i.TransactionID == id) as T;
            }
            return null;
        }

    }

}