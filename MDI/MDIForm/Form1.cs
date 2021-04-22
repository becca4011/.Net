using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    // ★isMdiContainer를 True로 바꿔주기
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                Image I = Image.FromFile(op.FileName);

                Form2 Child = new Form2();
                Child.image = I;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form2 Child = ActiveMdiChild as Form2; // 저장하려는 Form의 인스턴스 얻어옴
                if (Child != null)
                {
                    switch(saveFileDialog1.FilterIndex) // 파일 형식
                    {
                        case 1:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 2:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case 3:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }

        private void 닫기CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 포커스를 가지고 있는 윈도우를 닫기
            Form Child = ActiveMdiChild;
            if (Child != null)
            {
                Child.Close();
            }
        }

        private void 밝기조절ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild; // 활성화 된 창 가지고오기, 캐스팅 주의
            if (Child != null)
            {
                Image I = Child.image;
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
                        if (((ToolStripMenuItem)sender).Text.Equals("밝게하기"))
                        {
                            // 밝게하기
                            r = r + 50 > 255 ? 255 : r + 50;
                            g = g + 50 > 255 ? 255 : g + 50;
                            b = b + 50 > 255 ? 255 : b + 50;
                        }
                        else
                        {
                            // 어둡게하기
                            r = r - 50 < 0 ? 0 : r - 50;
                            g = g - 50 < 0 ? 0 : g - 50;
                            b = b - 50 < 0 ? 0 : b - 50;
                        }
                        B.SetPixel(x, y, Color.FromArgb(r, g, b)); // 변경된 값 넣기
                    }
                }
                Child = new Form2();
                Child.image = B; // 밝게, 어둡게 된 사진
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 계단식정렬CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 수평바둑판정렬HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 수직바둑판정렬VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void 흑백ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;

            if (Child != null)
            {
                Image I = Child.image;
                Bitmap B = new Bitmap(I);

                for (int y = 0; y < B.Height; y++)
                {
                    for (int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        byte r = (byte)((color.R + color.G + color.B) / 3); // RGB 평균값, 밝기만 남음
                        byte g = r;
                        byte b = r;

                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                Child = new Form2();
                Child.image = B; // 흑백 사진
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 이진화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 값이 127보다 작으면 0 / 127보다 크면 255 (검정, 흰색 2가지 값으로만 이루어짐)
            Form2 Child = (Form2)this.ActiveMdiChild;

            if (Child != null)
            {
                Image I = Child.image;
                Bitmap B = new Bitmap(I);

                for (int y = 0; y < B.Height; y++)
                {
                    for (int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        byte r = (byte)((color.R + color.G + color.B) / 3); // RGB 평균값, 밝기만 남음
                        r = r > (byte)127 ? (byte)255 : (byte)0;
                        byte g = r;
                        byte b = r;

                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                Child = new Form2();
                Child.image = B; // 이진화 사진
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;

            if (Child != null)
            {
                Image I = Child.image;
                Bitmap B = new Bitmap(I);

                for (int y = 0; y < B.Height; y++) // 이미지 높이
                {
                    for (int x = 0; x < B.Width; x++) // 이미지 너비
                    {
                        Color color = B.GetPixel(x, y); // GetPixel로 픽셀 하나 가져옴 (0, 0), (0, 1), ...
                        byte r = (byte)~color.R; // ~ : 반전
                        byte g = (byte)~color.G;
                        byte b = (byte)~color.B;
                        
                        B.SetPixel(x, y, Color.FromArgb(r, g, b)); // 반전시킨 픽셀 적용
                    }
                }
                Child = new Form2();
                Child.image = B; // 반전 사진
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;

            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);

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
             
                Bitmap Smoothing = new Bitmap(original.image);
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
                Form2 Child = new Form2();
                Child.image = Smoothing;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void edgeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // 경계 찾기 (주변 색깔의 값에서 값이 바뀌는 경우를 경계)
            Form2 original = ActiveMdiChild as Form2;

            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);

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

                Bitmap Edge = new Bitmap(original.image);
                int[,] m = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } }; // 소벨(Sobel) 마스크 (좌측과 우측의 차이, x 방향 차이)
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
                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R; // 마스크 안의 값들을 다 더함 (RGB 값은 다 같음)
                            }
                        }
                        sum = Math.Abs(sum); // 절댓값 취하기 (갑자기 어두워지면 - 차이, 갑자기 밝아지면 + 차이 / 차이 값만 필요해서 절댓값)
                        if (sum > 255) sum = 255;
                        //if (sum < 0) sum = 0;
                        Edge.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }
                }
                Form2 Child = new Form2();
                Child.image = Edge;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 중위수
            Form2 original = ActiveMdiChild as Form2;

            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);

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

                Bitmap Median = new Bitmap(original.image);
                int[] m = new int[9];

                // 바깥 테두리를 제외한 픽셀
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                            {
                                m[(r + 1) * 3 + (c + 1)] = gBitmap.GetPixel(x + r, y + c).R; // 9개의 값을 m 배열에 가지고 옴
                            }
                        }
                        Array.Sort(m); // 오름차순 정렬
                        Median.SetPixel(x, y, Color.FromArgb(m[4], m[4], m[4])); // 정렬한 값 중 4번째에 있는 값을 적용
                    }
                }
                Form2 Child = new Form2();
                Child.image = Median;
                Child.MdiParent = this;
                Child.Show();
            }
        }

    }
}