using Modio;
using Modio.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModIoModderClient
{
    public partial class FormAdd : Form
    {
        //public Mod Mod { get; private set;  }
        public ModClient ModClient { get; private set; }

        public FormAdd(ModClient modClient, Modio.Models.Mod mod)
        {
            InitializeComponent();
            //this.Mod = mod;
            this.ModClient = modClient;
            this.Text += $" => {mod.Name}";
        }

        private void btnSelectPackFolder_Click(object sender, EventArgs e)
        {
            DialogResult res = fbDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                txtFolder.Text = fbDialog.SelectedPath;
                IdentifyPackedFiles(fbDialog.SelectedPath);
            }
        }

        private void IdentifyPackedFiles(string folderPath)
        {
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(folderPath);
            FileInfo[] packedFiles = dirInfo.GetFiles("*.zip");
            IList<NewFile> newFiles = new List<NewFile>();
            
            foreach (FileInfo packedFile in packedFiles)
            {
                NewFile newFile = new NewFile(packedFile);
                SetPlatform(newFile);
                newFiles.Add(newFile);
            }

            dgvNewFiles.DataSource = newFiles;

            SetupGrid();
        }

        private async Task SetupGrid()
        {
            string[] visibleColumns = { "FileName", "Active", "FileSizeInMb", "Platform", "Version", "Changelog" };
            foreach (DataGridViewColumn col in dgvNewFiles.Columns)
            {
                col.Visible = visibleColumns.Contains(col.Name); //true;
            }

            string[] editableColumns = { "Active", "Version", "Changelog" };
            foreach (DataGridViewColumn col in dgvNewFiles.Columns)
            {
                col.ReadOnly = !editableColumns.Contains(col.Name); //true;
            }

            DataGridViewCheckBoxColumn uploadColumn = new DataGridViewCheckBoxColumn();
            uploadColumn.Name = "Upload";
            uploadColumn.HeaderText = "Upload?";
            

            // Add the button column to the control.
            dgvNewFiles.Columns.Add(uploadColumn);

            //DataGridViewCheckBoxColumn uploadedColumn = new DataGridViewCheckBoxColumn();
            //uploadedColumn.Name = "Uploaded";
            //uploadedColumn.HeaderText = "Uploaded";
            //uploadedColumn.ReadOnly = true;

            DataGridViewTextBoxColumn uploadedColumn = new DataGridViewTextBoxColumn();
            uploadedColumn.Name = "Uploaded";
            uploadedColumn.HeaderText = "Upload Status";
            uploadedColumn.ReadOnly = true;


            // Add the button column to the control.
            dgvNewFiles.Columns.Add(uploadedColumn);
        }

        private void SetPlatform(NewFile newFile)
        {
            string fileName = newFile.File.Name;
            if (fileName.Contains("_pc"))
                newFile.Platform = "windows";
            else if (fileName.Contains("_xbox_one"))
                newFile.Platform = "xboxone";
            else if (fileName.Contains("_xbox_series"))
                newFile.Platform = "xboxseriesx";
            else if (fileName.Contains("_playstation_4"))
                newFile.Platform = "ps4";
            else if (fileName.Contains("_playstation_5"))
                newFile.Platform = "ps5";
            else if (fileName.Contains("_nx64"))
                newFile.Platform = "switch";
        }

        private async void btnAddFiles_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show("Are you sure you want to upload the selected files?", "Confirm Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirmation == DialogResult.Yes)
                await UploadFiles();
        }

        private async Task UploadFiles()
        {
            SetControlsState(false);

            try
            {
                foreach (DataGridViewRow row in dgvNewFiles.Rows)
                {
                    object val = row.Cells["Upload"].Value;
                    if (val != null && (bool)val == true)
                    {
                        row.Cells["Uploaded"].Value = "Pending";
                    }
                    //else
                    //{
                    //    row.Cells["Uploaded"].Value = "";
                    //}
                }

                FilesClient modFiles = ModClient.Files;

                foreach (DataGridViewRow row in dgvNewFiles.Rows)
                {
                    object val = row.Cells["Upload"].Value;
                    if (val != null && (bool)val == true)
                    {
                        NewFile file = row.DataBoundItem as NewFile;
                        try
                        {
                            
                            file.Changelog ??= txtChangeLog.Text;
                            file.Version ??= txtVersion.Text;
                            //newFiles.Add(data);

                            row.Cells["Uploaded"].Value = "In-Progress";

                            Modio.Models.File uploadedFile = await modFiles.Add(file);

                            row.Cells["Uploaded"].Value = "Uploaded";
                            row.Cells["Upload"].Value = false;
                        }
                        catch(Exception ex)
                        {
                            row.Cells["Uploaded"].Value = "Failed";
                            var logger = LogManager.GetCurrentClassLogger();
                            logger.Error(ex, $"Upload failed for {file.FileName}");
                        }
                    }
                }

            }
            finally
            {
                SetControlsState(true);
            }
        }

        private void SetControlsState(bool enabled)
        {
            txtVersion.Enabled = enabled;
            txtChangeLog.Enabled = enabled;
            btnSelectPackFolder.Enabled = enabled;  
            dgvNewFiles.Enabled = enabled;
            btnClose.Enabled = enabled;
            btnAddFiles.Enabled = enabled;
            Cursor.Current = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
