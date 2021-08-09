using System;
using static Monsters.Monster;
using static ConsoleManipulation;
using static BattleMechanics.Battle;
using static Levels.Leveling;
using System.Diagnostics;
using System.Threading;

namespace HorrorHouse
{
    class Program
    {
        private static PlayerObject.Player Player;
        public static readonly Random randomRef = new Random();
        private static readonly ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "C:/Users/smpen/source/repos/HorrorHouse/DiscordRP/bin/Debug/net5.0/DisStatUpdate.exe",
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true
        };
        private static void DiscordUpdater()
        {
            Process proc = Process.Start(psi);
            proc.WaitForExit();
        }
        private static void Welcome()
        {
            ResetConsole();
            LineSeperator();
            Linefeed();
            ConsoleWriteRed("Welcome to the hellhouse...\nWhere monsters run amock");
            Console.WriteLine("How long do you think you can survive?");
            LineSeperator();
            Linefeed();
            ConsoleWriteBlue("Enter your name ...");
            Program.Player = new PlayerObject.Player(Console.ReadLine());
            Console.Clear();
            ConsoleWriteRed($"Welcome to the hell house, {Program.Player.name}.");
            int verifyDifficulty;
            do
            {
                verifyDifficulty = PlayerObject.Player.SelectDifficulty(Program.Player);
            } while (verifyDifficulty != 0);

        }
        private static void PlayerLevelUpCheck(double expToAdd)
        {
            Player.exp += expToAdd * (1 + Player.difficulty/3);
            bool shouldLevel = LevelUpCheck(Player.exp, Player.level);
            if (shouldLevel)
            {
                LevelRandom(Player);
                Player.level++;
                Player.maxHealth += HealthInc(Player.level);
                Player.health = Player.maxHealth;
                ConsoleWriteGreen($"You are now level {Player.level}!");
                LineSeperator();
                Console.WriteLine("Stats:");
                ConsoleWritePurple($"Max HP: {Player.maxHealth}");
                ConsoleWritePurple($"Max Damage: {Player.maxDamage}\t\tDefence: {Player.defence}");
                ConsoleWritePurple($"Crit Rate: {Math.Floor((1.0 - Player.critRate) * 100.0)}%\t\tDodge Rate: {Math.Floor((1.0 - Player.dodgeRate) * 100.0)}%");
                LineSeperator();
            }
        }
        private static bool PlayGame()
        {
            Welcome();
            int difficultyBuffer = 75 * Convert.ToInt32(Player.difficulty);
            Player.maxHealth -= difficultyBuffer;
            Player.health = Player.maxHealth;
            int defeats = 0;
            while (true)
            {
                dynamic Monster = SpawnMonster(Player);
                Monster.Spawn();
                Player.health = Fight(Monster, Player);
                if (Player.health <= 0) break;
                defeats++;
                PlayerLevelUpCheck(Monster.xp);
            }
            ConsoleWriteRed("You died.");
            Console.WriteLine($"You killed {defeats} monsters...");
            Console.Write("Do you wish to play again (Y/N)?");
            string continueQuery = Console.ReadLine().ToLower();
            if (continueQuery == "y") return true;
            return false;
        }
        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(DiscordUpdater));
            t.Start();
            bool cont;
            do
            {
                cont = PlayGame();
            } while (cont);
            Linefeed();
            Console.WriteLine("Thanks for playing in the Horror House. See you soon ;)");
            ConsoleWaitOut();
        }
    }
}
