using System;
using static Weapon;
using static ConsoleManipulation;
using static System.Convert;

public class PlayerObject
{
    private static readonly Random refRand = new Random();
    public class PlayerClass
    {
        public static dynamic SelectClass()
        {
            LineSeperator(); Linefeed();
            ConsoleWriteBlue("Select your Class:");
            ConsoleWriteGreen("1 - Wizard");
            Console.WriteLine("Casts spells that can deal heavy damage or heal you. Occurs randomly");
            Linefeed();
            ConsoleWriteGreen("2 - Warrior");
            Console.WriteLine("Can hit many, many times. Occurs randomly");
            Linefeed();
            ConsoleWriteGreen("3 - Rogue");
            Console.WriteLine("Can backstab your enemy, dealing massive damage.");
            LineSeperator(); Linefeed();
            int classSelect;
            try
            {
                classSelect = ToInt32(Console.ReadLine());
            }
            catch
            {
                ConsoleWriteRed("Invalid Response");
                return "";
            }
            return classSelect switch
            {
                1 => new Wizard(),
                2 => new Fighter(),
                3 => new Rogue(),
                _ => "",
            };
        }
        public class Wizard
        {
            public readonly int randHitNum = 6;
            private static void Heal(Player Player)
            {
                int heal = refRand.Next(5, 20);
                if (Player.health >= Player.maxHealth) return;
                else
                {
                    Player.health += heal;
                    if (Player.health > Player.maxHealth) Player.health = Player.maxHealth;
                    ConsoleWriteGreen($"You healed yourself for {heal} health.");
                }
            }
            private static void FireBall(dynamic MonsterObj)
            {
                int dmgDealt = refRand.Next(10, 31);
                MonsterObj.health -= dmgDealt;
                ConsoleWriteGreen($"You used fireball against the {MonsterObj.name} for {dmgDealt} damage.");
            }
            private static void HitDecider(Player Player, dynamic MonsterObj)
            {
                int hitNum = refRand.Next(1, 5);
                switch (hitNum)
                {
                    case 1:
                        Heal(Player);
                        break;
                    case 2:
                        FireBall(MonsterObj);
                        break;
                }
            }
            public void SpecialHit(Player Player, dynamic MonsterObj)
            {
                HitDecider(Player, MonsterObj);
            }

        }
        public class Fighter
        {
            public readonly int randHitNum = 3;
            public void SpecialHit(Player Player, dynamic MonsterObj)
            {
                int NumHits = 1;
                double HitRate = refRand.NextDouble();
                while (HitRate > 0.65)
                {
                    NumHits++;
                    HitRate = refRand.NextDouble();
                }
                int damageDealt = 0;
                for (int i = 0; i < NumHits; i++) damageDealt += refRand.Next(1, Player.maxDamage + 1);
                ConsoleWriteGreen($"You hit the {MonsterObj.name} {NumHits} extra times for {damageDealt} damage");
                MonsterObj.health -= damageDealt;
            }
        }
        public class Rogue
        {
            public readonly int randHitNum = 5;
            public void SpecialHit(Player Player, dynamic MonsterObj)
            {
                if (MonsterObj.health > 0)
                {
                    ConsoleWriteGreen($"BACKSTAB! You did {MonsterObj.health / 2} to the {MonsterObj.name}!");
                    MonsterObj.health /= 2;
                }
            }
        }
    }
    public class Player
    {
        public string name;
        public int maxDamage;
        public int maxHealth = 250;
        public double difficulty = 0;
        public int defence = 0;
        public int health = 250;
        public double exp = 0;
        public int level = 1;
        public double critRate;
        public double dodgeRate = 0.9;
        public dynamic PlayerClass;
        public Player(string name)
        {
            this.name = name;
            LineSeperator();
            dynamic playerWeapon = SelectWeapon();

            maxDamage = playerWeapon.maxDamage;
            critRate = playerWeapon.critRate;
            dodgeRate += playerWeapon.stealthDebuff;
            maxHealth += playerWeapon.healthBoost;
            health = maxHealth;

            do
            {
                PlayerClass = PlayerObject.PlayerClass.SelectClass();
            } while (PlayerClass is string);

        }
        public static int SelectDifficulty(Player Player)
        {
            ConsoleWriteRed(@"   ,    ,    /\   /\
  /( /\ )\  _\ \_/ /_
  |\_||_/| < \_   _/ >
  \______/  \|0   0|/
    _\/_   _(_  ^  _)_
   ( () ) /`\|V""""""V |/`\
     {}   \  \_____ /  /
     () /\   )= (   /\
     {}  /  \_ /\=/\_ /  \");
            Linefeed();
            Console.Write("Enter Difficulty. (0) = Easy, (1) = Normal, (2) = Hard, ");
            ConsoleWriteRed("(3) = HELLSCAPE");
            try
            {
                Player.difficulty = ToDouble(ToInt16(Console.ReadLine()));
            }
            catch
            {
                ConsoleWriteRed("That wasn't a valid option.");
                return 1;
            }
            if ( Player.difficulty < 0 || Player.difficulty > 3)
            {
                ConsoleWriteRed("That wasn't a valid option.");
                return 1;
            }
            Console.Clear();
            return 0;
        }

    }
}
