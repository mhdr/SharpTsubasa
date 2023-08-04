using AdvancedSharpAdbClient;

namespace SharpTsubasa.Libs;

public class AdbEx
{
    public static Task RunAdb()
    {
        var config = Config.Load();
        if (!AdbServer.Instance.GetStatus().IsRunning)
        {
            AdbServer server = new AdbServer();
            StartServerResult result = server.StartServer(config.Adb, false);
            if (result != StartServerResult.Started)
            {
                Console.WriteLine("Can't start adb server");
            }
            else
            {
                Console.WriteLine("Adb server is running...");
            }
        }
        else
        {
            Console.WriteLine("Adb server is running...");
        }

        return Task.CompletedTask;
    }
}