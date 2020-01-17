using System;
using System.Linq;
using Battle.Weapons;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Battle.Tests
{
    public class ArmyTests
    {
        private Soldier _gimli = new Soldier("Gimli", new Axe());
        private Soldier _aragorn = new Soldier("Aragorn", new Sword());
        private Soldier _angmar = new Soldier("Angmar", new Axe());
        private Soldier _orc = new Soldier("Orc", new BareFist());
        private Guid _lastCreatedGuid;

        [Fact]
        public void CreateArmy_GivenAName_ArmyIsCreatedWithGivenName()
        {
            const string armyName = "FellowshipOfTheRing";
            var headquarters = CreateStubbedHeadquarters();
            var army = new Army(armyName, headquarters);

            army.Name.Should().Be(armyName);
        }

        [Fact]
        public void CreateArmy_ArmyCreatedWithNoEnlistedSoldiers()
        {
            var army = new Army("FellowshipOfTheRing", CreateStubbedHeadquarters());

            army.EnlistedSoldiers.Should().BeEmpty();
        }

        [Fact]
        public void Enlist_GivenASoldier_ThanSoldierIsEnlisted()
        {
            var army = new Army("FellowshipOfTheRing", CreateStubbedHeadquarters());
            var soldierToEnlist = new Soldier("Gimli");

            army.Enlist(soldierToEnlist);

            army.EnlistedSoldiers.Should().HaveCount(1);
            army.EnlistedSoldiers.First().Should().Be(soldierToEnlist);
        }

        [Fact]
        public void GetFrontMan_Given2EnlistedSoldiers_ThanFirstEnlistedSoldierIsFrontMan()
        {
            var headquarters = Substitute.For<IHeadquarters>();
            headquarters.ReportEnlistment(Arg.Is(_gimli.Name)).Returns(Guid.NewGuid());
            headquarters.ReportEnlistment(Arg.Is(_aragorn.Name)).Returns(Guid.NewGuid());

            var army = new Army("FellowshipOfTheRing", headquarters);
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
            var fellowship = CreateFellowshipOfTheRing(_aragorn);
            var armyOfMordor = CreateArmyOfMordor(_orc);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().NotBeNull();
        }

        [Fact]
        public void Engage_GivenAttackingArmyHasTheStrongestWeapons_ThenReturnsNameAttackingArmy()
        {
            var fellowship = CreateFellowshipOfTheRing(_aragorn);
            var armyOfMordor = CreateArmyOfMordor(_orc);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().Be(fellowship.Name);
        }

        [Fact]
        public void Engage_GivenDefendingArmyHasTheStrongestWeapons_ThenReturnsNameDefendingArmy()
        {
            var fellowship = CreateFellowshipOfTheRing(_aragorn);
            var armyOfMordor = CreateArmyOfMordor(_angmar);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().Be(armyOfMordor.Name);
        }

        [Fact]
        public void Engage_GivenDefendingAndAttackingArmiesAreEqualInStrength_ThenReturnsNameAttackingArmy()
        {
            var fellowship = CreateFellowshipOfTheRing(_gimli);
            var armyOfMordor = CreateArmyOfMordor(_angmar);

            var outcome = fellowship.Engage(armyOfMordor);
            outcome.Should().Be(fellowship.Name);
        }

        [Fact]
        public void EnlistSoldier_GivenASoldierEnlists_ThenThisIsReportedToTheHQ()
        {
            var headquarters = CreateStubbedHeadquarters();
            var army = new Army("FellowshipOfTheRing", headquarters);
            var soldierToEnlist = new Soldier("Gimli");

            army.Enlist(soldierToEnlist);

            headquarters.Received(1).ReportEnlistment(soldierToEnlist.Name);
        }

        [Fact]
        public void EnlistSoldier_GivenTwoSoldierEnlists_AndTheyGetTheSameIdFromHQ_ThenAnExceptionIsThrown()
        {
            var sameId = Guid.NewGuid();
            var headquarters = Substitute.For<IHeadquarters>();
            headquarters.ReportEnlistment(Arg.Any<string>()).Returns(sameId);

            var army = new Army("FellowshipOfTheRing", headquarters);
            
            army.Enlist(_gimli);
            Action enlistSoldierSameId = () => army.Enlist(_aragorn);

            enlistSoldierSameId.Should().Throw<Exception>();
        }

        [Fact]
        public void EnlistSoldier_GivenASoldierEnlists_ThenSoldierGetsAssignedIdFromHQ()
        {
            var headquarters = CreateStubbedHeadquarters();
            var army = new Army("FellowshipOfTheRing", headquarters);
            var soldierToEnlist = new Soldier("Gimli");

            army.Enlist(soldierToEnlist);

            soldierToEnlist.Id.Should().Be(_lastCreatedGuid);
        }

        private IHeadquarters CreateStubbedHeadquarters()
        {
            _lastCreatedGuid = Guid.NewGuid();

            var headquarters = Substitute.For<IHeadquarters>();
            headquarters.ReportEnlistment(Arg.Any<string>()).Returns(_lastCreatedGuid);
            return headquarters;
        }

        private Army CreateArmyOfMordor(params Soldier[] soldiers)
        {
            var headquarters = CreateStubbedHeadquarters();
            var army = new Army("ArmyOfMordor", headquarters);

            foreach (var soldier in soldiers)
            {
                army.Enlist(soldier);
            }

            return army;
        }

        private  Army CreateFellowshipOfTheRing(params Soldier[] soldiers)
        {
            var headquarters = CreateStubbedHeadquarters();
            var army = new Army("FellowshipOfTheRing", headquarters);

            foreach (var soldier in soldiers)
            {
                army.Enlist(soldier);
            }

            return army;
        }
    }
}
