﻿using System.Drawing;
using System.Drawing.Imaging;
using AdvancedSharpAdbClient;

namespace SharpTsubasa.Libs;

public class AdbEx
{
    public AdbClient Client { get; set; }
    public DeviceData Device { get; set; }

    public AdbEx(AdbClient client, DeviceData device)
    {
        Client = client;
        Device = device;
    }

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

    public async Task Screenshot()
    {
        Image screenshot = (Image)await Client.GetFrameBufferAsync(Device, CancellationToken.None);
        screenshot.Save("screenshot.png", ImageFormat.Png);
    }

    public async Task Click(FindResult result, int wait = 2000)
    {
        if (result.IsFound)
        {
            await Client.ClickAsync(Device, new Cords(result.ClickPoint.X, result.ClickPoint.Y));
            await Task.Delay(wait);
        }
    }
}