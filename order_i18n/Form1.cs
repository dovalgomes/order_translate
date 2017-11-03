using System;
using System.Windows.Forms;

using order_i18n_core.interfaces;
using order_i18n_core.models;
using System.IO;

namespace order_i18n
{
    public partial class FormPrincipal : Form
    {

        public IJSONManager fileManager { get; private set; }

        const string fileTypes = "JSON Files|*json";

        public FormPrincipal()
        {
            InitializeComponent();
            SetEvents();
        }

        private void SetEvents()
        {
            btnSearch.Click += btnSearchClick;
            btnClear.Click += btnClearClick;
            btnProcess.Click += btnProcessClick;
            btnProcess.Enabled = false;
        }

        private void btnProcessClick(object sender, EventArgs e)
        {
            try
            {
                this.fileManager = new JSONManager(boxSource.Text);
                saveFileDialog.Filter = fileTypes;
                saveFileDialog.Title = "Translate File";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                saveFileDialog.DefaultExt = ".json";
                saveFileDialog.ShowDialog();
                saveFileDialog.RestoreDirectory = true;
                var extension = Path.GetExtension(saveFileDialog.FileName);
                saveFileDialog.DefaultExt = extension;
                this.fileManager.GenerateFile(saveFileDialog.FileName);
                MessageBox.Show("Your file was successfully generated in " + saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClearClick(sender, e);
            }
        }

        private void btnClearClick(object sender, EventArgs e)
        {
            if (fileManager != null)
            {
                this.fileManager.ClearObject();
                this.btnProcess.Enabled = false;
                boxSource.Text = string.Empty;
            }
        }
        private void btnSearchClick(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = fileTypes;
                this.openFileDialog.ShowDialog();
                this.btnProcess.Enabled = !string.IsNullOrEmpty(openFileDialog.FileName);
                this.fileManager = new JSONManager(openFileDialog.FileName);
                this.boxSource.Text = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.boxSource.Focus();
            }
        }
    }
}