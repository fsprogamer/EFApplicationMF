using System;
using System.Windows.Forms;
using EFApplicationMF.Method;

namespace EFApplicationMF
{
    public partial class MainForm : Form
    {
        ArtistSetController art_controller;
        public MainForm()
        {
            InitializeComponent();
            art_controller = new ArtistSetController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = art_controller.GetArtists();
            dataGridView1.DataSource = bindingSource1;
        }
             

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ArtistForm aForm = new ArtistForm();
            DialogResult result = aForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Artist artist = new Artist()
            {     
                Name = aForm.textBoxName.Text,
                Description = aForm.textBoxDescription.Text
            };
            art_controller.InsertArtist(artist);
            MessageBox.Show("Запись добавлена");
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                
                ArtistForm aForm = new ArtistForm();
                aForm.textBoxName.Text = dataGridView1["Name", index].Value.ToString();
                aForm.textBoxDescription.Text = dataGridView1["Description", index].Value.ToString();

                DialogResult result = aForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                Artist artist = new Artist() { Id=id,
                                               Name =aForm.textBoxName.Text,
                                               Description = aForm.textBoxDescription.Text
                };

                art_controller.UpdateArtist(artist);
                dataGridView1.Refresh();
                MessageBox.Show("Запись изменена");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Artist artist = new Artist() { Id = id };
                art_controller.DeleteArtist(artist);
                MessageBox.Show("Запись удалена");
            }

        }
    }
}
