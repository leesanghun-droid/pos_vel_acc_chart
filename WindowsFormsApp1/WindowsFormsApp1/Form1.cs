using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Series Chart1;
        Series Chart2;
        Series Chart3;

        public Form1()
        {
            InitializeComponent();



            Console.WriteLine("start~");
            chart3.Series.Clear();

            Chart1 = chart3.Series.Add("POS");
            Chart2 = chart3.Series.Add("VEL");
            Chart3 = chart3.Series.Add("ACC");

            Title title = new Title();
            title.Text = "pos";
            title.ForeColor = Color.Blue;
            title.Font = new Font("맑은고딕", 10, FontStyle.Bold);
            chart3.Titles.Add(title);

            Chart1.LegendText = "POS";
            Chart1.ChartType = SeriesChartType.Line;
            Chart1.Color = Color.Red;
            Chart1.BorderWidth = 1;

            Chart2.LegendText = "VEL";
            Chart2.ChartType = SeriesChartType.Line;
            Chart2.Color = Color.Green;
            Chart2.BorderWidth = 1;

            Chart3.LegendText = "ACC";
            Chart3.ChartType = SeriesChartType.Line;
            Chart3.Color = Color.Yellow;
            Chart3.BorderWidth = 1;

            chart3.ChartAreas[0].AxisY.Minimum = -7000;
            chart3.ChartAreas[0].AxisY.Maximum = +7000;

            chart3.ChartAreas[0].AxisY.LabelStyle.Interval = 1000;
            chart3.ChartAreas[0].AxisY.MajorGrid.Interval = 1000;



            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();

        }

        double delta_t = 0.001;
        double now_time = 0;
        double time_t = 0;
        double vel = 0;
        double ex_vel = 0;
        double pos = 0;
        double ex_pos = -0.0001;
        double acc = 0;

        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int total = Chart1.Points.Count();

            var pi = Math.PI;
            //double acc = Math.Sin(now_time*2*pi);
            //vel = vel + acc * delta_t;

            pos = 1.5 * Math.Sin(now_time * 2 * pi * 7);
            vel = (pos - ex_pos) * (1 / delta_t);
            acc = (vel - ex_vel) * (1 / delta_t);
            ex_pos = pos;
            ex_vel = vel;


            if (total < (1/ delta_t))
            {
                chart3.Invoke(new MethodInvoker(delegate
                {

                    Chart1.Points.AddXY(now_time, pos);
                    Chart2.Points.AddXY(now_time, vel);
                    Chart3.Points.AddXY(now_time, acc);
                    //Console.WriteLine(total);
                }));
                
            }

            now_time += delta_t;
        }
    }
}
