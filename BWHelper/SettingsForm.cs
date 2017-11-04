using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWHelper
{
    public partial class SettingsForm : Form
    {
        private ProcessSettings _settings;

        public SettingsForm(ProcessSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            settings.Changed += Settings_Changed;
            syncUI();
        }

        private void Settings_Changed(object sender, EventArgs e)
        {
            syncUI();
        }

        private void syncUI()
        {
            cb_negaviteColor.Checked = _settings.NegativeColor;
            cb_NegativeColorOffWithColorPage.Checked = _settings.NegativeColorOffWithColorPage;
            cb_NegativeColorOffWithPictureArea.Checked = _settings.NegativeColorOffWithPictureArea;

            cb_ReduceLight.Checked = _settings.ReduceLight;
            cb_ReduceLightOffWithColorPage.Checked = _settings.ReduceLightOffWithColorPage;
            cb_ReduceLightOffWithPictureArea.Checked = _settings.ReduceLightOffWithPictureArea;

            tb_ReduceLightRed.Value = _settings.ReduceLightRedPercent;
            tb_ReduceLightGreen.Value = _settings.ReduceLightGreenPercent;
            tb_ReduceLightBlue.Value = _settings.ReduceLightBluePercent;
        }

        private void applyFromUI()
        {
            _settings.Changed -= Settings_Changed;

            _settings.NegativeColor = cb_negaviteColor.Checked;
            _settings.NegativeColorOffWithColorPage = cb_NegativeColorOffWithColorPage.Checked;
            _settings.NegativeColorOffWithPictureArea = cb_NegativeColorOffWithPictureArea.Checked;

            _settings.ReduceLight = cb_ReduceLight.Checked;
            _settings.ReduceLightOffWithColorPage = cb_ReduceLightOffWithColorPage.Checked;
            _settings.ReduceLightOffWithPictureArea = cb_ReduceLightOffWithPictureArea.Checked;

            _settings.ReduceLightRedPercent = tb_ReduceLightRed.Value;
            _settings.ReduceLightGreenPercent = tb_ReduceLightGreen.Value;
            _settings.ReduceLightBluePercent = tb_ReduceLightBlue.Value;

            _settings.Changed += Settings_Changed;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            applyFromUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            applyFromUI();
            Hide();
        }

        private void link_email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetText("litfal1265@gmail.com");
            }
            catch (Exception)
            {
            }
            System.Diagnostics.Process.Start("mailto:litfal1265@gmail.com");
        }
    }
}
