using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;
using System.Runtime.InteropServices;
using better.cheat_V2;
using System.Diagnostics;
using System.Net;
using System.Linq.Expressions;

public class Renderer : Overlay
{
    protected override void Render()
    {
        ImGuiStylePtr style = ImGui.GetStyle();
        style.WindowBorderSize = 2.0f;
        style.WindowRounding = 10;
        style.ChildRounding = 5;
        style.GrabRounding = 5;
        style.PopupRounding = 5;
        style.FrameRounding = 5;
        style.Colors[(int)ImGuiCol.Border] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f); // Mittelgrau
        style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.8f, 0.8f, 0.8f, 1.0f); // Helles Grau
        style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.3f, 0.3f, 0.3f, 1.0f); // Dunkleres Grau
        style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.4f, 0.4f, 0.4f, 1.0f); // Etwas helleres Grau
        style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.6f, 0.6f, 0.6f, 1.0f); // Mittelhelles Grau
        style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.7f, 0.7f, 0.7f, 1.0f); // Helles Grau
        style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.9f, 0.9f, 0.9f, 1.0f); // Sehr helles Grau
        style.Colors[(int)ImGuiCol.Button] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f); // Mittelgrau (wie Border)
        style.Colors[(int)ImGuiCol.Header] = new Vector4(0.4f, 0.4f, 0.4f, 1.0f); // Etwas dunkleres Grau
        style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.6f, 0.6f, 0.6f, 1.0f); // Mittelhelles Grau
        MainMenu();

        const int menukey = 0x2D;
        if ((GetAsyncKeyState(menukey) & 0x8000) != 0)
        {
            menuvisible = !menuvisible;
            Thread.Sleep(100);
        }
    }
    public static string processName = "Protoverse";
    public static Globals globals = new Globals();
    public static bool menuvisible = true;
    public static int customprojectilespeed = 0;
    public static int custommaxspeed = 0;
    public static void MainMenu()
    {
        if (menuvisible)
        {
            ImGui.SetNextWindowSize(new Vector2(350, 450));
            ImGui.Begin("cheat made by dewzacsharp for better.game",
                ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoDocking
                | ImGuiWindowFlags.NoScrollbar);
            ImGui.Text("Hide/Show Menu = Insert");
            ImGui.SetCursorPos(new Vector2(320, 20));
            if (ImGui.Button("-", new Vector2(20, 20)))
                menuvisible = !menuvisible;

            ImGui.Separator();
            ImGui.Checkbox("Infinite Ammo", ref globals.InfAmmo);
            ImGui.Checkbox("Show Console", ref globals.ConsoleVisible);
            ImGui.Text("\nIf you got any ideas for other cheat features, pls dm me on Discord: dewzacsharp\n");
            ImGui.SeparatorText("v removed due to being useless v");
            ImGui.Checkbox("Vertical Camera", ref globals.VerticalCamera);
            ImGui.Checkbox("Horizontal Camera", ref globals.HorizontalCamera);
            ImGui.Checkbox("Fun Camera", ref globals.FunCamera);
            ImGui.Checkbox("Sideways Camera", ref globals.SideWaysCamera);
            ImGui.Separator();
            ImGui.Checkbox("Custom Projectile Speed", ref globals.CustomProjectileSpeed);
            ImGui.Checkbox("Custom Max Speed", ref globals.CustomMaxSpeed);
            ImGui.SeparatorText("^ removed due to being useless ^");

            // Putting the memory writing also in here to achieve max FPS when the menu is closed
            // because if people got an ass PC they can just close menu and get more FPS then if menu is open yk
            // type shi

            if (globals.ConsoleVisible)
                AllocConsole();
            else if (!globals.ConsoleVisible)
                FreeConsole();

            if (globals.InfAmmo)
            {
                Patch(hProcess, arAddress, Offsets.arAmmo);
                Patch(hProcess, PumpAddress, Offsets.PumpAmmo);
            }
            else if (!globals.InfAmmo)
            {
                Patch(hProcess, arAddress, Offsets.arAmmoOG);
                Patch(hProcess, PumpAddress, Offsets.PumpAmmoOG);
            }

            //if (globals.VerticalCamera)
            //    Patch(hProcess, cameraUpDownAddress, Offsets.CameraUpDownPatch);
            //else if (!globals.VerticalCamera)
            //    Patch(hProcess, cameraUpDownAddress, Offsets.CameraUpDownOG);
            //
            //if (globals.HorizontalCamera)
            //    Patch(hProcess, cameraLeftRightAddress, Offsets.CameraLeftRightPatch);
            //else if (!globals.HorizontalCamera)
            //    Patch(hProcess, cameraLeftRightAddress, Offsets.CameraLeftRightOG);
            //
            //if (globals.FunCamera)
            //    Patch(hProcess, cameraFunAddress, Offsets.CameraFunPatch);
            //else if (!globals.FunCamera)
            //    Patch(hProcess, cameraFunAddress, Offsets.CameraFunOG);
            //
            //if (globals.SideWaysCamera)
            //    Patch(hProcess, sidewaysCameraAddress, Offsets.SidewaysCameraPatch);
            //else if (!globals.SideWaysCamera)
            //    Patch(hProcess, sidewaysCameraAddress, Offsets.SidewaysCameraOG);
            //
            //if (globals.CustomProjectileSpeed)
            //{   
            //    ImGui.InputInt("", ref customprojectilespeed);
            //    if (ImGui.Button("Apply"))
            //    {
            //        IntPtr modulebase = client.GetModuleBase("Protoverse.exe");
            //        client.WriteFloat(modulebase + 0x19B9990, customprojectilespeed);
            //    }
            //}
            //
            //if (globals.CustomMaxSpeed)
            //{
            //    ImGui.InputInt("", ref custommaxspeed);
            //    
            //    if (ImGui.Button("Apply"))
            //    {
            //        IntPtr modulebase = client.GetModuleBase("Protoverse.exe");
            //        client.WriteFloat(modulebase + 0xDF1BF8, custommaxspeed);
            //    }
            //}
        }
    }
    public static void RunCommand(string fileName, string arguments)
    {
        ProcessStartInfo psi = new ProcessStartInfo(fileName, arguments)
        {
            CreateNoWindow = true,
            UseShellExecute = false
        };

        Process process = Process.Start(psi);
        process.WaitForExit();
    }
    public static Process process = Process.GetProcessesByName(processName)[0];
    public static IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
    public static IntPtr arAddress = process.MainModule.BaseAddress + 0x186E9A;
    public static IntPtr PumpAddress = process.MainModule.BaseAddress + 0x1867B1;
    public static IntPtr cameraUpDownAddress = process.MainModule.BaseAddress + 0x282469;
    public static IntPtr cameraLeftRightAddress = process.MainModule.BaseAddress + 0x282474;
    public static IntPtr cameraFunAddress = process.MainModule.BaseAddress + 0x282495;
    public static IntPtr sidewaysCameraAddress = process.MainModule.BaseAddress + 0x281FDA;

    [DllImport("kernel32.dll")]
    static extern bool FreeConsole();
    [DllImport("kernel32.dll")]
    static extern bool AllocConsole();


    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);
    [DllImport("kernel32.dll")]
    static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

    [DllImport("kernel32.dll")]
    static extern void CloseHandle(IntPtr hObject);

    const int PROCESS_ALL_ACCESS = 0x1F0FFF;
    const uint PAGE_EXECUTE_READWRITE = 0x40;

    public static Dewz client = new Dewz("Protoverse");

    static void Patch(IntPtr hProcess, IntPtr address, byte[] bytes)
    {
        uint oldProtect;
        VirtualProtectEx(hProcess, address, bytes.Length, PAGE_EXECUTE_READWRITE, out oldProtect);
        WriteProcessMemory(hProcess, address, bytes, bytes.Length, out _);
        VirtualProtectEx(hProcess, address, bytes.Length, oldProtect, out _);
    }
}
