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
    byte[] screenshot = null;

    // captain tsubasa dream team logo on welcome screen page
    //await adbEx.Screenshot();
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0002");
    await adbEx.Click(result, 4000);

    // story mode on first page
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0001");
    await adbEx.Click(result);
    
    // story mode on first page
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0003");
    await adbEx.Click(result);
    
    // search for completion mode normal
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0005");
    if (result.IsFound)
    {
        result = CvEx.Find2(screenshot, "0004",0.8);
        await adbEx.Click(result);
    }

    // normal button
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0006");
    await adbEx.Click(result);
    
    // solo play
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0008");
    await adbEx.Click(result);
    
    // play match button
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0007");
    await adbEx.Click(result);
    
    // FP on select friend page
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0009");
    await adbEx.Click(result);
    
    // kick off button
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0010");
    await adbEx.Click(result);
    
    // you win after match
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0011");
    await adbEx.Click(result);
    
    // dreamball after match - first clear reward
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0012");
    await adbEx.Click(result);
    
    // breakdown after match
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0013");
    await adbEx.Click(result);
    
    // go to scenario list - after match
    screenshot = await adbEx.Screenshot2();
    result = CvEx.Find2(screenshot, "0014");
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