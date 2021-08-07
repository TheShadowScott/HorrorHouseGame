using System;
using System.Collections.Generic;
using System.Linq;

class ConsoleManipulation
{
    private static readonly IEnumerable<string> seperator = Enumerable.Repeat("=", 30);
    public static void Linefeed()
    {
        Console.WriteLine("");
    }
    public static void ResetConsole()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public static void ConsoleWriteGreen(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(toPrint);
        ResetConsole();
    }
    public static void ConsoleWriteInlineGreen(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(toPrint);
        ResetConsole();
    }
    public static void ConsoleWriteRed(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(toPrint);
        ResetConsole();
    }
    public static void ConsoleWriteInlineRed(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(toPrint);
        ResetConsole();
    }
    public static void ConsoleWritePurple(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(toPrint);
        ResetConsole();
    }
    public static void ConsoleWriteBlue(dynamic toPrint)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(toPrint);
        ResetConsole();
    }
    public static void LineSeperator()
    {
        foreach (String str in ConsoleManipulation.seperator) Console.Write(str);
        Linefeed();
    }
    public static void ConsoleWaitOut()
    {
        Console.WriteLine("Press Any Key To Continue...");
        Linefeed();
        Console.ReadKey();
    }
    public static class ConsoleSummons
    {
        public static void DemonSummon()
        {
            ConsoleManipulation.ConsoleWriteRed("\t\tA Demon has appeared");
            ConsoleManipulation.ConsoleWriteRed(@"                            ,-.                               
       ___,---.__          /'|`\          __,---,___          
    ,-'    \`    `-.____,-'  |  `-.____,-'    //    `-.       
  ,'        |           ~'\     /`~           |        `.      
 /      ___//              `. ,'          ,  , \___      \    
|    ,-'   `-.__   _         |        ,    __,-'   `-.    |    
|   /          /\_  `   .    |    ,      _/\          \   |   
\  |           \ \`-.___ \   |   / ___,-'/ /           |  /  
 \  \           | `._   `\\  |  //'   _,' |           /  /      
  `-.\         /'  _ `---'' , . ``---' _  `\         /,-'     
     ``       /     \    ,='/ \`=.    /     \       ''          
             |__   /|\_,--.,-.--,--._/|\   __|                  
             /  `./  \\`\ |  |  | /,//' \,'  \                  
            /   /     ||--+--|--+-/-|     \   \                 
           |   |     /'\_\_\ | /_/_/`\     |   |                
            \   \__, \_     `~'     _/ .__/   /            
             `-._,-'   `-._______,-'   `-._,-'");
        }
    }
}