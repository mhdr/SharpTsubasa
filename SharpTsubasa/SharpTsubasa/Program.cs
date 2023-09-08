﻿// See https://aka.ms/new-console-template for more information

using AdvancedSharpAdbClient;
using SharpTsubasa.Libs;

var config = Config.Load();

bool shouldRun = true;

await AdbEx.RunAdb();

var client = new AdbClient();
// var d = $"localhost:{config.Port}";
// // await client.ConnectAsync(device.Serial);
// await client.ConnectAsync(d);
var devices = await client.GetDevicesAsync();
var device = devices.FirstOrDefault();

if (device == null)
{
    Console.WriteLine("no device found");
    return;
}

// var d = $"localhost:{config.Port}";
await client.ConnectAsync(device.Serial);
// await client.ConnectAsync(d);

Console.WriteLine("start processing...");
Console.CancelKeyPress += (sender, eventArgs) => { shouldRun = false; };


var adbEx = new AdbEx(client, device);

while (shouldRun)
{
    FindResult result = null;
    byte[] screenshot = null;

    screenshot = await adbEx.Screenshot2();

    // captain tsubasa dream team logo on welcome screen page

    result = CvEx.Find2(screenshot, "0002");
    await adbEx.Click(result, 4000);
    if (result.IsFound) continue;

    // app home page
    result = CvEx.Find2(screenshot, "0028");
    if (result.IsFound)
    {
        // story mode on first page
        result = CvEx.Find2(screenshot, "0001");
        await adbEx.Click(result);
        if (result.IsFound)
        {
            continue;
        }
        else
        {
            await adbEx.Swipe(700, 250, 700, 150);
            await Task.Delay(1000);
            continue;
        }
    }


    // story mode on first page
    result = CvEx.Find2(screenshot, "0003");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // search for completion mode normal
    result = CvEx.Find2(screenshot, "0005");
    if (result.IsFound)
    {
        // find story which is ready to be played in normal mode 
        result = CvEx.Find2(screenshot, "0004");
        await adbEx.Click(result);
        if (result.IsFound) continue;

        // if we can't find any story swpie to find another one
        await adbEx.Swipe(480,270,300,270);
        if (result.IsFound) continue;
    }

    // normal button
    result = CvEx.Find2(screenshot, "0006");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // solo play
    result = CvEx.Find2(screenshot, "0008");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // play match button
    result = CvEx.Find2(screenshot, "0007");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // FP on select friend page
    result = CvEx.Find2(screenshot, "0009");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // kick off button
    result = CvEx.Find2(screenshot, "0010");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // you win after match
    result = CvEx.Find2(screenshot, "0011");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // dreamball - after match - first clear reward - after demo
    result = CvEx.Find2(screenshot, "0012");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // breakdown after match
    result = CvEx.Find2(screenshot, "0013");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // go to scenario list - after match
    result = CvEx.Find2(screenshot, "0014");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // retry button
    result = CvEx.Find2(screenshot, "0015");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // story demo
    result = CvEx.Find2(screenshot, "0016");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // menu button on story demo
    result = CvEx.Find2(screenshot, "0017");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // skip button on story demo
    result = CvEx.Find2(screenshot, "0018");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // ok button
    result = CvEx.Find2(screenshot, "0019");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // close button
    result = CvEx.Find2(screenshot, "0020");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // resume match button while playing
    result = CvEx.Find2(screenshot, "0021");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // restore button - recovery energy dialog
    result = CvEx.Find2(screenshot, "0022");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // rank up
    result = CvEx.Find2(screenshot, "0023");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // cancel button - add friend
    result = CvEx.Find2(screenshot, "0024");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // resume match button
    result = CvEx.Find2(screenshot, "0025");
    await adbEx.Click(result);
    if (result.IsFound) continue;
    
    // new account - guide
    result = CvEx.Find2(screenshot, "0026");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // got it button
    result = CvEx.Find2(screenshot, "0027");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    if (!await adbEx.IsAppRunning())
    {
        await adbEx.RunApp();
        await Task.Delay(5000);
    }

    await Task.Delay(1);
}

await adbEx.Kill();
Console.WriteLine("exit bot.");