namespace WarCroft.Entities.Characters
{
    using System;

    using WarCroft.Constants;
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;

    public class Warrior : Character, IAttacker
    {
        public Warrior(string name)
            : base(name, 100, 50, 40, new Satchel())
        { }

        public void Attack(Character character)
        {
            this.EnsureAlive();
            character.EnsureAlive();

            if (character.Equals(this))
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }

            character.TakeDamage(this.AbilityPoints);
        }
    }
}
