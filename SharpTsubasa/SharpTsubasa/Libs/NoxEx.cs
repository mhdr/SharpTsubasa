using System.Diagnostics;
namespace SharpTsubasa.Libs;

public class NoxEx
{
    public Process? Process = null;
    public void RunNox()
    {
        var config = Config.Load();
        var arguments = $"-clone:{config.NoxInstance} -resolution:960x540 -dpi:150";
        var processInfo = new ProcessStartInfo(config.Nox, arguments);
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        Process = Process.Start(processInfo);
    }

    public void RunApp()
    {
        var config = Config.Load();
        var packageName = "com.klab.captain283.global";
        var arguments = $"-clone:{config.NoxInstance} -package:{packageName}";
        var processInfo = new ProcessStartInfo(config.Nox, arguments);
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        Process.Start(processInfo);
    }

    public void CloseNox()
    {
        var config = Config.Load();
        var arguments = $"-clone:{config.NoxInstance} -quit";
        var processInfo = new ProcessStartInfo(config.Nox, arguments);
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        Process = Process.Start(processInfo);
    }
    
    public void KillNox()
    {
        if (Process != null)
        {
            Process.Kill(true);
        }
    }
}