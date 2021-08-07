using System;
using static System.Convert;

namespace Levels
{
    class Leveling
        {
        private static readonly Random randRef = new Random();
        public static void LevelRandom(PlayerObject.Player Player)
        {
            for (int skillUp = 4; skillUp > 0; skillUp--)
            {
                int randUp = randRef.Next(0, 5);
                switch (randUp)
                {
                    case 1:
                        Player.maxDamage++;
                        break;
                    case 2:
                        if (Player.critRate > 0) Player.critRate -= 0.05;
                        break;
                    case 3:
                        Player.defence++;
                        break;
                    case 4:
                        if (Player.dodgeRate > 0.25) Player.dodgeRate -= 0.05;
                        break;
                    default:
                        Player.maxDamage++;
                        break;
                }
            }
        }
        private static double getLevelBracket(double exp)
        {
            double exponent = Math.Pow(1.000975, -exp);
            double level = (-51.0 * exponent) + 51;
            return Math.Floor(level);
        }
        public static bool levelUpCheck(double exp, int level)
        {
            int reqLevel = ToInt32(getLevelBracket(exp));
            if (reqLevel > level) return true;
            return false;
        }
        public static int healthInc(int level)
        {
            double inc = Math.Floor(Math.Log(level));
            return ToInt32(inc) + 1;

        }
    }
}