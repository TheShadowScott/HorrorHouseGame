using System;
using static ConsoleManipulation;
using static System.Convert;

namespace Monsters
{
    class Monster
    {
        private static Random referenceRandom = new Random();
        public  static dynamic spawnMonster(PlayerObject.Player Player)
        {
            double chance = referenceRandom.NextDouble();
            chance += 3 * Player.difficulty / 100;
            chance += 0.05 * Player.level;
            if (chance > 1.95) return new DemonPrince();
            else if (chance > 1.7) return new DemonKnight();
            else if (chance > 1.45) return new Demonling();
            else if (chance > 1.1) return new BoneMage();
            else if (chance > 0.75) return new Warlock();
            else if (chance > 0.5) return new Ghoul();
            else if (chance > 0.3) return new Skeleton();
            else return new Zombie();
        }

        public class Zombie
        {
            public readonly double xp = 5;
            public int health = 10;
            public readonly string name = "Zombie";
            public readonly double dodgeRate = 0.875;
            public readonly int defense = 0;
            public void spawn()
            {
                ConsoleWriteBlue("A Zombie has spawned!");
                Linefeed();
                ConsoleWaitOut();
            }
            public int hit()
            {
                return referenceRandom.Next(1, 16);
            }
        }
        public class Skeleton
        {
            public readonly double xp = 10;
            public readonly string name = "Skeleton";
            public int health = 15;
            public readonly double dodgeRate = double.PositiveInfinity;
            public readonly double critRate = 0.825;
            public readonly int defense = 0;
            public void spawn()
            {
                ConsoleWriteBlue("A Skeleton Spawned");
                Linefeed();
                ConsoleWaitOut();
            }
            public int hit()
            {
                int multiplier = 1;
                bool crit = referenceRandom.NextDouble() > critRate;
                if (crit)
                {
                    multiplier = 2;
                    ConsoleWriteRed("The Skeleton crit!");
                }
                return multiplier * referenceRandom.Next(2, 11);
            }
        }
        public class Ghoul
        {
            public readonly string name = "Ghoul";
            public readonly double xp = 25;
            public int health = 30;
            public readonly double dodgeRate = 0.95;
            public readonly double critRate = 0.9275;
            public readonly int defense = 0;
            public void spawn()
            {
                ConsoleWriteBlue("A Ghoul has Spawned!");
                Linefeed();
                ConsoleWaitOut();
            }
            public int hit()
            {
                int multiplier = 1;
                bool crit = referenceRandom.NextDouble() > critRate;
                if (crit)
                {
                    multiplier = 2;
                    ConsoleWriteRed("The Ghoul crit!");
                }
                double damage = multiplier * referenceRandom.Next(1, 11);
                double heal = Math.Floor(damage / 3);
                if (heal > 0) ConsoleWritePurple($"The ghoul steals your life and heals for {heal} HP!");
                this.health += ToInt32(heal);
                return ToInt32(damage);
            }

        }
        public class Warlock
        {
            public readonly double xp = 50;
            public readonly string name = "Warlock";
            public double rounds = 0;
            public int health = 25;
            public readonly double dodgeRate = 0.75;
            public readonly double critRate = 0.95;
            public readonly int defense = 2;
            private static int Summon()
            {
                int summonedImps = 0;
                while (true)
                {
                    double chance = referenceRandom.NextDouble();
                    if (chance >= 0.4) summonedImps++;
                    else break;
                }
                return summonedImps;
            }
            public void spawn()
            {
                ConsoleWritePurple("A BOSS WARLOCK HAS SPAWNED");
                ConsoleWaitOut();
                Linefeed();
            }
            public int hit()
            {
                double helperImps = Summon();
                if (helperImps > 0) ConsoleWriteRed($"The warlock summoned {helperImps} Imp(s) to aid his attack!");
                int impDamage = 0;
                for (int i = 0; i < helperImps; i++) impDamage += referenceRandom.Next(1,6);
                double roundMult = 1.0 + (this.rounds / 10);
                int multiplier = 1;
                bool crit = referenceRandom.NextDouble() > critRate;
                if (crit)
                {
                    multiplier = 2;
                    ConsoleWriteRed("The Warlock crit!");
                }
                int warlockDamage = Convert.ToInt32(referenceRandom.Next(5, 26) * roundMult);
                this.rounds += 1.0;
                return (warlockDamage * multiplier) + impDamage;
            }
        }
        public class BoneMage
        {
            public readonly double xp = 75;
            public int health = 50;
            public readonly string name = "Bone Mage";
            public readonly double dodgeRate = 0.9275;
            public readonly double critRate = 0.8;
            public readonly int defense = 2;
            public void spawn()
            {
                ConsoleWritePurple("A BOSS BONE MAGE HAS APPEARED!");
                Linefeed();
            }
            public int hit()
            {
                int numHits = referenceRandom.Next(1, 6);
                int damage = 0;
                int critHits = 0;
                for (int i = 1; i <= numHits; i++)
                {
                    int multiplier = 1;
                    bool crit = referenceRandom.NextDouble() > critRate;
                    if (crit)
                    {
                        multiplier = 2;
                        critHits++;
                    }
                    damage += referenceRandom.Next(6, 21) *  multiplier;
                }
                ConsoleWriteRed($"The Bone Mage hit you {numHits} times!");
                if (critHits > 0) ConsoleWriteRed($"He crit {critHits} time(s)");
                return damage;
            }
        }
        public class Demonling
        {
            public readonly double xp = 90;
            public int health = 100;
            public readonly string name = "Demonling";
            public readonly double dodgeRate = 0.75;
            public readonly double critRate = 0.7;
            public readonly int defense = 3;
            public void spawn()
            {
                ConsoleSummons.DemonSummon();
                ConsoleWritePurple("DEMONLING");
                LineSeperator();
            }
            public int hit()
            {
                bool crit = referenceRandom.NextDouble() > critRate;
                int critMult = 1;
                if (crit)
                {
                    critMult = 2;
                    ConsoleWriteRed("The Demonling crit!");
                }
                double healthBoost = Math.Floor(ToDouble(this.health) / 4);
                int damage = ToInt32(healthBoost) + referenceRandom.Next(10, 46);
                return damage * critMult;
            }
        }
        public class DemonKnight
        {
            public readonly double xp = 110;
            public int health = 125;
            public readonly string name = "Demon Kinght";
            public readonly double dodgeRate = 0.5;
            public readonly double critRate = 0.6;
            public readonly int defense = 5;
            public void spawn()
            {
                ConsoleSummons.DemonSummon();
                ConsoleWritePurple($"{name}");
                LineSeperator();

            }
            public int hit()
            {
                bool crit = referenceRandom.NextDouble() > critRate;
                double critMult = 0.75;
                if (crit)
                {
                    critMult = 1.5;
                    ConsoleWriteRed("The Demon Knight crit!");
                }
                return ToInt32(Math.Floor(referenceRandom.Next(5, 40) * critMult));
            }
        }
        public class DemonPrince
        {
            public readonly string name = "Demon Prince";
            public readonly double xp = 125;
            public int health = 200;
            public readonly double dodgeRate = 0.5;
            public readonly double critRate = 0.5;
            public void spawn()
            {
                ConsoleSummons.DemonSummon();
                ConsoleWritePurple(name);
                LineSeperator();

            }
            public int hit()
            {
                int totalDamage = 0;
                for (int numSwipes = referenceRandom.Next(1, 4); numSwipes > 0; numSwipes--)
                {
                    int indivHit = referenceRandom.Next(1, 31);
                    bool crit = referenceRandom.NextDouble() > critRate;
                    if (crit) indivHit *= 2;
                    totalDamage += indivHit;

                }
                return totalDamage;
            }
        }
    }
}