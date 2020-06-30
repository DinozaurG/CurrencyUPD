using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency
{
    public partial class Form1 : Form
    {
        const double k = 0.02;
        Random rnd = new Random();
        int i, day;
        Player player = new Player();
        double price, w = 0, u = 0.1, o = 0.1, tT = 0.01;
        List<double> pointsY = new List<double>();
        public Form1()
        {
            InitializeComponent();
            cash.Text = ("Cash: " + player.cash);
        }

        private void btCalc_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            price = (double)initialPrice.Value;
            day = (int)daysOfWish.Value;
            chart1.Series[0].Points.AddXY(0, price);
            for (i = 1; i <= day; i++)
            {
                w = w + Math.Sqrt(tT) * (rnd.NextDouble() % 1 - 0.5);
                price = price * Math.Exp((u - (o * o) / 2) * tT + o * w);
                pointsY.Add(price);
                chart1.Series[0].Points.AddXY(i, price);
            } 
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            w = w + Math.Sqrt(tT) * (rnd.NextDouble() % 1 - 0.5);
            price = price * Math.Exp((u - (o * o) / 2) * tT + o * w);
            pointsY.Add(price);
            chart1.Series[0].Points.AddXY(i, price);
            dayCount.Text = ("Day: " + (i - day));
            i++;
        }
        private void buyButton_Click(object sender, EventArgs e)
        {
            if (player.cash - pointsY[i - day] >= 0)
            {
                player.BuyPapers(price);
                cash.Text = ("Cash: " + player.cash);
            }
            else
                MessageBox.Show("Недостаточно золота");
        }
        private void sellButton_Click(object sender, EventArgs e)
        {
            if (player.cntPapers - 1 >= 0)
            {
                player.SellPapers(price);
                cash.Text = ("Cash: " + player.cash);
            }
            else
                MessageBox.Show("Нечего продавать");
        }
    }
}
