using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLines
{
    // ★대화상자 중간에 100% 나옴
    // 대화상자에서 이미지 조정
    public partial class Form2 : Form
    {
        public Color iDialogPenColor { get; set; }
        public int iDialogPenWidth { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // 콤보박스 항목에 있는 것을 선택
        {
            // iDialogPenWidth = (((ComboBox)sender).SelectedIndex + 1) * 2;
            iDialogPenWidth = int.Parse(comboBox1.Text);
            label5.Invalidate(); // 다시 그리기
        }

        private void Form2_Load(object sender, EventArgs e) // Form2가 처음 뜰 때
        {
            // 항목 추가하고, 보이기
            for (int i = 2; i <= 10; i += 2)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = iDialogPenWidth.ToString();
            hScrollBar1.Value = iDialogPenColor.R;
            hScrollBar2.Value = iDialogPenColor.G;
            hScrollBar3.Value = iDialogPenColor.B;
            textBox1.Text = (iDialogPenColor.R).ToString();
            textBox2.Text = (iDialogPenColor.G).ToString();
            textBox3.Text = (iDialogPenColor.B).ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e) // 콤보박스에 값을 입력
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
            label5.Invalidate(); // 다시 그리기
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e) // 스크롤바를 움직임
        {
            // 스크롤바의 maximum을 264로 설정하면 255까지 감
            iDialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = (hScrollBar1.Value).ToString();
            textBox2.Text = (hScrollBar2.Value).ToString();
            textBox3.Text = (hScrollBar3.Value).ToString();
            label5.Invalidate(); // 다시 그리기
        }

        private void label5_Paint(object sender, PaintEventArgs e) // 그리기
        {
            // 아래 주석으로 하면 label5 위치에 그리는 것이 아니라 Form2에 그림
            // Graphics g = CreateGraphics(); // g에는 그리기 도구와 어디에 그려야 할 지(윈도우)를 저장하고 있음 (그릴 윈도우는 Form2)
            // g.DrawLine(new Pen(iDialogPenColor, iDialogPenWidth), 0, label5.Height / 2, label5.Width, label5.Height / 2);

            e.Graphics.DrawLine(new Pen(iDialogPenColor, iDialogPenWidth), 0, label5.Height / 2, label5.Width, label5.Height / 2);
            // 펜 설정(색, 굵기), x1(왼쪽, 시작점), y1(왼쪽, 시작점), x2(오른쪽, 끝점), y2(오른쪽, 끝점)
            // PaintEventArgs e는 label5에 그려야 한다는 것을 가짐
        }
    }
}
