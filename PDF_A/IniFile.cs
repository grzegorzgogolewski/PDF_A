using System;
using System.IO;
using System.Reflection;
using IniParser;
using IniParser.Model;

namespace PDF_A
{
    public static class IniFile
    {
        public static void SaveIni(string section, string key, string value)
        {
            string startDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ;
            string cfgfilePath = Path.Combine(startDirectory ?? throw new InvalidOperationException(), "PDF_A.ini");

            if (!File.Exists(cfgfilePath)) File.Create(cfgfilePath).Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile(cfgfilePath);
            iniFile[section][key] = value;

            parser.WriteFile(cfgfilePath, iniFile);
        }

        public static string ReadIni(string section, string key)
        {
            string startDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ;
            string cfgfilePath = Path.Combine(startDirectory ?? throw new InvalidOperationException(), "PDF_A.ini");

            if (!File.Exists(cfgfilePath)) File.Create(cfgfilePath).Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile(cfgfilePath);
            return iniFile[section][key];
        }

    }
}