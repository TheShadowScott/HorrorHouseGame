using System;
using static ConsoleManipulation;

namespace BattleMechanics
{
    class Battle
    {
        private readonly static Random randRef = new Random();
        private static bool testDodge(double dodgeRate)
        {
            return randRef.NextDouble() > dodgeRate;
        }
        private static int attack(int maxDamage, double critRate)
        {
            int critMult = 1;
            if (randRef.NextDouble() >= critRate)
            {
                critMult = 2;
                ConsoleWriteGreen("You crit!");
            }
            return randRef.Next(1, maxDamage + 1) * critMult;

        }
        public static int Fight(dynamic monster, PlayerObject.Player Player)
        {
            int rounds = 0;
            while (true) {
                if (!testDodge(Player.dodgeRate))
                { 
                    int damage = monster.hit() - Player.defence;
                    if (damage < 0) damage = 0;
                    Player.health -= damage;
                    if (Player.health <= 0)
                    {
                        return Player.health;
                    }
                    Console.WriteLine($"The {monster.name} did {damage} damage! You have {Player.health} HP left.");
                } 
                else
                {
                    ConsoleWriteGreen("You dodged!");
                }
                if (!testDodge(monster.dodgeRate))
                {
                    int playerHit = attack(Player.maxDamage, Player.critRate);
                    monster.health -= playerHit;
                    Console.WriteLine($"You hit the {monster.name} for {playerHit}.");
                    int roundCalc = randRef.Next(1 + rounds, 10);
                    if (roundCalc > Player.PlayerClass.randHitNum) Player.PlayerClass.SpecialHit(Player, monster);
                    if (monster.health <= 0) break;
                }
                else
                {
                    ConsoleWriteRed($"The {monster.name} dodged!");
                }
                rounds++;
                ConsoleWaitOut();
            }
            Linefeed();
            ConsoleWriteGreen($"You killed the {monster.name}");
            Linefeed();
            return Player.health;
        }
    }
}