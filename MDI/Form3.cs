using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    public partial class Form3 : Form
    {
        public int iColorCount { get; set; }

        public Form3()
        {
            InitializeComponent();
            textBox1.Text = (hScrollBar1.Value).ToString();
        }
        public Image newImage; // 밝기 조절한 이미지 저장
        public Image image { get; set; } // 이미지 받아오기

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            iColorCount = hScrollBar1.Value; // 스크롤바 값
            textBox1.Text = iColorCount.ToString(); // 텍스트박스 변경

            label1.Invalidate(); // 이미지 그리기
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            Image I = image;
            Bitmap B = new Bitmap(I);

            for (int y = 0; y < B.Height; y++)
            {
                for (int x = 0; x < B.Width; x++)
                {
                    Color color = B.GetPixel(x, y);
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    // Saturation
                    if (iColorCount > 0)
                    {
                        r = r + iColorCount > 255 ? 255 : r + iColorCount;
                        g = g + iColorCount > 255 ? 255 : g + iColorCount;
                        b = b + iColorCount > 255 ? 255 : b + iColorCount;
                    }
                    else
                    {
                        r = r + iColorCount < 0 ? 0 : r + iColorCount;
                        g = g + iColorCount < 0 ? 0 : g + iColorCount;
                        b = b + iColorCount < 0 ? 0 : b + iColorCount;
                    }

                    B.SetPixel(x, y, Color.FromArgb(r, g, b)); // 변경된 값 넣기
                }
            }
            newImage = B; // 새로운 이미지 저장
            e.Graphics.DrawImage(newImage, 0, 0, label1.Width, label1.Height); // 이미지 그리기
        }
    }
}

