using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace lab5
{
    public partial class MiniPanit : Form
    {
        
        public MiniPanit()
        {
            InitializeComponent();
            bit = new Bitmap(picDraw.Width, picDraw.Height);
            g = Graphics.FromImage(bit);
            g.Clear(Color.White);
            picDraw.Image = bit;
        }

        Graphics g;
        bool StartPaint = false;
        bool Del = false;
        Bitmap bit;
        Point px, py;
        Pen p = new Pen(Color.Black, 5);
        private void MiniPanit_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
                cboLevel.Items.Add(i.ToString());
        }

        private void picDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (StartPaint)
            {
                if (!Del)
                {
                    px = e.Location;
                    Pen p = new Pen(btnColor.BackColor,float.Parse(cboLevel.Text));
                    g.DrawLine(p, px, py);
                    py = px;
                }
                else 
                {
                    px = e.Location;
                    Pen p = new Pen(Color.White, float.Parse(cboLevel.Text));
                    g.DrawLine(p, px, py);
                    py = px;
                }
            }

            
            picDraw.Refresh();
            
        }
        
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if(color.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = color.Color;
            }
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            StartPaint = true;
            Del = false;    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Del = true;
            StartPaint = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            g.Clear(picDraw.BackColor);
            StartPaint = false;
            Del = false;
        }

        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            StartPaint = false;
            
        }

        private void picDraw_MouseDown(object sender, MouseEventArgs e)
        {
            StartPaint = true;
            py = e.Location;
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm =  bit.Clone(new Rectangle(0,0,picDraw.Width,picDraw.Height),bit.PixelFormat);
                btm.Save(sfd.FileName,  ImageFormat.Jpeg);
                MessageBox.Show("Image saved!!!");
            }
        }

        private void picDraw_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

        }

        
    }
}
