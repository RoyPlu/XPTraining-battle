using System;
using Battle.Weapons;

namespace Battle
{
    public class Soldier
    {
        public Soldier(string name)
        {
            ValidateNameisNotBlank(name);
            Name = name;
            Weapon = new BareFist();
        }

        public Soldier(string name, IWeapon weapon)
        {
            ValidateNameisNotBlank(name);
            Name = name;
            Weapon = weapon;
        }

        private void ValidateNameisNotBlank(string name)
        {
            if (IsBlank(name))
            {
                throw new ArgumentException("name can not be blank");
            }
        }

        private bool IsBlank(string name) => string.IsNullOrEmpty(name?.Trim());
        
        public string Name { get; }
        public IWeapon Weapon { get; }

        public string Fight(Soldier defender)
        {
            if (Weapon.Damage == defender.Weapon.Damage)
            {
                return Name;
            }

            return Weapon.Damage > defender.Weapon.Damage ? Name : defender.Name;
        }
    }
}