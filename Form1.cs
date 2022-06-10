using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_3
{
    public partial class Form1 : Form
    {
        Semaphore full;
        Semaphore empty;
        Semaphore mutex;
        static Semaphore main = new Semaphore(1, 1);
        int full1;
        int empty1;
        int resource;
        int step;
        int max;

        bool[] processer_bool;
        bool[] consumer_bool;

        Thread[] threads;
        Thread[] threads_c;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void start_execute()
        {
            for (int i = 0; i < resource; i++) progressBar1.PerformStep();
            step = 1;
            threads = new Thread[4];
            threads_c = new Thread[4];
            
            for (int i = 0; i < 4; i++)
            {
                if (processer_bool[i])
                {
                    threads[i] = new Thread(new ParameterizedThreadStart(proceducer));
                    threads[i].Start(new MessageOfThread(this, i));
                }
                
            }

            for (int i = 0; i < 4; i++)
            {
                if (consumer_bool[i])
                {
                    threads_c[i] = new Thread(new ParameterizedThreadStart(consumer));
                    threads_c[i].Start(new MessageOfThread(this, i));
                }
                
            }
        }
        public void proceducer (object obj)
        {
            Form1 form = ((MessageOfThread)obj).form;
            int index = ((MessageOfThread)obj).index;

            label9.Text = resource.ToString();
            label8.Text = full1.ToString();
            label5.Text = empty1.ToString();
            
            while (true)
            {
                
                switch (index)
                {
                    case 0:
                        {
                            this.textBox2.Text = "等待empty信号量中...";
                            empty.WaitOne();// empty
                            empty1--;
                            this.label5.Text = empty1.ToString(); 
                            this.textBox2.Text = "已获得empty信号量，等待mutex中...";
                            mutex.WaitOne();// mutex
                            this.label3.Text = "Proceducer " + index;
                            this.textBox2.Text = "已获得mutex信号量，生产中...";
                            this.textBox2.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            resource++;
                            this.label9.Text = resource.ToString();
                            this.progressBar1.Step = this.step;
                            this.progressBar1.PerformStep();
                            this.textBox2.Text = "生产结束！";
                            this.textBox2.BackColor = Color.White;

                            this.label8.Text = (++full1).ToString();
                            mutex.Release();
                            full.Release();

                            Thread.Sleep(500);
                            
                            break;
                        }

                    case 1:
                        {
                            this.textBox3.Text = "等待empty信号量中...";
                            empty.WaitOne();
                            empty1--;
                            this.label5.Text = empty1.ToString();
                            this.textBox3.Text = "已获得empty信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Proceducer " + index;
                            this.textBox3.Text = "已获得mutex信号量，生产中...";
                            this.textBox3.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            resource++;
                            this.label9.Text = resource.ToString();
                            this.progressBar1.Step = this.step;
                            this.progressBar1.PerformStep();
                            this.textBox3.Text = "生产结束！";
                            this.textBox3.BackColor = Color.White;

                            this.label8.Text = (++full1).ToString();
                            mutex.Release();
                            full.Release();

                            Thread.Sleep(500);

                            break;
                        }
                    case 2:
                        {
                            this.textBox4.Text = "等待empty信号量中...";
                            empty.WaitOne();
                            empty1--;
                            this.label5.Text = empty1.ToString();
                            this.textBox4.Text = "已获得empty信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Proceducer " + index;
                            this.textBox4.Text = "已获得mutex信号量，生产中...";
                            this.textBox4.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            resource++;
                            this.label9.Text = resource.ToString();
                            this.progressBar1.Step = this.step;
                            this.progressBar1.PerformStep();
                            this.textBox4.Text = "生产结束！";
                            this.textBox4.BackColor = Color.White;

                            this.label8.Text = (++full1).ToString();
                            mutex.Release();
                            full.Release();

                            Thread.Sleep(500);

                            break;
                        }

                    case 3:
                        {
                            this.textBox5.Text = "等待empty信号量中...";
                            empty.WaitOne();
                            empty1--;
                            this.label5.Text = empty1.ToString();
                            this.textBox5.Text = "已获得empty信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Proceducer " + index;
                            this.textBox5.Text = "已获得mutex信号量，生产中...";
                            this.textBox5.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            resource++;
                            this.label9.Text = resource.ToString();
                            this.progressBar1.Step = this.step;
                            this.progressBar1.PerformStep();
                            this.textBox5.Text = "生产结束！";
                            this.textBox5.BackColor = Color.White;

                            this.label8.Text = (++full1).ToString();
                            mutex.Release();
                            full.Release();

                            Thread.Sleep(500);

                            break;
                        }
                }
                
            }

        }
        public void consumer (object obj)
        {
            Form1 form = ((MessageOfThread)obj).form;
            int index = ((MessageOfThread)obj).index;

            while (true)
            {

                switch (index)
                {
                    case 0:
                        {
                            this.textBox6.Text = "等待full信号量中...";
                            full.WaitOne();
                            full1--;
                            this.label8.Text = full1.ToString();
                            this.textBox6.Text = "已获得full信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Consumer " + index;
                            this.textBox6.Text = "已获得mutex信号量，消耗中...";
                            this.textBox6.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            this.progressBar1.Step = this.step * -1;
                            this.progressBar1.PerformStep();
                            resource--;
                            this.label9.Text = resource.ToString();
                            this.textBox6.Text = "消耗结束！";
                            this.textBox6.BackColor = Color.White;
                            this.label5.Text = (++empty1).ToString();
                            mutex.Release();
                            empty.Release();

                            Thread.Sleep(500);

                            break;
                        }

                    case 1:
                        {
                            this.textBox7.Text = "等待full信号量中...";
                            full.WaitOne();
                            full1--;
                            this.label8.Text = full1.ToString();
                            this.textBox7.Text = "已获得full信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Consumer " + index;
                            this.textBox7.Text = "已获得mutex信号量，消耗中...";
                            this.textBox7.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            this.progressBar1.Step = this.step * -1;
                            this.progressBar1.PerformStep();
                            resource--;
                            this.label9.Text = resource.ToString();
                            this.textBox7.Text = "消耗结束！";
                            this.textBox7.BackColor = Color.White;
                            this.label5.Text = (++empty1).ToString();
                            mutex.Release();
                            empty.Release();

                            Thread.Sleep(500);

                            break;
                        }

                    case 2:
                        {
                            this.textBox8.Text = "等待full信号量中...";
                            full.WaitOne();
                            full1--;
                            this.label8.Text = full1.ToString();
                            this.textBox8.Text = "已获得full信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Consumer " + index;
                            this.textBox8.Text = "已获得mutex信号量，消耗中...";
                            this.textBox8.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            this.progressBar1.Step = this.step * -1;
                            this.progressBar1.PerformStep();
                            resource--;
                            this.label9.Text = resource.ToString();
                            this.textBox8.Text = "消耗结束！";
                            this.textBox8.BackColor = Color.White;
                            this.label5.Text = (++empty1).ToString();
                            mutex.Release();
                            empty.Release();

                            Thread.Sleep(500);

                            break;
                        }

                    case 3:
                        {
                            this.textBox9.Text = "等待full信号量中...";
                            full.WaitOne();
                            full1--;
                            this.label8.Text = full1.ToString();
                            this.textBox9.Text = "已获得full信号量，等待mutex中...";
                            mutex.WaitOne();
                            this.label3.Text = "Consumer " + index;
                            this.textBox9.Text = "已获得mutex信号量，消耗中...";
                            this.textBox9.BackColor = Color.Yellow;
                            Thread.Sleep(2000);
                            this.progressBar1.Step = this.step * -1;
                            this.progressBar1.PerformStep();
                            resource--;
                            this.label9.Text = resource.ToString();
                            this.textBox9.Text = "消耗结束！";
                            this.textBox9.BackColor = Color.White;
                            this.label5.Text = (++empty1).ToString();
                            mutex.Release();
                            empty.Release();

                            Thread.Sleep(500);

                            break;
                        }

                }

            }
        }



        public static bool IsNumeral(string input)//shuz数字 numeral
        {
            if (input.Length == 0) return false;
            foreach (char ch in input)
            {
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(IsNumeral(textBox1.Text) && IsNumeral(textBox10.Text)))
            {
                MessageBox.Show("请输入正确的数字！");
                return;
            }

            this.max = int.Parse(textBox10.Text);
            progressBar1.Maximum = max;
            progressBar1.Step = 1;


            this.resource = int.Parse(textBox1.Text);

            if (max < resource)
            {
                MessageBox.Show("关系错误！");
                return;
            }


            main.WaitOne();
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            processer_bool = new bool[4];
            consumer_bool = new bool[4];

            textBox10.Enabled = false;
            textBox1.Enabled = false;

            progressBar1.Value = 0;

            processer_bool[0] = checkBox1.Checked;
            processer_bool[1] = checkBox2.Checked;
            processer_bool[2] = checkBox3.Checked;
            processer_bool[3] = checkBox4.Checked;
            consumer_bool[0] = checkBox5.Checked;
            consumer_bool[1] = checkBox6.Checked;
            consumer_bool[2] = checkBox7.Checked;
            consumer_bool[3] = checkBox8.Checked;

            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;


            textBox2.Text = "";
            textBox2.Enabled = checkBox1.Checked;
            textBox3.Text = "";
            textBox3.Enabled = checkBox2.Checked;
            textBox4.Text = "";
            textBox4.Enabled = checkBox3.Checked;
            textBox5.Text = "";
            textBox5.Enabled = checkBox4.Checked;
            textBox6.Text = "";
            textBox6.Enabled = checkBox5.Checked;
            textBox7.Text = "";
            textBox7.Enabled = checkBox6.Checked;
            textBox8.Text = "";
            textBox8.Enabled = checkBox7.Checked;
            textBox9.Text = "";
            textBox9.Enabled = checkBox8.Checked;

            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox8.BackColor = Color.White;
            textBox9.BackColor = Color.White;


            

            

                this.empty1 = max - resource;
            this.full1 = resource;

            this.full = new Semaphore(full1, max);
            this.empty = new Semaphore(empty1, max);
            this.mutex = new Semaphore(1, 1);

            main.Release();

            start_execute();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
            this.button2.Enabled = false;

            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;

            for (int i = 0; i < 4; i++)
            {
                if (processer_bool[i]) threads[i].Abort();
                if (consumer_bool[i]) threads_c[i].Abort();

            }

            textBox10.Enabled = true;
            textBox1.Enabled = true;

        }
    }

    class MessageOfThread
    {
        public Form1 form;
        public int index;
        public MessageOfThread(Form1 form, int index)
        {
            this.form = form;
            this.index = index;
        }
    }
}

