using AdvancedSharpAdbClient;

namespace SharpTsubasa.Libs;

public class Worker
{
    public static Task RunAdb(string adbPath)
    {
        if (!AdbServer.Instance.GetStatus().IsRunning)
        {
            AdbServer server = new AdbServer();
            StartServerResult result = server.StartServer(adbPath, false);
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