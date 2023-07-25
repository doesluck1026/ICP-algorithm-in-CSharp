using ICP_Algorithm.WFA.Languages;
using ICP_Algorithm.WFA.Properties;
using System.Globalization;
using System.Windows.Forms;

namespace ICP_Algorithm.WFA.Methods
{
    internal class SettingsMethods
    {
        public static void SetLanguage(string languageName, string languageCode)
        {
            Localization.Culture = new CultureInfo(languageCode);
            Settings.Default.Language = languageName;
            Settings.Default.Save();
            Application.Restart();
        }
    }
}