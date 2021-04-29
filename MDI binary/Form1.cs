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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form2 Child = ActiveMdiChild as Form2; // 저장하려는 Form의 인스턴스 얻어옴
                if (Child != null)
                {
                    switch (saveFileDialog1.FilterIndex) // 파일 형식
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

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;

            if (original != null)
            {
                Form3 dig = new Form3();
                Image I = original.image;
                dig.image = I;

                if (dig.ShowDialog() == DialogResult.OK)
                {
                    Form2 Child = new Form2();
                    Child.image = dig.newImage; // 이미지 저장한 것을 그대로 사용
                    Child.MdiParent = this;
                    Child.Show(); // 밝기 조절한 이미지
                }
            }
        }
    }
}
