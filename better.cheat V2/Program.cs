using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

public class Program
{
    private readonly static string updateCheckUrl = "https://quickwhisper.fun/better.cheat/check.php";
    private readonly static string currentVersion = "2.1.0";
    private readonly static string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private readonly static string tempDownloadPath = Path.Combine(appDirectory, "update.zip");

    public static void Main()
    {
        RealMain().GetAwaiter().GetResult();
    }
    public static async Task RealMain()
    {
        Console.Write("Checking for Updates...");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine();

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string response = await client.GetStringAsync(updateCheckUrl);
                dynamic updateInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                string latestVersion = updateInfo.version;
                string updateUrl = updateInfo.update_url;

                if (latestVersion != currentVersion)
                {
                    await DownloadAndInstallUpdateAsync(updateUrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR while checking for Updates: {ex.Message}");
                Console.ReadKey();
            }
        }
        Console.Clear();
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
    private static async Task DownloadAndInstallUpdateAsync(string updateUrl)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] updateData = await client.GetByteArrayAsync(updateUrl);
                File.WriteAllBytes(tempDownloadPath, updateData);
                Console.WriteLine("Update Downloaded!");

                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://quickwhisper.fun/better.cheat/updater.exe", Path.Combine(appDirectory, "updater.exe"));
                string updaterPath = Path.Combine(appDirectory, "updater.exe");

                Process.Start(updaterPath, $"\"{tempDownloadPath}\" \"{AppDomain.CurrentDomain.FriendlyName}\"");
                Environment.Exit(0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR While downloading Update: {ex.Message}");
            Console.ReadKey();
        }
    }


    private static void ExtractUpdate()
    {
        try
        {
            ZipFile.ExtractToDirectory(tempDownloadPath, appDirectory, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR while extracting update: {ex.Message}");
            Console.ReadKey();
        }
        finally
        {
            if (File.Exists(tempDownloadPath))
            {
                File.Delete(tempDownloadPath);
            }
        }
    }
}