using IniParser;
using IniParser.Model;

namespace SharpTsubasa.Libs;

public class Config
{
    private static Config? instance;

    #region ini file

    public string NoxPath { get; set; }
    public string NoxAdb { get; set; }

    #endregion

    #region computed

    public string AdbPath { get; set; }

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
        instance.AdbPath = Path.Combine(instance.NoxPath, instance.NoxAdb);

        return instance;
    }
}