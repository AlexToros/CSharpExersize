using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace userControlExersize
{
    public partial class Form1 : Form
    {
        string Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        FileInfo[] JpgFiles;
        List<PictureControl> MyControls;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Folder);
            JpgFiles = dir.EnumerateFiles("*.jpg",SearchOption.AllDirectories).ToArray();
            
            MyControls = new List<PictureControl>();
            foreach (FileInfo picture in JpgFiles)
            {
                PictureControl PicControl = new PictureControl();
                PicControl.Build(picture);
                MyControls.Add(PicControl);
            }
            for (int i = 0, x = 0, y = 0; i < MyControls.Count; i++)
            {
                PictureControl pic = MyControls[i];
                pic.Location = new Point(x + 10, y + 10);
                this.panel1.Controls.Add(pic);
                x += pic.Width;
                if (x > this.Width - pic.Width - 10)
                {
                    x = 0;
                    y += pic.Height;
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            this.SuspendLayout();
            for (int i = 0, x = 0, y = 0; i < MyControls.Count; i++)
            {
                PictureControl pic = MyControls[i];
                pic.Location = new Point(x + 10, y + 10);
                x += pic.Width;
                if (x > this.Width - pic.Width - 10)
                {
                    x = 0;
                    y += pic.Height;
                }
            }

            this.ResumeLayout();
        }
    }
}
