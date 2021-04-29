using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021_중간
{
    public partial class Form3 : Form
    {
        public int scroll;

        public Form3()
        {
            InitializeComponent();
            scroll = hScrollBar1.Value;
            label4.Text = scroll.ToString();
        }

        public Image image { get; set; }
        public Image bwImage { get; set; }
        public Image newImage;

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            scroll = hScrollBar1.Value;
            label4.Text = scroll.ToString();
            label2.Invalidate();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap B = new Bitmap(image);

            if (B.PixelFormat.ToString() != "Format8bppIndexed") // 흑백 이미지가 아닐 경우 (컬러 이미지인 경우) / Format8bppIndexed : 흑백
            {
                // 흑백 이미지 만들기
                for (int i = 0; i < B.Height; i++)
                {
                    for (int j = 0; j < B.Width; j++)
                    {
                        Color color = B.GetPixel(j, i);
                        int c = (color.R + color.G + color.B) / 3;
                        B.SetPixel(j, i, Color.FromArgb(c, c, c));
                    }
                }
            }

            bwImage = B;
            e.Graphics.DrawImage(bwImage, 0, 0, label1.Width, label1.Height);
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            Bitmap B = new Bitmap(image);

            for (int i = 0; i < B.Height; i++)
            {
                for (int j = 0; j < B.Width; j++)
                {
                    Color color = B.GetPixel(j, i);
                    int c = color.R > scroll ? 255 : 0;
                    B.SetPixel(j, i, Color.FromArgb(c, c, c));
                }
            }
            newImage = B;
            e.Graphics.DrawImage(newImage, 0, 0, label2.Width, label2.Height);
        }
    }
}
