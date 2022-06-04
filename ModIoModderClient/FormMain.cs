using Modio;
using Modio.Models;
using NLog;
using System.ComponentModel;

namespace ModIoModderClient
{
    public partial class FormMain : Form
    {
        private Client client;
        private Mod selectedMod;
        private ModClient modClient;

        public FormMain()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await GetAllMods();
        }

        private async Task GetAllMods()
        {
            string apiKey = string.Empty; // Program.Configuration["modioApiKey"];
            string authToken = Program.Configuration["modioAuthToken"];

            if (string.IsNullOrEmpty(authToken))
            {
                MessageBox.Show("ModIo Auth Token has to be set before using this app", "Missing Auth Token", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string httpTimeOutInMins = Program.Configuration["httpTimeOutInMins"];
            int timeoutInMins;
            if (int.TryParse(httpTimeOutInMins, out timeoutInMins))
                client = new Client(new Credentials(apiKey, authToken), timeoutInMins);
            else
                client = new Client(new Credentials(apiKey, authToken), 2); //Connection with default timeout;

            User user = await client.User.GetCurrentUser();

            this.Text += $" - {user.Username}";

            IReadOnlyList<Mod> userMods = await client.User.GetMods().ToList();

            cboMods.ValueMember = "Id";
            cboMods.DisplayMember = "Name";
            cboMods.DataSource = userMods;

        }

        private async void btnGetModFiles_Click(object sender, EventArgs e)
        {
            await GetModDetails();
        }

        private async Task GetModDetails()
        {
            selectedMod = (Mod)cboMods.SelectedItem;
            if (null == selectedMod)
            {
                MessageBox.Show("ModIo Auth Token has to be set before using this app", "Missing Auth Token", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            GameClient gameClient = client.Games[selectedMod.GameId];
            modClient = gameClient.Mods[selectedMod.Id];

            /*
            List<Modio.Models.File> files = new List<Modio.Models.File>(await modClient.Files.Search().ToList());

            var list = new BindingList<Modio.Models.File>(files);            

            dgvModFiles.DataSource = list;
            */
            IReadOnlyList<Modio.Models.File> files = await modClient.Files.Search().ToList();
            files = files.OrderByDescending(f => f.DateAdded).ToList();
            dgvModFiles.DataSource = files;

            await SetupGrid();
        }

        private async Task SetupGrid()
        {
            string[] visibleColumns = { "Id", "Filename", "Version", "Changelog", "FileSizeInMb", "DateAddedLocal", "MD5Sum", "ApprovedPlatforms", "PendingPlatforms" };
            foreach (DataGridViewColumn col in dgvModFiles.Columns)
            {
                col.Visible = visibleColumns.Contains(col.Name); //true;
            }

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Delete";
            deleteColumn.Text = "X";
            deleteColumn.UseColumnTextForButtonValue = true;


            // Add the button column to the control.
            dgvModFiles.Columns.Add(deleteColumn);

            btnAddFiles.Visible = true;
        }

        private async void btnAddFiles_Click(object sender, EventArgs e)
        {
            FormAdd fa = new FormAdd(modClient, selectedMod);
            fa.ShowDialog(this);

            await GetModDetails();
        }

        private async void dgvModFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                //They clicked the header column, do nothing
                return;
            }

            if (grid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
            {
                DataGridViewColumn clickedColumn = grid.Columns[e.ColumnIndex];
                if (clickedColumn.Name == "Delete")
                {
                    DialogResult confirmation = MessageBox.Show("Are you sure you want to delete this file?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (confirmation == DialogResult.Yes)
                    {
                        try
                        {
                            Modio.Models.File fileToDelete = (Modio.Models.File)grid.Rows[e.RowIndex].DataBoundItem;
                            await modClient.Files.Delete(fileToDelete.Id);
                            MessageBox.Show("The file was deleted successfully", "File Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await GetModDetails();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"The file could not be deleted. Reason: {ex.Message}", "File Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            var logger = LogManager.GetCurrentClassLogger();
                            logger.Error(ex, $"File deletion failed");
                        }
                    }

                }
            }
        }

        private void cboMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMod = (Mod)cboMods.SelectedItem;
            btnGetModFiles.Enabled = (null != selectedMod);
        }
            
    }
}