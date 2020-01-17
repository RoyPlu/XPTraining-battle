using System.Linq;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class ArmyTests
    {
        [Fact]
        public void CreateArmy_ArmyCreatedWithNoEnlistedSoldiers()
        {
            var army = new Army();

            army.EnlistedSoldiers.Should().BeEmpty();
        }

        [Fact]
        public void Enlist_GivenASoldier_ThanSoldierIsEnlisted()
        {
            var army = new Army();
            var soldierToEnlist = new Soldier("Gimli");

            army.Enlist(soldierToEnlist);

            army.EnlistedSoldiers.Should().HaveCount(1);
            army.EnlistedSoldiers.First().Should().Be(soldierToEnlist);
        }

        [Fact]
        public void GetFrontMan_Given2EnlistedSoldiers_ThanFirstEnlistedSoldierIsFrontMan()
        {
            var army = new Army();
            var firstEnlistedSoldier = new Soldier("Gimli");
            var secondEnlistedSoldier = new Soldier("Aragorn");

            army.Enlist(firstEnlistedSoldier);
            army.Enlist(secondEnlistedSoldier);

            army.EnlistedSoldiers.Should().HaveCount(2);
            army.FrontMan.Should().Be(firstEnlistedSoldier);
        }
    }
}
