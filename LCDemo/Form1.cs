using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using LC_SDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LCDemo
{
    public partial class Form1 : Form
    {
        public static string token = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LCSDK.LCOpenSDK_SetSystem(textBox1.Text, textBox2.Text);
            //管理员账号模拟登录
            if (!LCSDK.LCOpenSDK_GetToken(textBox3.Text, comboBox1.SelectedIndex, ref token))
            {
                MessageBox.Show("得不到token");
                return;
            }
            //MessageBox.Show(token);
            OPEN_API_INIT_PARAM initParam;
            initParam.appId = "lc2e12496fc30a4a7b";
            initParam.appSecret = "a4ad8f9063ea47398982a6617c40c8";
            initParam.caPath = Application.StartupPath + "\\LCSDK\\222.pem";
            initParam.host = "openapi.lechange.cn";
            initParam.port = 443;
            LCSDK.LCOpenSDK_initOpenApi(initParam);
            frmMain obj = new frmMain();
            this.Visible = false;
            obj.ShowDialog();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 1;//初始化登录类型
        }
    }
}
