using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2019_연습
{
    public partial class Form2 : Form
    {
        public int scroll;
        public Image image { get; set; }
        public Image newImage;

        int mode = 1;

        public Form2()
        {
            InitializeComponent();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            mode = 0;
            scroll = hScrollBar1.Value;
            label1.Invalidate();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = 1;
            label1.Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mode = 2;
            label1.Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = 3;
            label1.Invalidate();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            Image I = image;
            Bitmap B = new Bitmap(I);

            if (mode == 0) // 밝기조절
            {
                radioButton1.Checked = true;
                for (int y = 0; y < B.Height; y++)
                {
                    for (int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        int r = color.R;
                        int g = color.G;
                        int b = color.B;

                        // Saturation
                        if (scroll > 0)
                        {
                            r = r + scroll > 255 ? 255 : r + scroll;
                            g = g + scroll > 255 ? 255 : g + scroll;
                            b = b + scroll > 255 ? 255 : b + scroll;
                        }
                        else
                        {
                            r = r + scroll < 0 ? 0 : r + scroll;
                            g = g + scroll < 0 ? 0 : g + scroll;
                            b = b + scroll < 0 ? 0 : b + scroll;
                        }

                        B.SetPixel(x, y, Color.FromArgb(r, g, b)); // 변경된 값 넣기
                    }
                }
                newImage = B;
            }
            else if (mode == 1) // 원본
            {
                newImage = image;
            }
            else if (mode == 2) // 스무딩
            {
                Bitmap gBitmap = new Bitmap(image);

                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed") // 흑백 이미지가 아닐 경우 (컬러 이미지인 경우) / Format8bppIndexed : 흑백
                {
                    // 흑백 이미지 만들기
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }
                }

                Bitmap Smoothing = new Bitmap(image);
                int[,] m = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }; // 3X3 마스크
                int sum;

                // 바깥 테두리를 제외한 픽셀
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        // 마스크 씌우기
                        sum = 0;
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                            {
                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R; // 마스크 안의 값들을 다 더함
                            }
                        }
                        sum = Math.Abs(sum); // 절댓값 취하기
                        sum /= 9; // 평균 (과도하게 밝거나, 어두운 부분 완화)
                        if (sum > 255) sum = 255;
                        if (sum < 0) sum = 0;
                        Smoothing.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }
                }
                newImage = Smoothing;
            }
            else if (mode == 3) // NTSC
            {
                Bitmap NTSC = new Bitmap(image);
                for (int y = 0; y < NTSC.Height; y++) // 이미지 높이
                {
                    for (int x = 0; x < NTSC.Width; x++) // 이미지 너비
                    {
                        Color color = NTSC.GetPixel(x, y);
                        int r = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
                        int g = r;
                        int b = r;
                        
                        NTSC.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                newImage = NTSC;
            }

            e.Graphics.DrawImage(newImage, 0, 0, label1.Width, label1.Height);
        }
    }
}
