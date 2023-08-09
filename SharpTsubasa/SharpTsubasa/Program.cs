// See https://aka.ms/new-console-template for more information

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
    if (!await adbEx.IsAppRunning())
    {
        nox.RunApp();
        await Task.Delay(5000);
    }

    FindResult result = null;
    byte[] screenshot = null;

    screenshot = await adbEx.Screenshot2();
    
    // captain tsubasa dream team logo on welcome screen page
    
    result = CvEx.Find2(screenshot, "0002");
    await adbEx.Click(result, 4000);
    if (result.IsFound) continue;

    // story mode on first page
    result = CvEx.Find2(screenshot, "0001");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // story mode on first page
    result = CvEx.Find2(screenshot, "0003");
    await adbEx.Click(result);
    if (result.IsFound) continue;

    // search for completion mode normal
    result = CvEx.Find2(screenshot, "0005");
    if (result.IsFound)
    {
        result = CvEx.Find2(screenshot, "0004", 0.8);
        await adbEx.Click(result);
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

    await Task.Delay(1);
}

if (config.NoxAttach == "0")
{
    nox.CloseNox();
    await Task.Delay(1000 * 10);
    nox.KillNox();
}

Console.WriteLine("exit bot.");