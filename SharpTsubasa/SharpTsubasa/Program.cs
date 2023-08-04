// See https://aka.ms/new-console-template for more information

using SharpTsubasa.Libs;

var config = Config.Load();
Console.WriteLine(config.AdbPath);