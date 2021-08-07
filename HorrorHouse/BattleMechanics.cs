using Monsters;
using System;
using static ConsoleManipulation;

namespace BattleMechanics
{
    class Battle
    {
        private readonly static Random randRef = new Random();
        private static bool TestDodge(double dodgeRate)
        {
            return randRef.NextDouble() > dodgeRate;
        }
        private static int Attack(int maxDamage, double critRate)
        {
            int critMult = 1;
            if (randRef.NextDouble() >= critRate)
            {
                critMult = 2;
                ConsoleWriteGreen("You crit!");
            }
            return randRef.Next(1, maxDamage + 1) * critMult;

        }
        public static int Fight(dynamic Monster, PlayerObject.Player Player)
        {
            int rounds = 0;
            while (true) {
                if (!TestDodge(Player.dodgeRate))
                {
                    int damage = Monster.Hit() - Player.defence;
                    if (damage < 0) damage = 0;
                    Player.health -= damage;
                    if (Player.health <= 0)
                    {
                        return Player.health;
                    }
                    Console.WriteLine($"The {Monster.name} did {damage} damage! You have {Player.health} HP left.");
                }
                else
                {
                    ConsoleWriteGreen("You dodged!");
                }
                if (!TestDodge(Monster.dodgeRate))
                {
                    int playerHit = Attack(Player.maxDamage, Player.critRate);
                    Monster.health -= playerHit;
                    Console.WriteLine($"You hit the {Monster.name} for {playerHit}.");
                    int roundCalc = randRef.Next(1 + rounds, 10);
                    if (roundCalc > Player.PlayerClass.randHitNum)
                    {
                        Player.PlayerClass.SpecialHit(Player, Monster);
                        rounds = 0;
                    }
                    if (Monster.health <= 0) break;
                }
                else
                {
                    ConsoleWriteRed($"The {Monster.name} dodged!");
                }
                rounds++;
                ConsoleWaitOut();
            }
            Linefeed();
            ConsoleWriteGreen($"You killed the {Monster.name}");
            Linefeed();
            return Player.health;
        }
    }
}