using System.Drawing;
using System.Drawing.Imaging;
using AdvancedSharpAdbClient;

namespace SharpTsubasa.Libs;

public class AdbEx
{
    public AdbClient Client { get; set; }
    public DeviceData Device { get; set; }

    public Framebuffer? Framebuffer { get; set; }

    public AdbEx(AdbClient client, DeviceData device)
    {
        Client = client;
        Device = device;
    }

    public async Task<bool> IsAppRunning()
    {
        var result = await Client.IsAppRunningAsync(Device, "com.klab.captain283.global");
        return result;
    }

    public static Task RunAdb()
    {
        var config = Config.Load();
        if (!AdbServer.Instance.GetStatus().IsRunning)
        {
            AdbServer server = new AdbServer();
            StartServerResult result = server.StartServer(config.Adb, true);
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

    public async Task Screenshot()
    {
        if (Framebuffer == null)
        {
            Framebuffer = await Client.GetFrameBufferAsync(Device, CancellationToken.None);
        }
        else
        {
            await Framebuffer.RefreshAsync(false);
        }

        Framebuffer.ToImage().Save("screenshot.png", ImageFormat.Png);
    }

    public async Task<byte[]> Screenshot2()
    {
        if (Framebuffer == null)
        {
            Framebuffer = await Client.GetFrameBufferAsync(Device, CancellationToken.None);
        }
        else
        {
            await Framebuffer.RefreshAsync(false);
        }

        var stream = new MemoryStream();
        Framebuffer.ToImage().Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    public async Task Click(FindResult result, int wait = 500)
    {
        if (result.IsFound)
        {
            await Client.ClickAsync(Device, new Cords(result.ClickPoint.X, result.ClickPoint.Y));
            await Task.Delay(wait);
        }
    }

    public async Task Swipe(int x1 = 480, int y1 = 270, int x2 = 300, int y2 = 270, int speed = 100, int wait = 2000)
    {
        await Client.SwipeAsync(Device, x1, y1, x2, y2, speed); // Swipe from (480;270) to (400;270) on 100 ms
        await Task.Delay(wait);
    }

    public async Task Kill()
    {
        await Client.KillAdbAsync();
    }

    public async Task RunApp()
    {
        var packageName = "com.klab.captain283.global";
        await Client.StartAppAsync(Device, packageName);
    }
}