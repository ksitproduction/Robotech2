using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboTech1
{

    public partial class Form1 : Form
    {
        List<MyPoint> points = new List<MyPoint>(); //список точек
        Pen pn = new Pen(Color.Black, 5);  // Перо для рисования
        int angle = 0;
        int step = 6;
        double r = 0;
        Graphics g;
        int redPoint = 0;

        public Form1()
        {
            InitializeComponent(); //иниализация внешнего вида формы
            g = this.CreateGraphics();
            KeyPreview = true;
            this.label1.Text = "";
            this.comboBox1.Visible = false;
            this.upBtn.Visible = false;
            this.downBtn.Visible = false;
            groupBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            comboBox1.Visible = true;
            upBtn.Visible = true;
            downBtn.Visible = true;
            points.Clear();
            Draw();
        }

        private void Draw()
        {
            g.Clear(Color.White);
            if (pointsCount.Text != "")  //разрешить ввод только цифр (на будущее)
            {
                int numOfPoints = Convert.ToInt32(pointsCount.Text);
                if ((numOfPoints <= 1) || (numOfPoints > 9))
                {
                    MessageBox.Show("Введите число от 1 до 9");
                }
                else
                {
                    comboBox1.Items.Clear();
                    g.DrawEllipse(pn, 300, 300, 10, 10); //начальная точка

                    for (int i = 0; i < numOfPoints; i++)
                    {
                        points.Add(new MyPoint(300 + i * 10, 300 - i * 30, i));
                    }

                    foreach (MyPoint p in points)
                    {
                        g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
                    }

                    for (int i = 0; i < points.Count; i++)
                    {
                        if (points[i].Index > 0)
                        {
                            g.DrawLine(Pens.Black, new System.Drawing.Point(Convert.ToInt32(points[i].X), Convert.ToInt32(points[i].Y)), new System.Drawing.Point(Convert.ToInt32(points[i - 1].X), Convert.ToInt32(points[i - 1].Y)));
                        }
                    }
                }
            }
        } //функция начальной отрисовки

        private void Draw2(List<MyPoint> list)
        {
            g.Clear(Color.White);
            g.DrawEllipse(pn, 300, 300, 10, 10); //начальная точка

            foreach (MyPoint p in list)
            {
                g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Index > 0)
                {
                    g.DrawLine(Pens.Black, new System.Drawing.Point(Convert.ToInt32(list[i].X), Convert.ToInt32(points[i].Y)), new System.Drawing.Point(Convert.ToInt32(list[i - 1].X), Convert.ToInt32(list[i - 1].Y)));

                }
            }

            if (list.Count != 0)
            {
                foreach (MyPoint p in list)
                {
                    if (p.Index == redPoint)
                    {
                        pn = new Pen(Color.Red, 5);  // Перо для рисования
                        g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
                    }
                    else
                    {
                        pn = new Pen(Color.Black, 5);  // Перо для рисования
                        g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
                    }
                }
            }
        } //отрисовка при нажатии кнопок "Верх, Вниз"

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (stepTextBox.Text != "")
                step = Convert.ToInt32(stepTextBox.Text.ToString());
            g.Clear(Color.White);
            foreach (MyPoint p in points)
            {
                if (p.Index > redPoint)
                {
                    if (p.Index == (redPoint + 1))
                    {
                        angle = Convert.ToInt32(Math.Round((Math.Atan2((p.Y - points[p.Index - 1].Y), (p.X - points[p.Index - 1].X))) * 180 / Math.PI));
                    }
                    if (p.Index > (redPoint + 1))
                    {
                        angle = Convert.ToInt32(Math.Round((Math.Atan2((p.Y - points[redPoint].Y), (p.X - points[redPoint].X))) * 180 / Math.PI));
                    }
                    r = Math.Sqrt(((p.X - points[redPoint].X) * (p.X - points[redPoint].X)) + ((p.Y - points[redPoint].Y) * (p.Y - points[redPoint].Y)));

                    p.X = Math.Round(points[redPoint].X + r * Math.Cos((angle - step) * Math.PI / 180));
                    p.Y = Math.Round(points[redPoint].Y + r * Math.Sin((angle - step) * Math.PI / 180));

                    textBox1.Text = (angle - step).ToString();
                }
            }
            Draw2(points);
            comboBox1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (points.Count != 0)
            {
                foreach (MyPoint p in points)
                {
                    if (p.Index == (comboBox1.SelectedIndex))
                    {
                        redPoint = p.Index;
                        pn = new Pen(Color.Red, 5);  // Перо для рисования
                        g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
                    }
                    else
                    {
                        pn = new Pen(Color.Black, 5);  // Перо для рисования
                        g.DrawEllipse(pn, (float)p.X, (float)p.Y, 10, 10);
                    }
                }
            }

        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            if (stepTextBox.Text != "")
                step = Convert.ToInt32(stepTextBox.Text.ToString());
            g.Clear(Color.White);
            foreach (MyPoint p in points)
            {
                if (p.Index == (redPoint + 1))
                {
                    r = Math.Sqrt(((p.X - points[redPoint].X) * (p.X - points[redPoint].X)) + ((p.Y - points[redPoint].Y) * (p.Y - points[redPoint].Y)));
                    angle = Convert.ToInt32(Math.Round((Math.Atan2((p.Y - points[p.Index - 1].Y), (p.X - points[p.Index - 1].X))) * 180 / Math.PI));

                    p.X = Math.Round(points[redPoint].X + r * Math.Cos((angle + step) * Math.PI / 180));
                    p.Y = Math.Round(points[redPoint].Y + r * Math.Sin((angle + step) * Math.PI / 180));

                }
                if (p.Index > (redPoint + 1))
                {
                    angle = Convert.ToInt32(Math.Round((Math.Atan2((p.Y - points[redPoint].Y), (p.X - points[redPoint].X))) * 180 / Math.PI));
                    r = Math.Sqrt(((p.X - points[redPoint].X) * (p.X - points[redPoint].X)) + ((p.Y - points[redPoint].Y) * (p.Y - points[redPoint].Y)));

                    textBox1.Text = (angle + step).ToString();
                    p.X = Math.Round(points[redPoint].X + r * Math.Cos((angle + step) * Math.PI / 180));
                    p.Y = Math.Round(points[redPoint].Y + r * Math.Sin((angle + step) * Math.PI / 180));
                }
            }
            Draw2(points);
            comboBox1.Items.Clear();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) //реакция на нажатия стрелок на клавиатуре
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                if (e.KeyCode == Keys.Up) upBtn.PerformClick();
                if (e.KeyCode == Keys.Down) downBtn.PerformClick();
                this.label1.Focus();
            }
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) //реакция на нажатия стрелок на клавиатуре
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                if (e.KeyCode == Keys.Up) upBtn.PerformClick();
                if (e.KeyCode == Keys.Down) downBtn.PerformClick();
                this.label1.Focus();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e) //если введено некорректное число и пытаемся выбрать какой-то узел
        {
            comboBox1.Items.Clear();
            int numOfPoints = Convert.ToInt32(pointsCount.Text);
            if ((numOfPoints <= 1) || (numOfPoints > 9))
            {
                MessageBox.Show("Введите число от 1 до 9");
            }
            else
            {
                for (int i = 0; i < numOfPoints; i++)
                {
                    if (i >= 0)
                        comboBox1.Items.Add(i + 1);
                }
            }
        }

        private void stepTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pointsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
