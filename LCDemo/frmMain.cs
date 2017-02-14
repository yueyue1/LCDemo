using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using LC_SDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.IO;

namespace LCDemo
{
    public partial class frmMain : Form
    {
        JObject[] deviceJsonObj = new JObject[100];
        _dhlc_device_t[] deviceInfoList = new _dhlc_device_t[100];
        int objectIndex = 100;
        _dhlc_device_t nowDeviceInfo = new _dhlc_device_t();
        bool isPlaying = false;
        bool isPlayBacking = false;
        bool isRecordying = false;
        bool isTalking = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string deviceList = "";
            if (LCSDK.LCOpenSDK_GetdeviceList(Form1.token, "1-100", ref deviceList, 0))
            {
                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(deviceList);
                int deviceCount = jsonObj["devices"].Count();
                int channelTotalCount = 0;
                for (int i = 0; i < deviceCount; i++)
                {
                    deviceJsonObj[i] = (JObject)jsonObj["devices"][i];//
                    int channelCount = deviceJsonObj[i]["channels"].Count();
                    for (int j = 0; j < channelCount; j++)
                    {
                        string channelId = deviceJsonObj[i]["channels"][j]["channelId"].ToString();
                        deviceInfoList[channelTotalCount] = new _dhlc_device_t();
                        deviceInfoList[channelTotalCount].definitionMode = 0;
                        deviceInfoList[channelTotalCount].objectIndex = objectIndex;
                        deviceInfoList[channelTotalCount].channelIndex = int.Parse(channelId);
                        deviceInfoList[channelTotalCount].deviceID = deviceJsonObj[i]["deviceId"].ToString();
                        deviceInfoList[channelTotalCount].token =Form1.token;
                        deviceInfoList[channelTotalCount].playType = PlayCtrlType.PLAYCTRL_REALPLAY;
                        deviceInfoList[channelTotalCount].hWnd = (int)picVideo.Handle;

                        AddVideo(channelTotalCount, deviceJsonObj[i]["name"].ToString(), channelId
                            , deviceJsonObj[i]["channels"][j]["channelOnline"].ToString(), deviceJsonObj[i]["channels"][j]["channelPicUrl"].ToString());
                        channelTotalCount++;
                    }
                }
                nowDeviceInfo = deviceInfoList[0];
            }
            else
            {
                //labelPrompt.Text = "提示信息：" + deviceList;
                MessageBox.Show(deviceList);
            }

        }
        private void AddVideo(int index, string deviceName, string channelId, string status, string channelPicUrl)
        {
            this.listBox1.Items.Add(index + " 设备ID:" + deviceName + " 通道:" + channelId + " 在线:" + status + " ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isPlayBacking)
            {
                MessageBox.Show("请先停止当前回放");
                return;
            }
            if (!isPlaying)
            {
                if (nowDeviceInfo.token != "")
                {

                    if (LCSDK.LCOpenSDK_startStream(nowDeviceInfo) == 0)
                    {
                        isPlaying = true;
                        button2.Text = "停止播放";
                    }
                }
                else
                {
                    MessageBox.Show("请先获取设备列表");
                }
            }
            else
            {
                isPlaying = false;
                LCSDK.LCOpenSDK_stopStream(objectIndex);
                button2.Text = "开始播放";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!isRecordying)
            {
                isRecordying = true;
                string videoPath = "F:\\LCVideo\\LCVideo.wmv";
                LCSDK.LCOpenSDK_startRecord(objectIndex, System.Text.Encoding.Default.GetBytes(videoPath));
                button3.Text = "停止录像";
            }
            else
            {
                button3.Text = "开始录像";
                LCSDK.LCOpenSDK_stopRecord(objectIndex);
                isRecordying = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!isPlaying) return;
            if (!isTalking)
            {
                isTalking = true;
                LCSDK.LCOpenSDK_playTalk(objectIndex);
                button4.Text = "停止对讲";
            }
            else
            {
                LCSDK.LCOpenSDK_stopTalk(objectIndex);
                isTalking = false;
                button4.Text = "开始对讲";
            }
        }

        private void ptzCtrl_Click(object sender, EventArgs e)
        {
            if (!isPlaying) return;
            Label obj = (Label)sender;
            string strType = obj.Name.Replace("ptzCtrl", "");
            PTZCtrlType ptzCtrlType = (PTZCtrlType)int.Parse(strType);
            string errorMsg = "";
            if (!LCSDK.LCOpenSDK_PTZCtrl(nowDeviceInfo.token, nowDeviceInfo.deviceID, (int)nowDeviceInfo.channelIndex, ptzCtrlType, ref errorMsg))
            {
            }
            else
            {
                //labelPrompt.Text = "提示信息：" + errorMsg;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {           
            if (button6.Text == "录像回放")
            {
                if (!isPlaying)
                {
                    if (!File.Exists("F:\\LCVideo\\LCVideo.wmv"))
                        return;
                    PictureBox PlayScreen = new PictureBox();
                    PlayScreen = this.picVideo;
                    string mciCommand;
                    mciCommand = "open " + @"F:\LCVideo\LCVideo.wmv" + " alias MyAVI";
                    mciCommand = mciCommand + " parent " + PlayScreen.Handle.ToInt32() + " style child";
                    LibWrap.mciSendString(mciCommand, null, 0, 0);
                    Rectangle r = PlayScreen.ClientRectangle;
                    mciCommand = "put MyAVI window at 0 0 " + r.Width + " " + r.Height;
                    LibWrap.mciSendString(mciCommand, null, 0, 0);
                    LibWrap.mciSendString("play MyAVI", null, 0, 0);
                    isPlayBacking = true;

                    this.button6.Text = "停止回放";
                }
                else
                {
                    //停止实时播放
                    isPlaying = false;
                    LCSDK.LCOpenSDK_stopStream(objectIndex);
                    button2.Text = "开始播放";

                    if (!File.Exists("F:\\LCVideo\\LCVideo.wmv"))
                        return;
                    PictureBox PlayScreen = new PictureBox();
                    PlayScreen = this.picVideo;
                    string mciCommand;
                    mciCommand = "open " + @"F:\LCVideo\LCVideo.wmv" + " alias MyAVI";
                    mciCommand = mciCommand + " parent " + PlayScreen.Handle.ToInt32() + " style child";
                    LibWrap.mciSendString(mciCommand, null, 0, 0);
                    Rectangle r = PlayScreen.ClientRectangle;
                    mciCommand = "put MyAVI window at 0 0 " + r.Width + " " + r.Height;
                    LibWrap.mciSendString(mciCommand, null, 0, 0);
                    LibWrap.mciSendString("play MyAVI", null, 0, 0);

                    this.button6.Text = "停止回放";
                }
            }
            else
            {
                LibWrap.mciSendString("stop MyAVI", null, 0, 0);
                this.button6.Text = "录像回放";
                isPlayBacking = false;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string deviceList = "";
            if (LCSDK.LCOpenSDK_GetdeviceList(Form1.token, "1-100", ref deviceList, 1))
            {
                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(deviceList);
                int deviceCount = jsonObj["devices"].Count();
                int channelTotalCount = 0;
                this.listBox1.Items.Clear();
                for (int i = 0; i < deviceCount; i++)
                {
                    deviceJsonObj[i] = (JObject)jsonObj["devices"][i];//
                    int channelCount = deviceJsonObj[i]["channels"].Count();
                    for (int j = 0; j < channelCount; j++)
                    {
                        string channelId = deviceJsonObj[i]["channels"][j]["channelId"].ToString();
                        deviceInfoList[channelTotalCount] = new _dhlc_device_t();
                        deviceInfoList[channelTotalCount].definitionMode = 0;//分辨率模式 0-高清 1-标清
                        deviceInfoList[channelTotalCount].objectIndex = objectIndex;//视频播放类对象的标识ID
                        deviceInfoList[channelTotalCount].channelIndex = int.Parse(channelId);//播放设备通道号
                        deviceInfoList[channelTotalCount].deviceID = deviceJsonObj[i]["deviceId"].ToString();//设备ID
                        deviceInfoList[channelTotalCount].token = Form1.token;
                        deviceInfoList[channelTotalCount].playType = PlayCtrlType.PLAYCTRL_REALPLAY;//播放模式 实时
                        deviceInfoList[channelTotalCount].hWnd = (int)picVideo.Handle;//视频窗口句柄

                        AddVideo(channelTotalCount, deviceJsonObj[i]["name"].ToString(), channelId
                            , deviceJsonObj[i]["channels"][j]["channelOnline"].ToString(), deviceJsonObj[i]["channels"][j]["channelPicUrl"].ToString());
                        channelTotalCount++;
                        objectIndex--;
                    }
                }
                nowDeviceInfo = deviceInfoList[0];
                objectIndex = 100;
            }
            else
            {
                //labelPrompt.Text = "提示信息：" + deviceList;
                MessageBox.Show(deviceList);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isPlaying)
            {
                int index = this.listBox1.SelectedIndex;
                nowDeviceInfo = deviceInfoList[index];
                objectIndex = 100 - index;
            } 
        }
    }
}
