using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Waiting for better.game (Protoverse.exe)...");
        check:
        if (Process.GetProcessesByName("Protoverse").Length == 0)
        {
            Thread.Sleep(500);
            goto check;
        }
        else
        {
            Console.WriteLine("Protoverse Found!\nStarting Cheat...");
            Renderer renderer = new Renderer();
            renderer.Start();
        }
    }
}