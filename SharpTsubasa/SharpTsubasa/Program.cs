// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Drawing.Imaging;
using AdvancedSharpAdbClient;
using SharpTsubasa.Libs;
using OpenCvSharp;
using Point = OpenCvSharp.Point;

var nox = new NoxEx();
bool shouldRun = true;

await AdbEx.RunAdb();
Console.WriteLine("running nox...");
nox.RunNox();
await Task.Delay(1000 * 60);
Console.WriteLine("running Captain Tsubasa app...");
nox.RunApp();
await Task.Delay(1000 * 60);

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
    Console.WriteLine("killing nox...");
    shouldRun = false;
};

while (shouldRun)
{
    Image screenshot = (Image)await client.GetFrameBufferAsync(device, CancellationToken.None);

    screenshot.Save("screenshot.png", ImageFormat.Png);
    var src = Cv2.ImRead("screenshot.png");
    var tmp = Cv2.ImRead("templates/0001.png");

    var result = src.MatchTemplate(tmp, TemplateMatchModes.CCoeffNormed);

    double minVal, maxVal;
    Point minLoc, maxLoc;

    result.MinMaxLoc(out minVal, out maxVal, out minLoc, out maxLoc);

    Console.WriteLine("minLoc: {0}, minVal: {1}, maxLoc: {2}, maxVal: {3}", minLoc, minVal, maxLoc, maxVal);
    await Task.Delay(1000);
}

nox.CloseNox();
await Task.Delay(1000 * 10);
nox.KillNox();
Console.WriteLine("exit bot.");