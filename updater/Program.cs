using System.Diagnostics;
using System.IO.Compression;

class Updater
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Update started incorrect.");
            return;
        }

        string zipPath = args[0];
        string mainAppName = args[1];
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

        try
        {
            Console.Write("Waiting for Main App to be closed.");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();

            while (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(mainAppName)).Length > 0)
            {
                Thread.Sleep(500);
            }

            Console.Write("Unzipping Update.");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();
            ZipFile.ExtractToDirectory(zipPath, appDirectory, true);

            Console.Write("Starting new Version.");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();
            Console.Clear();
            Process.Start(Path.Combine(appDirectory, mainAppName));

            File.Delete(zipPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update-ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }
}
