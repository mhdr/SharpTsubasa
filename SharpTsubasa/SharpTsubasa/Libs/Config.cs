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
        instance.NoxPath = data["Android"]["Path"];
        instance.NoxAdb = data["Android"]["Adb"];
        
        instance.Adb = Path.Combine(instance.NoxPath, instance.NoxAdb);
        return instance;
    }
}