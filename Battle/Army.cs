using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public class Army
    {
        public List<Soldier> EnlistedSoldiers { get; } = new List<Soldier>();
        public Soldier FrontMan { get; private set; }

        public void Enlist(Soldier soldierToEnlist)
        {
            if (IsFirstEnlistedSoldier())
            {
                FrontMan = soldierToEnlist;
            }

            EnlistedSoldiers.Add(soldierToEnlist);
        }

        private bool IsFirstEnlistedSoldier()
        {
            return !EnlistedSoldiers.Any();
        }
    }
}
