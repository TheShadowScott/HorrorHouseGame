using System;
using static System.Convert;
using static ConsoleManipulation;

class Weapon
{
    private static int WeaponInput()
    {
        int weaponSelection;
        try
        {
            weaponSelection = ToInt32(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid Selection");
            return -1;
        }
        if (weaponSelection > 4 || weaponSelection < 1)
        {
            Console.WriteLine("Invalid Selection");
            return -1;
        }
        return weaponSelection;
    }
    public static dynamic SelectWeapon()
    {
        LineSeperator(); Linefeed();
        ConsoleWriteBlue("Select Your Weapon:");
        Linefeed(); LineSeperator(); Linefeed();
        ConsoleWriteBlue("1: Claymore - Heavy hitter, but strongly debuffs your swiftness");
        ConsoleWriteInlineGreen("MAX DMG: +5\t"); Console.Write("CRIT: +0%\t"); ConsoleWriteInlineRed("STEALTH: -10%\t"); Console.Write("MAX HEALTH: +0");
        Linefeed(); Linefeed();
        ConsoleWriteBlue("2: Dagger - Small, fast, and lethal, but doesn't hurt that much");
        ConsoleWriteInlineRed("MAX DMG: -2\t"); ConsoleWriteInlineGreen("CRIT: +15%\t"); ConsoleWriteInlineGreen("STEALTH: +10%\t"); Console.WriteLine("MAX HEALTH: 0");
        Linefeed(); Linefeed();
        ConsoleWriteBlue("3: Rapier - Well rounded, but not extravagant weapon");
        ConsoleWriteInlineGreen("MAX DMG: +2\t"); ConsoleWriteInlineGreen("CRIT: +5%\t"); ConsoleWriteInlineGreen("STEALTH: +5%\t"); Console.Write("MAX HEALTH: +0");
        Linefeed(); Linefeed();
        ConsoleWriteBlue("4: Spelltome - A magic tome that moderately increases your life force");
        Console.Write("MAX DMG: +0\t"); ConsoleWriteInlineGreen("CRIT: +5%\t"); ConsoleWriteInlineGreen("STEALTH: +5%\t"); ConsoleWriteInlineGreen("MAX HEALTH: +20");
        Linefeed(); Linefeed();
        int returnFeed;
        do
        {
            returnFeed = WeaponInput();
        } while (returnFeed == -1);
        return returnFeed switch {
            1 => new Claymore(),
            2 => new Dagger(),
            3 => new Rapier(),
            4 => new SpellTome(),
            _ => null,
        };
    }
    public class Claymore
    {
        public readonly int maxDamage = 15;
        public readonly double critRate = 0.85;
        public readonly double stealthDebuff = 0.1;
        public readonly int healthBoost = 0;
    }
    public class Dagger
    {
        public readonly int maxDamage = 8;
        public readonly double critRate = 0.7;
        public readonly double stealthDebuff = -0.10;
        public readonly int healthBoost = 0;
    }
    public class Rapier
    {
        public readonly int maxDamage = 12;
        public readonly double critRate = 0.8;
        public readonly double stealthDebuff = -0.05;
        public readonly int healthBoost = 0;
    }
    public class SpellTome
    {
        public readonly int maxDamage = 10;
        public readonly double critRate = 0.8;
        public readonly double stealthDebuff = -0.05;
        public readonly int healthBoost = 20;
    }
}