﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double val1 ;
        double val2 ;
        double val3 ;
        double val4 ;
        double val5 ;
        int n ;
        double countSum;
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            val1 = (double)Val1Ed1.Value;
            val2 = (double)Val1Ed2.Value;
            val3 = (double)Val1Ed3.Value;
            val4 = (double)Val1Ed4.Value;
            double s = val1 + val2 + val3 + val4;
            if (s > 1) 
            {
                MessageBox.Show("Ошибка, сумма вероятностей должна быть равна 1");
            }
            else
            {
                val5 = 1 - s;
            }
            Val1Ed5.Value = (decimal)val5;
            n = (int)numEd.Value;
            
            countSum = 0;

                //timer1.Start();

            double[] probabilities = { val1, val2, val3, val4, val5 };
            double[] genProg = new double[5];

            countSum = 0;
            Random rnd = new Random();
            double randNum;
            for (int i = 0; i < n; i++)
            {
                randNum = rnd.NextDouble();
                int event_id = 0;
                randNum -= probabilities[0];
                while (randNum > 0)
                {
                    event_id++;
                    randNum -= probabilities[event_id];
                };
                genProg[event_id]++;
            }
            double sred=0;
            for (int j = 0; j < 5; j++)
            {
                genProg[j] /= n;
                sred += genProg[j];
                chart1.Series[j].Points.AddXY(j,genProg[j]);
            }
            double Msred = sred / 5;
            double ds = 0;
            for (int i = 0; i < 5; i++)
            {
                ds += ((genProg[i] - Msred) * (genProg[i] - Msred))*genProg[i];
            }
            double[] sigma = new double[5]; 
            for (int i = 0; i < 5; i++)
            {
                sigma[i] = (genProg[i] - probabilities[i]) / probabilities[i] * 100;
            }
            double chi = 0;
            for (int i = 0; i < 5; i++)
            {
                double c;
                c = Math.Abs(genProg[i] - probabilities[i]);
                if (c == 0)
                {
                    c = 1;
                }
                chi = chi + (double)(c * c) / probabilities[i];
            }
           /* for (int i = 0; i < 5; i++)
            {
                chi += Math.Pow(genProg[i] - probabilities[i], 2) / probabilities[i];
            }*/
            EmpVer1.Text = Math.Round(genProg[0], 4).ToString();
            EmpVer2.Text = Math.Round(genProg[1], 4).ToString();
            EmpVer3.Text = Math.Round(genProg[2], 4).ToString();
            EmpVer4.Text = Math.Round(genProg[3], 4).ToString();
            EmpVer5.Text = Math.Round(genProg[4], 4).ToString();
            vibSredLbl.Text = Math.Round(Msred, 4).ToString();
            DLbl.Text = Math.Round(ds, 4).ToString();
            sigma1.Text = Math.Round(sigma[0], 4).ToString();
            sigma2.Text = Math.Round(sigma[1], 4).ToString();
            sigma3.Text = Math.Round(sigma[2], 4).ToString();
            sigma4.Text = Math.Round(sigma[3], 4).ToString();
            sigma5.Text = Math.Round(sigma[4], 4).ToString();


            if(chi>9.488)
            {
                chiLbl.Text = chi.ToString() + " > " + "9.488" + " true";
            }
            else
            {
                chiLbl.Text = chi.ToString() + " < " + "9.488" + " false";
            }
        }


    }
}
