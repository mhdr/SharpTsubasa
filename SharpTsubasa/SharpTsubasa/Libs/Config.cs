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
        instance.Adb = Path.Combine(instance.NoxPath, instance.NoxAdb);
        instance.Nox = Path.Combine(instance.NoxPath, instance.NoxNox);

        return instance;
    }
}