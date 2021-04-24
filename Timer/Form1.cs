using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerEx
{
    public partial class Form1 : Form
    {
        private char current_char;
        private Point char_position;
        private int all, score;
    
        private void next_char()
        {
            all++;
            Random random = new Random();
            //current_char = (char)random.Next('a', 'z' + 1); // cast error 나면
            current_char = Convert.ToChar(random.Next('a', 'z' + 1));
            char_position.X = random.Next(10, ClientRectangle.Width - 10);
            char_position.Y = 0;
            Invalidate();
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == current_char)
            {
                score++;
            }
            next_char();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            next_char();
            Timer T = new Timer();
            T.Interval = 500;
            T.Tick += new EventHandler(Form1_Timer); // 이벤트 핸들러 추가
            T.Start(); // 1초 간격으로 Form1_Timer 호출
        }

        private void Form1_Timer(object sender, System.EventArgs e) // 이벤트(번개) 아님
        {
            char_position.Y += 100;

            // 문자가 창 크기를 벗어나면 다음 문자 출력
            if (char_position.Y > ClientRectangle.Height) // ClientRactangle.Width : 윈도우의 폭 / ClientRactangle.Height : 윈도우의 높이
            {
                next_char();
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("모든 문자수 : " + all, this.Font, Brushes.Black, 10, 10);
            e.Graphics.DrawString("맞춘 문자수 : " + score, this.Font, Brushes.Black, 10, 30);
            e.Graphics.DrawString(current_char.ToString(), this.Font, Brushes.Black, char_position.X, char_position.Y);
        }
    }
}
