using Battle.Weapons;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class WeaponTests
    {
        [Fact]
        public void Weapon_AxeHasDamage3()
        {
            var weapon = new Axe();

            weapon.Damage.Should().Be(3);
        }

        [Fact]
        public void Weapon_SpearHasDamage2()
        {
            var weapon = new Spear();

            weapon.Damage.Should().Be(2);
        }

        [Fact]
        public void Weapon_SwordHasDamage2()
        {
            var weapon = new Sword();

            weapon.Damage.Should().Be(2);
        }

        [Fact]
        public void Weapon_BareFistHasDamage1()
        {
            var weapon = new BareFist();

            weapon.Damage.Should().Be(1);
        }
    }
}
