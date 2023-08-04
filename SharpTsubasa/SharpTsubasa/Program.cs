﻿// See https://aka.ms/new-console-template for more information

using AdvancedSharpAdbClient;
using SharpTsubasa.Libs;

var config = Config.Load();

var nox = new NoxEx();
bool shouldRun = true;

await AdbEx.RunAdb();

if (config.NoxAttach == "0")
{
    Console.WriteLine("running nox...");
    nox.RunNox();
    await Task.Delay(1000 * 60);
    Console.WriteLine("running Captain Tsubasa app...");
    nox.RunApp();
    await Task.Delay(1000 * 60);
}

var client = new AdbClient();
var devices = await client.GetDevicesAsync();
var device = devices.FirstOrDefault();


if (device == null)
{
    Console.WriteLine("no device found");
    return;
}

await client.ConnectAsync(device.Serial);

Console.WriteLine("start processing...");
Console.CancelKeyPress += (sender, eventArgs) =>
{
    if (config.NoxAttach == "0")
    {
        Console.WriteLine("killing nox...");
    }
    else
    {
        Console.WriteLine("Detaching nox...");
    }

    shouldRun = false;
};


var adbEx = new AdbEx(client, device);

while (shouldRun)
{
    FindResult result = null;

    // captain tsubasa dream team logo on welcome screen page
    await adbEx.Screenshot();
    result = CvEx.Find("0002");
    await adbEx.Click(result);

    // story mode on first page
    await adbEx.Screenshot();
    result = CvEx.Find("0001");
    await adbEx.Click(result);

    await Task.Delay(1);
}

if (config.NoxAttach == "0")
{
    nox.CloseNox();
    await Task.Delay(1000 * 10);
    nox.KillNox();
}

Console.WriteLine("exit bot.");