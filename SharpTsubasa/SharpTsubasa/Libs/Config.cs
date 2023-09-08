using System.Reflection;
using IniParser;
using IniParser.Model;

namespace SharpTsubasa.Libs;

public class Config
{
    private static Config? instance;

    #region ini file

    public string NoxPath { get; set; }
    public string NoxAdb { get; set; }
    public string NoxNox { get; set; }
    public string NoxInstance { get; set; }
    public string NoxAttach { get; set; }

    public string Port { get; set; }

    #endregion

    #region computed

    public string Adb { get; set; }
    public string Nox { get; set; }

    #endregion

    private Config()
    {
        // Initialization code, if any
    }

    public static Config Load()
    {
        if (instance == null)
        {
            instance = new Config();
        }

        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile("Config.ini");
        instance.NoxPath = data["Nox"]["Path"];
        instance.NoxAdb = data["Nox"]["Adb"];
        instance.NoxNox = data["Nox"]["Nox"];
        instance.NoxInstance = data["Nox"]["Instance"];
        instance.NoxAttach = data["Nox"]["Attach"];
        instance.Port = data["Nox"]["Port"];

        // Get the location of the currently running executable
        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string platformToolsPath = Path.Combine(path, "platform-tools");

        instance.Adb = Path.Combine(platformToolsPath, "adb.exe");
        instance.Nox = Path.Combine(instance.NoxPath, instance.NoxNox);

        return instance;
    }
}