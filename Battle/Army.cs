using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public class Army
    {
        private readonly IHeadquarters _headquarters;

        public Army(string name, IHeadquarters headquarters)
        {
            _headquarters = headquarters;
            Name = name;
        }

        public List<Soldier> EnlistedSoldiers { get; } = new List<Soldier>();
        public Soldier FrontMan { get; private set; }
        public string Name { get; }

        public void Enlist(Soldier soldierToEnlist)
        {
            var newSoldierId = _headquarters.ReportEnlistment(soldierToEnlist.Name);
            AssignId(soldierToEnlist, newSoldierId);

            if (IsFirstEnlistedSoldier())
            {
                FrontMan = soldierToEnlist;
            }
            
            EnlistedSoldiers.Add(soldierToEnlist);
        }

        private void AssignId(Soldier soldierToEnlist, Guid soldierId)
        {
            var soldierWithSameIdEnlisted = EnlistedSoldiers.Any(s => s.Id.Equals(soldierId));
            
            if (soldierWithSameIdEnlisted)
            {
                throw new Exception();
            }

            soldierToEnlist.AssignId(soldierId);
        }

        private bool IsFirstEnlistedSoldier()
        {
            return !EnlistedSoldiers.Any();
        }

        public string Engage(Army defendingArmy)
        {
            var victorer = string.Empty;
            var warIsOver = false;

            while (!warIsOver)
            {
                ResolveFight(defendingArmy);

                if (AttackingArmyHasWon(defendingArmy))
                {
                    warIsOver = true;
                    victorer = Name;
                }

                if (DefendingArmyHasWon(defendingArmy))
                {
                    warIsOver = true;
                    victorer = defendingArmy.Name;
                }
            }

            return victorer;
        }

        private bool AttackingArmyHasWon(Army defendingArmy)
        {
            return FrontMan != null && defendingArmy.FrontMan == null;
        }

        private bool DefendingArmyHasWon(Army defendingArmy)
        {
            return FrontMan == null && defendingArmy.FrontMan != null;
        }

        private void ResolveFight(Army defendingArmy)
        {
            var outcome = FrontMan.Fight(defendingArmy.FrontMan);

            if (outcome == FrontMan.Name)
            {
                defendingArmy.KillFrontMan();
            }
            else
            {
                KillFrontMan();
            }
        }

        private void KillFrontMan()
        {
            EnlistedSoldiers.Remove(FrontMan);
            FrontMan = EnlistedSoldiers.FirstOrDefault();
        }
    }
}