using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsFormsApp2
{
    /// 写入注册表
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tovalue"></param>
    
    public enum KeyModifiers //组合键枚举
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }

    public partial class Form1 : Form
    {
        /*
         * RegisterHotKey函数原型及说明：
         * BOOL RegisterHotKey(
         * HWND hWnd,         // window to receive hot-key notification
         * int id,            // identifier of hot key
         * UINT fsModifiers, // key-modifier flags
         * UINT vk            // virtual-key code);
         * 参数 id为你自己定义的一个ID值
         * 对一个线程来讲其值必需在0x0000 - 0xBFFF范围之内,十进制为0~49151
         * 对DLL来讲其值必需在0xC000 - 0xFFFF 范围之内,十进制为49152~65535
         * 在同一进程内该值必须唯一参数 fsModifiers指明与热键联合使用按键
         * 可取值为：MOD_ALT MOD_CONTROL MOD_WIN MOD_SHIFT参数，或数字0为无，1为Alt,2为Control，4为Shift，8为Windows
         * vk指明热键的虚拟键码
         */

        [System.Runtime.InteropServices.DllImport("user32.dll")] //申明API函数
        public static extern bool RegisterHotKey(
         IntPtr hWnd, // handle to window
         int id, // hot key identifier
         uint fsModifiers, // key-modifier options
         Keys vk // virtual-key code
        );
        [System.Runtime.InteropServices.DllImport("user32.dll")] //申明API函数
        public static extern bool UnregisterHotKey(
         IntPtr hWnd, // handle to window
         int id // hot key identifier
        );
        

        public Form1()
        {
            InitializeComponent();
        }
        int timeData = 0;//时间数据变量，以秒为单位
        int timeData1 = 0;
        int timeData2 = 0;
        int timeData3 = 0;
        int timeData4 = 0;
        int timeData5 = 0;

        bool btFlag = true;//计时器标签
        bool btFlag1 = true;
        bool btFlag2 = true;
        bool btFlag3 = true;
        bool btFlag4 = true;
        bool btFlag5 = true;

        //bool buFlag6 = true;
        int buttonlag = 1;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //Handle为当前窗口的句柄,继续自Control.Handle,Control为定义控件的基类
            //RegisterHotKey(Handle, 100, 0, Keys.A); //注册快捷键,热键为A
            //RegisterHotKey(Handle, 100, KeyModifiers.Alt | KeyModifiers.Control, Keys.B);//这时热键为Alt+CTRL+B
            //RegisterHotKey(Handle, 100, 1, Keys.B); //1为Alt键，热键为Alt+B
            textBox2.Text = "快捷键";
            textBox3.Text = "顺序启动 CTRL+空格键";
            textBox4.Text = "顺序复位 CTRL+B";
            textBox5.Text = "启动勾选项 小键盘“0”键";
            textBox6.Text = "顺序启动复位完需点下复位到第一个计时，不然顺序不对";
            //RegisterHotKey(Handle, 100, 2, Keys.Space);
            //RegisterHotKey(Handle, 200, 2, Keys.B);//册2个热键,根据id值100,200来判断需要执行哪个函数
            //RegisterHotKey(Handle, 300, 0, Keys.NumPad0);
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 400;
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 200;
            button1.Text = "开始";
            button3.Text = "开始";
            button4.Text = "开始";
            button5.Text = "开始";
            button6.Text = "开始";
            button7.Text = "开始";

            button2.Text = "复位";
            button8.Text = "复位";
            button9.Text = "复位";
            button10.Text = "复位";
            button11.Text = "复位";
            button12.Text = "复位";

            button13.Text = "启动勾选项";
            button14.Text = "复位勾选项";

            label7.Text = DateTime.Now.ToLongTimeString().ToString();
            btFlag = true;
            label1.Text = null;
            label2.Text = null;
            label3.Text = null;
            label4.Text = null;
            label5.Text = null;
            label6.Text = null;

            checkBox1.Text = null;
            checkBox2.Text = null;
            checkBox3.Text = null;
            checkBox4.Text = null;
            checkBox5.Text = null;
            checkBox6.Text = null;

            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);//添加事件
            textBox8.TextChanged += new EventHandler(textBox8_TextChanged);//添加事件
            textBox9.TextChanged += new EventHandler(textBox9_TextChanged);//添加事件
            textBox10.TextChanged += new EventHandler(textBox10_TextChanged);//添加事件
            textBox11.TextChanged += new EventHandler(textBox11_TextChanged);//添加事件
            textBox12.TextChanged += new EventHandler(textBox12_TextChanged);//添加事件
        }
        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_HOTKEY = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    ProcessHotkey(m);//按下热键时调用ProcessHotkey()函数
                    break;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }
        //****第1个开始按钮****//
        private void button1_Click(object sender, EventArgs e)
        {
      
            //UnregisterHotKey(Handle, 100);//卸载快捷键
          //RegisterHotKey(Handle, 100, 2, Keys.C); //注册新的快捷键，参数0表示无组合键
            if (btFlag)//如果是启动计时器标签为true
            {
                if (timeData >= 0)
                {
                    ShowTimeLabel(ref timeData);//显示具体是时间数
                    timer1.Start();//启动计时器
                    button1.Text = "停止";
                    btFlag = false;
                   
                }
            }
            else
            {
                timer1.Stop();//关闭计时器
                button1.Text = "开始";
                btFlag = true;
            }

        }
        int bs = 0;
        //****第1个ShowTimeLabel****//
        private void ShowTimeLabel(ref int timeData)
        {
            string hourStr = (timeData / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData % 3600 >= 60)
            {
                minuteStr = ((timeData % 3600) / 60).ToString();
                secondStr = ((timeData % 3600) % 60).ToString();
            }
            else
            {
                secondStr = timeData.ToString();
            }
            label1.Text = minuteStr + ":" + secondStr;//hourStr + ":" + minuteStr + ":" + secondStr;
            
        }

        //****第1个输入框****//
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            timer1.Stop();
            label1.Text = string.Empty;
            try
            {
                if (textBox1.Text != string.Empty)
                    timeData = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox1.Text = string.Empty;
            }
            button1.Text = "开始";
            btFlag = true;
        }
        //****第1个复位****//
        private void button2_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag = false;
            button1.PerformClick();
            label1.Text = null;
            timer1.Stop();//关闭计时器
            if (textBox1.Text != string.Empty)
            {
                timeData = Convert.ToInt32(textBox1.Text);
            }


        }
        //****第1个timer****//
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeData--;
            if (timeData >= 0)
            {
                ShowTimeLabel(ref timeData);
            }

            if (timeData == 0)
            {
                timeData = Convert.ToInt32(textBox1.Text);
            }
        }
        //****第2个timer(用于显示当前时间)****//
        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToLongTimeString().ToString();
        }


        //****第2个输入框****//
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            timer3.Stop();
            label2.Text = string.Empty;
            try
            {
                if (textBox8.Text != string.Empty)
                    timeData1 = Convert.ToInt32(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox8.Text = string.Empty;
            }
            button3.Text = "开始";
            btFlag1 = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void label7_Click(object sender, EventArgs e)
        {
            label7.Dock = DockStyle.Fill;
            
        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            timer4.Stop();
            label3.Text = string.Empty;
            try
            {
                if (textBox9.Text != string.Empty)
                    timeData2 = Convert.ToInt32(textBox9.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox9.Text = string.Empty;
            }
            button4.Text = "开始";
            btFlag2 = true;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            timer5.Stop();
            label4.Text = string.Empty;
            try
            {
                if (textBox10.Text != string.Empty)
                    timeData3 = Convert.ToInt32(textBox10.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox10.Text = string.Empty;
            }
            button5.Text = "开始";
            btFlag3 = true;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            timer6.Stop();
            label5.Text = string.Empty;
            try
            {
                if (textBox11.Text != string.Empty)
                    timeData4 = Convert.ToInt32(textBox11.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox11.Text = string.Empty;
            }
            button6.Text = "开始";
            btFlag4 = true;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            timer7.Stop();
            label6.Text = string.Empty;
            try
            {
                if (textBox12.Text != string.Empty)
                    timeData5 = Convert.ToInt32(textBox12.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法数据", "温馨提示");
                textBox12.Text = string.Empty;
            }
            button7.Text = "开始";
            btFlag5 = true;
        }


        //******第2个开始按钮*****///
        private void button3_Click(object sender, EventArgs e)
        {
            if (btFlag1)//如果是启动计时器标签为true
            {
                if (timeData1 >= 0)
                {
                    ShowTimeLabel1(ref timeData1);//显示具体是时间数
                    timer3.Start();//启动计时器
                    button3.Text = "停止";
                    btFlag1 = false;

                }
            }
            else
            {
                timer3.Stop();//关闭计时器
                button3.Text = "开始";
                btFlag1 = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (btFlag2)//如果是启动计时器标签为true
            {
                if (timeData2 >= 0)
                {
                    ShowTimeLabel2(ref timeData2);//显示具体是时间数
                    timer4.Start();//启动计时器
                    button4.Text = "停止";
                    btFlag2 = false;
                    //label7.Text  = 

                }
            }
            else
            {
                timer4.Stop();//关闭计时器
                button4.Text = "开始";
                btFlag2 = true;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (btFlag3)//如果是启动计时器标签为true
            {
                if (timeData3 >= 0)
                {
                    ShowTimeLabel3(ref timeData3);//显示具体是时间数
                    timer5.Start();//启动计时器
                    button5.Text = "停止";
                    btFlag3 = false;

                }
            }
            else
            {
                timer5.Stop();//关闭计时器
                button5.Text = "开始";
                btFlag3 = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (btFlag4)//如果是启动计时器标签为true
            {
                if (timeData4 >= 0)
                {
                    ShowTimeLabel4(ref timeData4);//显示具体是时间数
                    timer6.Start();//启动计时器
                    button6.Text = "停止";
                    btFlag4 = false;

                }
            }
            else
            {
                timer6.Stop();//关闭计时器
                button6.Text = "开始";
                btFlag4 = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (btFlag5)//如果是启动计时器标签为true
            {
                if (timeData5 >= 0)
                {
                    ShowTimeLabel5(ref timeData5);//显示具体是时间数
                    timer7.Start();//启动计时器
                    button7.Text = "停止";
                    btFlag5 = false;

                }
            }
            else
            {
                timer7.Stop();//关闭计时器
                button7.Text = "开始";
                btFlag5 = true;
            }
        }


        //****第2个复位**//
        private void button8_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag1 = false;
            button3.PerformClick();
            label2.Text = null;
            timer3.Stop();//关闭计时器
            if (textBox8.Text != string.Empty)
            {
                timeData1 = Convert.ToInt32(textBox8.Text);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag2 = false;
            button4.PerformClick();
            label3.Text = null;
            timer4.Stop();//关闭计时器
            if (textBox9.Text != string.Empty)
            {
                timeData2 = Convert.ToInt32(textBox9.Text);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag3 = false;
            button5.PerformClick();
            label4.Text = null;
            timer5.Stop();//关闭计时器
            if (textBox10.Text != string.Empty)
            {
                timeData3 = Convert.ToInt32(textBox10.Text);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag4 = false;
            button6.PerformClick();
            label5.Text = null;
            timer6.Stop();//关闭计时器
            if (textBox11.Text != string.Empty)
            {
                timeData4 = Convert.ToInt32(textBox11.Text);
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
            btFlag5 = false;
            button7.PerformClick();
            label6.Text = null;
            timer7.Stop();//关闭计时器
            if (textBox12.Text != string.Empty)
            {
                timeData5 = Convert.ToInt32(textBox12.Text);
            }

        }

        bool a = true;
        //****启动勾选项*****//
        private void button13_Click(object sender, EventArgs e)
        {

            
            a = !a;
            if (a)
            {
                button13.Text = "启动勾选项";
                btFlag = false;
                button1.Enabled = true;

                if (checkBox1.Checked)//如果是启动计时器标签为true
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button1.PerformClick();


                }
                if (checkBox2.Checked)//如果是启动计时器标签为true
                {
                    button3.Enabled = true;
                    button8.Enabled = true;
                    button3.PerformClick();

                }
                if (checkBox3.Checked)//如果是启动计时器标签为true
                {
                    button4.Enabled = true;
                    button9.Enabled = true;
                    button4.PerformClick();

                }
                if (checkBox4.Checked)//如果是启动计时器标签为true
                {
                    button5.Enabled = true;
                    button10.Enabled = true;
                    button5.PerformClick();

                }
                if (checkBox5.Checked)//如果是启动计时器标签为true
                {
                    button6.Enabled = true;
                    button11.Enabled = true;
                    button6.PerformClick();

                }
                if (checkBox6.Checked)//如果是启动计时器标签为true
                {
                    button7.Enabled = true;
                    button12.Enabled = true;
                    button7.PerformClick();

                }


            }
            else
            {
                if (checkBox1.Checked)//如果是启动计时器标签为true
                {
                    button1.PerformClick();
                    button1.Enabled = false;
                    button2.Enabled = false;

                }
                if (checkBox2.Checked)//如果是启动计时器标签为true
                {
                    button3.PerformClick();
                    button3.Enabled = false;
                    button8.Enabled = false;
                }
                if (checkBox3.Checked)//如果是启动计时器标签为true
                {
                    button4.PerformClick();
                    button4.Enabled = false;
                    button9.Enabled = false;
                }
                if (checkBox4.Checked)//如果是启动计时器标签为true
                {
                    button5.PerformClick();
                    button5.Enabled = false;
                    button10.Enabled = false;
                }
                if (checkBox5.Checked)//如果是启动计时器标签为true
                {
                    button6.PerformClick();
                    button6.Enabled = false;
                    button11.Enabled = false;
                }
                if (checkBox6.Checked)//如果是启动计时器标签为true
                {
                    button7.PerformClick();
                    button7.Enabled = false;
                    button12.Enabled = false;
                }
                button13.Text = "停止勾选项";


            }
            
            

        }

        private void button14_Click(object sender, EventArgs e)
        {
            bs = 0;
            buttonlag = 1;
            a = false;
            button13.PerformClick();

            if (checkBox1.Checked)
            {

                button2.PerformClick();

            }
            if (checkBox2.Checked)
            {

                button8.PerformClick();

            }
            if (checkBox3.Checked)
            {

                button9.PerformClick();

            }
            if (checkBox4.Checked)
            {

                button10.PerformClick();

            }
            if (checkBox5.Checked)
            {

                button11.PerformClick();

            }

            if (checkBox6.Checked)
            {

                button12.PerformClick();

            }

        }

        //****第2个计时器**///
        private void timer3_Tick(object sender, EventArgs e)
        {
            timeData1--;
            if (timeData1 >= 0)
            {
                ShowTimeLabel1(ref timeData1);
            }

            if (timeData1 == 0)
            {
                timeData1 = Convert.ToInt32(textBox8.Text);
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timeData2--;
            if (timeData2 >= 0)
            {
                ShowTimeLabel2(ref timeData2);
            }

            if (timeData2 == 0)
            {
                timeData2 = Convert.ToInt32(textBox9.Text);
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            timeData3--;
            if (timeData3 >= 0)
            {
                ShowTimeLabel3(ref timeData3);
            }

            if (timeData3 == 0)
            {
                timeData3 = Convert.ToInt32(textBox10.Text);
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            timeData4--;
            if (timeData4 >= 0)
            {
                ShowTimeLabel4(ref timeData4);
            }

            if (timeData4 == 0)
            {
                timeData4 = Convert.ToInt32(textBox11.Text);
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            timeData5--;
            if (timeData5 >= 0)
            {
                ShowTimeLabel5(ref timeData5);
            }

            if (timeData5 == 0)
            {
                timeData5 = Convert.ToInt32(textBox12.Text);
            }
        }
        //****第2个ShowTimeLabel****//
        private void ShowTimeLabel1(ref int timeData1)
        {
            string hourStr = (timeData1 / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData1 % 3600 >= 60)
            {
                minuteStr = ((timeData1 % 3600) / 60).ToString();
                secondStr = ((timeData1 % 3600) % 60).ToString();
            }
            //if (timeData1 < 10)
            //{
            //    Form2 f2 = new Form2(label1.Text.ToString());
            //    AddOwnedForm(f2);
            //    label1.Text = secondStr;
            //    f2.Show();
            //}
            else
            {
                secondStr = timeData1.ToString();
            }
            label2.Text = minuteStr + ":" + secondStr;// hourStr + ":" +

        }
        //****第3个ShowTimeLabel****//
        
        private void ShowTimeLabel2(ref int timeData2)
        {
            string hourStr = (timeData2 / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData2 % 3600 >= 60)
            {
                minuteStr = ((timeData2 % 3600) / 60).ToString();
                secondStr = ((timeData2 % 3600) % 60).ToString();
            }
            else
            {
                secondStr = timeData2.ToString();
            }
            if (label1.Text == "0:0")
            {
                bs++;

            }
            if (label1.Text == "0:0" && (label2.Text == "0:0" | label2.Text == "0:1") && (label3.Text == "0:0"| label3.Text == "0:1"))
            {
                bs = 0;
            }
            label8.Text = bs.ToString();
            label3.Text = minuteStr + ":" + secondStr;//hourStr + ":" + 
            
        }
        //****第4个ShowTimeLabel****//
        private void ShowTimeLabel3(ref int timeData3)
        {
            string hourStr = (timeData3 / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData3 % 3600 >= 60)
            {
                minuteStr = ((timeData3 % 3600) / 60).ToString();
                secondStr = ((timeData3 % 3600) % 60).ToString();
            }
            else
            {
                secondStr = timeData3.ToString();
            }
            label4.Text = minuteStr + ":" + secondStr;//hourStr + ":" + 
        }
        //****第5个ShowTimeLabel****//
        private void ShowTimeLabel4(ref int timeData4)
        {
            string hourStr = (timeData4 / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData4 % 3600 >= 60)
            {
                minuteStr = ((timeData4 % 3600) / 60).ToString();
                secondStr = ((timeData4 % 3600) % 60).ToString();
            }
            else
            {
                secondStr = timeData4.ToString();
            }
            label5.Text = minuteStr + ":" + secondStr;//hourStr + ":" + 
        }
        //****第5个ShowTimeLabel****//
        private void ShowTimeLabel5(ref int timeData5)
        {
            string hourStr = (timeData5 / 3600).ToString();
            string minuteStr = "0";
            string secondStr = "0";
            if (timeData5 % 3600 >= 60)
            {
                minuteStr = ((timeData5 % 3600) / 60).ToString();
                secondStr = ((timeData5 % 3600) % 60).ToString();
            }
            else
            {
                secondStr = timeData5.ToString();
            }
            label6.Text =  minuteStr + ":" + secondStr;//hourStr + ":" +
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button21.Text = "鼓神";
            button22.Text = "古蛇";
            button23.Text = "圣金甲";
            button24.Text = "怒目";
            button25.Text = "";
            button26.Text = "";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button21.Text = "神武罗";
            button22.Text = "猴子";
            button23.Text = "狗";
            button24.Text = "十方";
            button25.Text = "罗刹";
            button26.Text = "";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button21.Text = "小铁";
            button22.Text = "子纯";
            button23.Text = "仓力";
            button24.Text = "天地";
            button25.Text = "堕落";
            button26.Text = "幽冥";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button21.Text = "乌龟";
            button22.Text = "冰魅";
            button23.Text = "小怒";
            button24.Text = "站秋";
            button25.Text = "";
            button26.Text = "";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            button21.Text = "神使";
            button22.Text = "洪荒";
            button23.Text = "盘古";
            button24.Text = "";
            button25.Text = "";
            button26.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button21.Text = "狗";
            button22.Text = "光辉";
            button23.Text = "十方";
            button24.Text = "";
            button25.Text = "";
            button26.Text = "";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button21.Text == "鼓神")
            {
                textBox2.Text = "晕（开打计）";
                textBox3.Text = "全屏攻击（开打计）";
                textBox4.Text = "WX身上有减唱可立马T封印";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "20";
                textBox8.Text = "600";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "神武罗")
            {
                textBox2.Text = "固伤4000（开打计）";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "30";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false ;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "小铁")
            {
                textBox2.Text = "破甲群 无利齿320W计  真256W 假272W";
                textBox3.Text = "利刃   无利齿200W计  真160W 假170W";
                textBox4.Text = "大群   无利齿120W计  真90W 假102W";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "20";
                textBox8.Text = "30";
                textBox9.Text = "20";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "乌龟")
            {
                textBox2.Text = "基础吟唱 开打计";
                textBox3.Text = "乱跑   无利齿133W  真106.4W 假111.72W";
                textBox4.Text = "坚甲   无利齿79.8W  真63.84W 假67W";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "12";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true ;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "神使")
            {
                textBox2.Text = "封印 开打计";
                textBox3.Text = "群攻 开打计";
                textBox4.Text = "高伤群攻 开打计";
                textBox5.Text = "SS上去时先扑断群开场群攻";
                textBox6.Text = "一MM开蓝阵";
                textBox7.Text = "另一MM不能解SS其他随便";


                textBox1.Text = "15";
                textBox8.Text = "10";
                textBox9.Text = "20";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false  )//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true  ;
                }
                if (checkBox2.Checked == false  )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true  ;
                }
                if (checkBox3.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true ;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "狗")
            {
                textBox2.Text = "吸蓝 16";
                textBox3.Text = "木系群攻 ";
                textBox4.Text = "单体攻击";
                textBox5.Text = "都站BOSS旁边防止BOSS走位";
                textBox6.Text = "站远了BOSS移位会有两个灭团群";
                textBox7.Text = "MM解自己吸蓝，其他不用解";


                textBox1.Text = "16";
                textBox8.Text = "14";
                textBox9.Text = "20";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true ;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true ;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button21.Text == "子纯")
            {
                textBox2.Text = "单体流血 开场计";
                textBox3.Text = "暴走 开场计";
                textBox4.Text = "灭团群 开场计";
                textBox5.Text = "技能重叠时，先流血再暴走后大群";
                textBox6.Text = "SS带物理首饰，吃防御符，震慑，乌龟";
                textBox7.Text = "MM解毒要快，不能解SS，分开站BOSS转身就解";


                textBox1.Text = "5";
                textBox8.Text = "15";
                textBox9.Text = "25";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true ;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true ;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }

        }

            private void button22_Click(object sender, EventArgs e)
            {
            if (button22.Text == "古蛇")
            {
                textBox2.Text = "群小毒（开打计）";
                textBox3.Text = "单大毒（开打计）";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "20";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "猴子")
            {
                textBox2.Text = "巨力迅捷（开打计）";
                textBox3.Text = "扇形大（开打计）";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "60";
                textBox8.Text = "45";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "子纯")
            {
                textBox2.Text = "单体封印 无利齿450W计  真360W 假382.5W";
                textBox3.Text = "群晕   无利齿300W计  真240W 假255W";
                textBox4.Text = "第一仇恨吸蓝流血，无利齿150W计 真120W 假127.5W";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "20";
                textBox8.Text = "20";
                textBox9.Text = "30";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "冰魅")
            {
                textBox2.Text = "单体封印 开打计";
                textBox3.Text = "群驱状态 开打计";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "63";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "洪荒")
            {
                textBox2.Text = "近身群晕毒（开打计）";
                textBox3.Text = "水毒（开打计）";
                textBox4.Text = "群木毒（开打计）";
                textBox5.Text = "二仇木毒 （开打计）";
                textBox6.Text = "群水毒之后如果不能马上解毒，再中近身晕的话一次会掉3W-4W，连续掉3次后衰减到1.5W，3次后继续衰减直到下次水毒";
                textBox7.Text = "关键解水毒,重叠顺序是近身晕，群水毒，群木，二仇恨";


                textBox1.Text = "11";
                textBox8.Text = "31";
                textBox9.Text = "19";
                textBox10.Text = "26";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true;
                }
                if (checkBox4.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = true;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "光辉")
            {
                textBox2.Text = "随机单体80%伤害（开打计）";
                textBox3.Text = "近身晕毒（开打计）";
                textBox4.Text = "灭团群 （开打计）";
                textBox5.Text = "技能重叠时先单体80%，再近身晕毒，最后大群";
                textBox6.Text = "爆小元或凌波躲晕T大群";
                textBox7.Text = "SS上去扑开场群,关键T大群";


                textBox1.Text = "10";
                textBox8.Text = "20";
                textBox9.Text = "30";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true;
                }
                if (checkBox4.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false ;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button22.Text == "仓力")
            {
                textBox2.Text = "喷火（开打计）";
                textBox3.Text = "狂暴（开打计）";
                textBox4.Text = "灭团群 （开打计）";
                textBox5.Text = "BOSS3次无敌，分别再75%/50%/25%持续25秒";
                textBox6.Text = "645.69W/430.46W/215.23W,可爆元、乌龟躲大群";
                textBox7.Text = "技能重叠时先喷火狂暴再大群";


                textBox1.Text = "5";
                textBox8.Text = "15";
                textBox9.Text = "25";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
        }

            private void button23_Click(object sender, EventArgs e)
            {
            if (button23.Text == "圣金甲")
            {
                textBox2.Text = "乱仇恨（开打计）";
                textBox3.Text = "6000固伤（开打计）";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "60";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "狗")
            {
                textBox2.Text = "单体吸蓝（开打计）";
                textBox3.Text = "每掉25%血群晕，可尝试踢掉";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "25";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false ;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "仓力")
            {
                textBox2.Text = "群流血 无利齿450W计  真360W 假382.5W";
                textBox3.Text = "群晕   无利齿250W计  真200W 假212.5W";
                textBox4.Text = "大群 无利齿150W计 真120W 假127.5W";
                textBox5.Text = "大群 无利齿125W计 真100W 假106.25W";
                textBox6.Text = "大群150W开始，125W会刷新大群时间，清仇恨";
                textBox7.Text = "最好是160W等晕后20秒内直接龙打到125W以下";


                textBox1.Text = "30";
                textBox8.Text = "20";
                textBox9.Text = "20";
                textBox10.Text = "20";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "小怒")
            {
                textBox2.Text = "80%/60%/40%/20% 322W/241.8W/161W/80W各出一个护法";
                textBox3.Text = "巨力 322W";
                textBox4.Text = "迅捷 241.8W";
                textBox5.Text = "晕   241.8W计";
                textBox6.Text = "龙飞击+诅咒  241.8W计";
                textBox7.Text = "";


                textBox1.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "21";
                textBox11.Text = "27";
                textBox12.Text = "";


                if (checkBox1.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false ;
                }
                if (checkBox2.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false ;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox4.Checked = true ;
                }
                if (checkBox5.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox5.Checked = true ;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "盘古")
            {
                textBox2.Text = "龙飞机（开打计）";
                textBox3.Text = "灭团群（开打计）";
                textBox4.Text = "加状态（开打计）龙飞机后先加状态在大群";
                textBox5.Text = "四次龙飞机后一次大群";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "5";
                textBox8.Text = "25";
                textBox9.Text = "75";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true;
                }
                if (checkBox4.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false ;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "十方")
            {
                textBox2.Text = "扇形群大（开打计）";
                textBox3.Text = "单体流血（开打计）";
                textBox4.Text = "除SS所有人站BOSS后面";
                textBox5.Text = "开场扑群，这个不能算进次数";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "120";
                textBox8.Text = "15";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false ;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button23.Text == "天地")
            {
                textBox2.Text = "雷击（概率晕）开场计";
                textBox3.Text = "封印 开场计";
                textBox4.Text = "灭团大群 开场计";
                textBox5.Text = "无敌  开场计";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "5";
                textBox8.Text = "15";
                textBox9.Text = "25";
                textBox10.Text = "60";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true ;
                }
                if (checkBox4.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox4.Checked = true ;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
        }

            private void button24_Click(object sender, EventArgs e)
            {
            if (button24.Text == "怒目")
            {
                textBox2.Text = "扇形攻击（开打计）";
                textBox3.Text = "近战群晕（开打计）";
                textBox4.Text = "狂暴  50%血计时";
                textBox5.Text = "开启需要";
                textBox6.Text = "10个古旧残剑";
                textBox7.Text = "10个迷之骨头";


                textBox1.Text = "15";
                textBox8.Text = "35";
                textBox9.Text = "30";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button24.Text == "十方")
            {
                textBox2.Text = "群减血（开打计）";
                textBox3.Text = "流血 无利齿250W 真200W 假212.5W";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "30";
                textBox8.Text = "45";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button24.Text == "天地")
            {
                textBox2.Text = "群驱状态 无利齿540W计  真432W 假459W";
                textBox3.Text = "群吸元  无利齿420W计  真336W 假357W";
                textBox4.Text = "群木毒  无利齿300W计 真240W 假255W";
                textBox5.Text = "群晕   无利齿180W计 真144W 假153W";
                textBox6.Text = "大群   无利齿150W计 真120W 假127.5W";
                textBox7.Text = "";


                textBox1.Text = "60";
                textBox8.Text = "45";
                textBox9.Text = "35";
                textBox10.Text = "30";
                textBox11.Text = "55";
                textBox12.Text = "";


                if (checkBox1.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button24.Text == "站秋")
            {
                textBox2.Text = "群驱状态 338.6W";
                textBox3.Text = "群毒 338.6W计 ";
                textBox4.Text = "群晕 282.2W计 50%";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "";
                textBox8.Text = "17";
                textBox9.Text = "30";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false ;
                }
                if (checkBox5.Checked == true )//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false ;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            
        }

            private void button25_Click(object sender, EventArgs e)
            {
            if (button25.Text == "堕落")
            {
                textBox2.Text = "雷+减血概率晕  开打计";
                textBox3.Text = "狂暴 开打计";
                textBox4.Text = "大群 开打计";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "25";
                textBox9.Text = "35";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true ;
                }
                if (checkBox2.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true ;
                }
                if (checkBox3.Checked == false )//如果是启动计时器标签为true
                {
                    checkBox3.Checked = true ;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
            if (button25.Text == "罗刹")
            {
                textBox2.Text = "金系群攻（开打计）";
                textBox3.Text = "单体落雷（开打计）";
                textBox4.Text = "单体封印  50%血计时";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "35";
                textBox9.Text = "45";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }

        }

            private void button26_Click(object sender, EventArgs e)
            {
            if (button26.Text == "幽冥")
            {
                textBox2.Text = "小范围物理群（开打计）";
                textBox3.Text = "扔炸弹（开打计）";
                textBox4.Text = "凤凰展翅  50%血计时";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


                textBox1.Text = "15";
                textBox8.Text = "25";
                textBox9.Text = "55";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";


                if (checkBox1.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox1.Checked = true;
                }
                if (checkBox2.Checked == false)//如果是启动计时器标签为true
                {
                    checkBox2.Checked = true;
                }
                if (checkBox3.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox5.Checked = false;
                }
                if (checkBox6.Checked == true)//如果是启动计时器标签为true
                {
                    checkBox6.Checked = false;
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button21.Text = "子纯";
            button22.Text = "仓力";
            button23.Text = "天地";
            button24.Text = "";
            button25.Text = "";
            button26.Text = "";
        }
        private void From1SiziChanged(object sender, EventArgs e)
        {
            button1.Font = new Font(button1.Font.FontFamily,button1.Height*0.4F);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void ProcessHotkey(Message m) //按下设定的键时调用该函数
        {
            IntPtr id = m.WParam; //IntPtr用于表示指针或句柄的平台特定类型
            //MessageBox.Show(id.ToString()); 
            string sid = id.ToString();
            switch (sid)
            {
                case "100":

                    
                    if (buttonlag==1)
                    {
                        button1.PerformClick();
                        
                    }
                    if (buttonlag == 2)
                    {
                        button3.PerformClick();
                        
                    }
                    if (buttonlag == 3)
                    {
                        button4.PerformClick();
                        
                    }
                    if (buttonlag == 4)
                    {
                        button5.PerformClick();
                        
                    }
                    if (buttonlag == 5)
                    {
                        button6.PerformClick();
                        
                    }
                    if (buttonlag == 6)
                    {
                        button7.PerformClick();
                        
                    }
                    buttonlag++;
                    if (buttonlag <=0)
                    {

                        buttonlag = 1;
                    }
                    if (buttonlag > 6)
                    {

                        buttonlag = 1;
                    }
                    
                    break;
                case "200":
                    switch (buttonlag )
                    {
                        case 1:
                            button2.PerformClick();
                            buttonlag=2;
                            break;
                        case 2:
                            button8.PerformClick();
                            buttonlag=3;
                            break;
                        case 3:
                            button9.PerformClick();
                            buttonlag=4;
                            break;
                        case 4:
                            button10.PerformClick();
                            buttonlag=5;
                            break;
                        case 5:
                            button11.PerformClick();
                            buttonlag=6;
                            break;
                        case 6:
                            button12.PerformClick();
                            buttonlag=1;
                            break;
                            
                    }
                    break;

                case "300":
                    {
                        button13.PerformClick();
                    }
                    break;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            buttonlag = 1;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
    } 
