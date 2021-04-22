using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DrawLines
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        CMyData data;
        private int x, y;
        private int iCurrentPenWidth;
        private Color iCurrentPenColor;
        //private ArrayList ar;

        public Form1()
        {
            // 초기값
            total_lines = new LinkedList<CMyData>();
            iCurrentPenWidth = 2;
            iCurrentPenColor = Color.Black;
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                data = new CMyData();
                data.Color = iCurrentPenColor;
                data.Width = iCurrentPenWidth;
                //ar = new ArrayList();
                //ar.Add(new Point(x, y));
                data.AR.Add(new Point(x, y));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                //펜 설정하기(색깔과 굵기) 
                Pen p = new Pen(data.Color, data.Width);
                G.DrawLine(p, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                data.AR.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData line in total_lines)
            {
                Pen p = new Pen(line.Color, line.Width);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e) // 선색깔 메뉴에서 red 클릭
        {
            iCurrentPenColor = Color.FromArgb(255, 0, 0);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e) // 선색깔 메뉴에서 green 클릭
        {
            iCurrentPenColor = Color.FromArgb(0, 255, 0);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e) // 선색깔 메뉴에서 blue 클릭
        {
            iCurrentPenColor = Color.FromArgb(0, 0, 255);
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e) // Form2 띄우기
        {
            Form2 dlg = new Form2();
            dlg.iDialogPenColor = iCurrentPenColor; // 대화상자에 현재 선택되어 있는 항목을 전달 (프로퍼티의 set)
            dlg.iDialogPenWidth = iCurrentPenWidth;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                iCurrentPenColor = dlg.iDialogPenColor; // 프로퍼티의 get
                iCurrentPenWidth = dlg.iDialogPenWidth;
            }
            dlg.Dispose();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e) // 선색깔 메뉴에서 체크 하기
        {
            redToolStripMenuItem.Checked = (iCurrentPenColor == Color.FromArgb(255, 0, 0));
            greenToolStripMenuItem.Checked = (iCurrentPenColor == Color.FromArgb(0, 255, 0));
            blueToolStripMenuItem.Checked = (iCurrentPenColor == Color.FromArgb(0, 0, 255));
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e) // 저장하기
        {
            // saveFileDialog1.InitialDirectory = "C:\\"; // 어디서 출발할 것인지
            saveFileDialog1.Title = "저장하기";
            saveFileDialog1.Filter = "Bin 파일|*.bin";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(fs, total_lines);
                    // total_lines 인스턴스를 바이너리로 포멧해서 fs에 저장 (어떤 인스턴스가 들어와도 포멧 가능)

                    fs.Close();

                    MessageBox.Show("파일을 저장했습니다.");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("지정한 파일이 없습니다.");
                }
            }
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e) // 불러오기
        {
            // ★불러올 때 파일 더블클릭하면 오류 발생★
            // 더블클릭 하지 말고 열기 클릭하기

            // openFileDialog1.InitialDirectory = "C:\\"; // 어디서 출발할 것인지
            openFileDialog1.Title = "불러오기";
            openFileDialog1.Filter = "Bin 파일|*.bin";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    total_lines = (LinkedList<CMyData>)bf.Deserialize(fs); // 캐스팅 (LinkedList<CMyData>)
                    fs.Close();

                    Invalidate();

                    MessageBox.Show("파일을 불러왔습니다.");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("지정한 파일이 없습니다.");
                }
            }
        }
    }

    [Serializable] // 시리얼라이즈 쓸 때 필요
    class CMyData
    {
        private ArrayList Ar;

        public CMyData()  //생성자함수
        {
            Ar = new ArrayList();
        }
        public Color Color
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public ArrayList AR
        {
            get { return Ar; }
        }
    }
}
