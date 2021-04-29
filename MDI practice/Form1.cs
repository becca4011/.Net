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

                Form3 Child = new Form3();
                Child.image = I;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 original = (Form3)this.ActiveMdiChild;

            if (original != null)
            {
                Form2 dig = new Form2();
                Image I = original.image;
                dig.image = I;
                if (dig.ShowDialog() == DialogResult.OK)
                {
                    Form3 Child = new Form3();
                    Child.image = dig.newImage; // 이미지 저장한 것을 그대로 사용
                    Child.MdiParent = this;
                    Child.Show(); // 밝기 조절한 이미지
                }
            }
        }
    }
}
