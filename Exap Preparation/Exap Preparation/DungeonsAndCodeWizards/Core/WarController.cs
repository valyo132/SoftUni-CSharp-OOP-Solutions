namespace WarCroft.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WarCroft.Constants;
    using WarCroft.Entities.Characters;
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Items;

    public class WarController
    {
        private List<Character> party;
        private List<Item> pool;

        public WarController()
        {
            party = new List<Character>();
            pool = new List<Item>();
        }

        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];

            Character hero = characterType switch
            {
                nameof(Warrior) => new Warrior(name),
                nameof(Priest) => new Priest(name),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType))
            };

            party.Add(hero);

            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];

            Item item = itemName switch
            {
                nameof(FirePotion) => new FirePotion(),
                nameof(HealthPotion) => new HealthPotion(),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName))
            };

            pool.Add(item);

            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];

            if (!party.Any(x => x.Name == characterName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, characterName);
            }

            if (!pool.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            var hero = party.Find(x => x.Name == characterName);
            var item = pool.Last();

            hero.Bag.AddItem(item);

            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            if (!party.Any(x => x.Name == characterName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, characterName);
            }

            var hero = party.Find(x => x.Name == characterName);
            var item = hero.Bag.GetItem(itemName);

            hero.UseItem(item);

            return string.Format(SuccessMessages.UsedItem, characterName, itemName);
        }

        public string GetStats()
        {
            StringBuilder result = new StringBuilder();

            foreach (var hero in party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
                string isAlive = hero.IsAlive ? "Alive" : "Dead";

                result.AppendLine($"{hero.Name} - HP: {hero.Health}/{hero.BaseHealth}, AP: {hero.Armor}/{hero.BaseArmor}, Status: {isAlive}");
            }

            return result.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            StringBuilder result = new StringBuilder();

            string attackerName = args[0];
            string receiverName = args[1];

            if (!party.Any(x => x.Name == attackerName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, attackerName);
            }

            if (!party.Any(x => x.Name == receiverName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, receiverName);
            }

            var attacker = party.Find(x => x.Name == attackerName);
            var receiver = party.Find(x => x.Name == receiverName);

            if (attacker.GetType() != typeof(Warrior))
            {
                throw new ArgumentException(ExceptionMessages.AttackFail, attackerName);
            }

             (attacker as Warrior).Attack(receiver);

            result.AppendLine($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! {receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");

            if (receiver.Health <= 0)
            {
                result.AppendLine($"{receiver.Name} is dead!");
                receiver.IsAlive = false;
            }

            return result.ToString().TrimEnd();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            if (!party.Any(x => x.Name == healerName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, healerName);
            }

            if (!party.Any(x => x.Name == healingReceiverName))
            {
                throw new ArgumentException(ExceptionMessages.CharacterNotInParty, healingReceiverName);
            }

            var healer = party.Find(x => x.Name == healerName);
            var receiver = party.Find(x => x.Name == healingReceiverName);

            if (healer.GetType() != typeof(Priest))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }

            (healer as Priest).Heal(receiver);

            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
        }
    }
}
