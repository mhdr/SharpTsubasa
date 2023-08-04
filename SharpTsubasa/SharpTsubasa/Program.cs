// See https://aka.ms/new-console-template for more information

using AdvancedSharpAdbClient;
using SharpTsubasa.Libs;

var config = Config.Load();

await Worker.RunAdb(config.AdbPath);