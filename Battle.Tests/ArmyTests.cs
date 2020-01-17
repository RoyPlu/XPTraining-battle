using System.Linq;
using Battle.Weapons;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class ArmyTests
    {
        private Soldier _gimli = new Soldier("Gimli", new Axe());
        private Soldier _aragorn = new Soldier("Aragorn", new Sword());
        private Soldier _angmar = new Soldier("Angmar", new Axe());
        private Soldier _orc = new Soldier("Orc", new Spear());

        [Fact]
        public void CreateArmy_GivenAName_ArmyIsCreatedWithGivenName()
        {
            const string armyName = "FellowshipOfTheRing";

            var army = new Army(armyName);

            army.Name.Should().Be(armyName);
        }

        [Fact]
        public void CreateArmy_ArmyCreatedWithNoEnlistedSoldiers()
        {
            var army = new Army("FellowshipOfTheRing");

            army.EnlistedSoldiers.Should().BeEmpty();
        }

        [Fact]
        public void Enlist_GivenASoldier_ThanSoldierIsEnlisted()
        {
            var army = new Army("FellowshipOfTheRing");
            var soldierToEnlist = new Soldier("Gimli");

            army.Enlist(soldierToEnlist);

            army.EnlistedSoldiers.Should().HaveCount(1);
            army.EnlistedSoldiers.First().Should().Be(soldierToEnlist);
        }

        [Fact]
        public void GetFrontMan_Given2EnlistedSoldiers_ThanFirstEnlistedSoldierIsFrontMan()
        {
            var army = new Army("FellowshipOfTheRing");
            var firstEnlistedSoldier = new Soldier("Gimli");
            var secondEnlistedSoldier = new Soldier("Aragorn");

            army.Enlist(firstEnlistedSoldier);
            army.Enlist(secondEnlistedSoldier);

            army.EnlistedSoldiers.Should().HaveCount(2);
            army.FrontMan.Should().Be(firstEnlistedSoldier);
        }

        [Fact]
        public void Engage_GivenTwoArmies_TheyCanFightEachOther()
        {
            var fellowship = CreateFellowshipOfTheRing(_aragorn,_gimli);
            var armyOfMordor = CreateArmyOfMordor(_angmar,_orc);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().NotBeNull();
        }

        [Fact]
        public void Engage_GivenAttackingArmyHasTheStrongestWeapons_ArmiesOfEqualSize_ThenReturnsNameAttackingArmy()
        {
            var fellowship = CreateFellowshipOfTheRing(_aragorn,_gimli);
            var armyOfMordor = CreateArmyOfMordor(_angmar,_orc);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().Be(fellowship.Name);
        }

        [Fact]
        public void Engage_GivenDefendingArmyHasTheStrongestWeapons_ArmiesOfEqualSize_ThenReturnsNameDefendingArmy()
        {
            var fellowship = CreateFellowshipOfTheRing(_aragorn);
            var armyOfMordor = CreateArmyOfMordor(_angmar);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().Be(armyOfMordor.Name);
        }

        private Army CreateArmyOfMordor(params Soldier[] soldiers)
        {
            var army = new Army("ArmyOfMordor");

            foreach (var soldier in soldiers)
            {
                army.Enlist(soldier);
            }

            return army;
        }

        private  Army CreateFellowshipOfTheRing(params Soldier[] soldiers)
        {
            var army = new Army("FellowshipOfTheRing");

            foreach (var soldier in soldiers)
            {
                army.Enlist(soldier);
            }

            return army;
        }
    }
}
