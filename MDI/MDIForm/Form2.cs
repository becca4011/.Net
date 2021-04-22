using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Image image { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(image.Width, image.Height); 
            // 그림 크기와 창 크기를 같게 함
            // Size는 윈도우의 전체 크기 (타이틀바 등이 포함) 이므로 그림이 잘림
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }
    }
}