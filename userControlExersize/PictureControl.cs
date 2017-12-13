using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace userControlExersize
{
    class PictureControl : UserControl
    {

        private Panel MainPanel = new Panel() { Size = new Size(220,120), Visible = true,Location = new Point(10,10), BorderStyle = BorderStyle.Fixed3D};
        private PictureBox ImageBox = new PictureBox() { Size = new Size(120, 120), BorderStyle = BorderStyle.Fixed3D, Visible = true };
        private Label Name_lb = new Label() { Visible = true, Location = new Point(125, 10), Text = "Имя файла", Height = 20 };
        private Label Size_lb = new Label() { Visible = true, Location = new Point(125, 60), Text = "Размер", Height = 20 };
        private TextBox Name_tb = new TextBox() { Visible = true, Location = new Point(125, 30), Width = 75 };
        private TextBox Size_tb = new TextBox() { Visible = true, Location = new Point(125, 80), Width = 75, Enabled = false };

        public FileInfo PictureFile { get; private set; }

        private Image MiniBM;
        public PictureControl()
        {
            InitializeComponent();
            Name_tb.Leave += Name_tb_Leave;
        }

        void Name_tb_Leave(object sender, EventArgs e)
        {

            if (Name_tb.Text + ".jpg" != PictureFile.Name)
            {
                DialogResult result = MessageBox.Show("Изменить имя файла?", "Внимание!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    UpdateFile();
                else
                    Name_tb.Text = PictureFile.Name.Replace(".jpg", String.Empty);
            }
        }
        private void InitializeComponent()
        {
            this.Size = new Size(220, 120);
            this.Controls.Add(MainPanel);
            MainPanel.Controls.Add(ImageBox);
            MainPanel.Controls.Add(Name_lb);
            MainPanel.Controls.Add(Size_lb);
            MainPanel.Controls.Add(Name_tb);
            MainPanel.Controls.Add(Size_tb);
        }
        public void Build(FileInfo Picture)
        {
            PictureFile = Picture;
            using (var fstream = File.OpenRead(PictureFile.FullName))
            {
                MiniBM = Bitmap.FromStream(fstream); 
            }
            ImageBox.Image = new Bitmap(MiniBM, new Size(120, 120)); 
            Name_tb.Text = PictureFile.Name.Replace(".jpg", String.Empty);
            Size_tb.Text = PictureFile.Length.ToString();
            //MiniBM.Dispose();
        }
        public void UpdateFile()
        {
            if (PictureFile != null)
            {
                File.Move(PictureFile.FullName, PictureFile.DirectoryName + "/" + Name_tb.Text + ".jpg");
            }
        }
    }
}
