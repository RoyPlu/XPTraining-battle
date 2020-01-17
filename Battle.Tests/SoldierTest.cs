using System;
using Battle.Weapons;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class SoldierTest
    {

        [Fact]
        public void Construction_ASoldierMustHaveAName()
        {
            var soldier = new Soldier("name");

            soldier.Name.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void Construction_ASoldierMustHaveAName_CannotBeBlank(string name)
        {
            Action act = () => new Soldier(name);
             
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construction_ASoldierHasBareFistAsDefaultWeapon()
        {
            var soldier = new Soldier("name");

            soldier.Weapon.Should().BeEquivalentTo(new BareFist());
        }

        [Fact]
        public void Construction_ASoldierCanHaveAnAxe()
        {
            var soldier = new Soldier("name", new Axe());

            soldier.Weapon.Should().BeEquivalentTo(new Axe());
        }

        [Fact]
        public void Construction_ASoldierCanHaveASpear()
        {
            var soldier = new Soldier("name", new Spear());

            soldier.Weapon.Should().BeEquivalentTo(new Spear());
        }

        [Fact]
        public void Construction_ASoldierCanHaveASword()
        {
            var soldier = new Soldier("name", new Sword());

            soldier.Weapon.Should().BeEquivalentTo(new Sword());
        }

        [Fact]
        public void Construction_ASoldierCanHaveBareFist()
        {
            var soldier = new Soldier("name", new BareFist());

            soldier.Weapon.Should().BeEquivalentTo(new BareFist());
        }

        [Fact]
        public void Fight_ASoldierCanFightAnotherSoldier_SoldierNameReturned()
        {
            var attacker = new Soldier("name");
            var defender = new Soldier("eman");

            var result = attacker.Fight(defender);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Fight_GivenAttackerHasMostDamagingWeapon_ThenReturnsAttackerName()
        {
            var attacker = new Soldier("name", new Axe());
            var defender = new Soldier("eman", new Spear());

            var result = attacker.Fight(defender);

            result.Should().Be(attacker.Name);
        }

        [Fact]
        public void Fight_GivenDefenderHasMostDamagingWeapon_ThenReturnsDefenderName()
        {
            var attacker = new Soldier("name", new BareFist());
            var defender = new Soldier("eman", new Spear());

            var result = attacker.Fight(defender);

            result.Should().Be(defender.Name);
        }

        [Fact]
        public void Fight_GivenAttackerAndDefenderHaveWeaponsOfEqualStrength_ThenReturnsAttackerName()
        {
            var attacker = new Soldier("name", new Spear());
            var defender = new Soldier("eman", new Spear());

            var result = attacker.Fight(defender);

            result.Should().Be(attacker.Name);
        }
    }
}