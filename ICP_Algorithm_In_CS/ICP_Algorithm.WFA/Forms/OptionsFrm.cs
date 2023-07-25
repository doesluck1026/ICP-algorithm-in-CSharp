using ICP_Algorithm.WFA.Languages;
using ICP_Algorithm.WFA.Methods;
using ICP_Algorithm.WFA.Properties;
using System;
using System.Windows.Forms;

namespace ICP_Algorithm.WFA.Forms
{
    public partial class OptionsFrm : Form
    {
        private bool cmbBoxLangsec;

        public OptionsFrm()
        {
            InitializeComponent();
            GetLang();
            GetLanguagesList();
            GetSettings();
        }

        private void GetLang()
        {
            this.Text = Localization.OptionsTitle;

            lbLang.Text = Localization.Language;

            tabPageGeneral.Text = Localization.General;
        }

        private void GetSettings()
        {
            cmbBoxLang.SelectedItem = Settings.Default.Language;
            cmbBoxLangsec = true;
        }

        private void GetLanguagesList()
        {
            cmbBoxLang.Items.Add(Languages.Languages.EnglishUK);
            cmbBoxLang.Items.Add(Languages.Languages.EnglishUS);
            cmbBoxLang.Items.Add(Languages.Languages.TurkishTR);
        }

        private void cmbBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxLangsec)
            {
                if (cmbBoxLang.Text == Languages.Languages.TurkishTR)
                {
                    SettingsMethods.SetLanguage(Languages.Languages.TurkishTR, Languages.Languages.TurkishCodeTR);
                }
                else if (cmbBoxLang.Text == Languages.Languages.EnglishUS)
                {
                    SettingsMethods.SetLanguage(Languages.Languages.EnglishUS, Languages.Languages.EnglishCodeUS);
                }
                else if (cmbBoxLang.Text == Languages.Languages.EnglishUK)
                {
                    SettingsMethods.SetLanguage(Languages.Languages.EnglishUK, Languages.Languages.EnglishCodeGB_UK);
                }
                else
                {
                    SettingsMethods.SetLanguage(Languages.Languages.EnglishUK, "");
                }
            }
        }
    }
}