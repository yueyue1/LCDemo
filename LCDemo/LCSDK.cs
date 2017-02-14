using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LC_SDK
{
    //事件类型
    public enum PlayCtrlType
    {
        PLAYCTRL_REALPLAY,
        PLAYCTRL_CLOUDPLAY,
        PLAYCTRL_DEVICEPLAY_FILE,
        PLAYCTRL_DEVICEPLAY_TIME
    }

    //云台方向类型
    public enum PTZCtrlType
    {
        PTZ_CONTROL_UP,
        PTZ_CONTROL_LEFT,
        PTZ_CONTROL_DOWN,
        PTZ_CONTROL_RIGHT,
        PTZ_CONTROL_LEFTUP,
        PTZ_CONTROL_LEFTDOWN,
        PTZ_CONTROL_RIGHTUP,
        PTZ_CONTROL_RIGHTDOWN,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OPEN_API_INIT_PARAM
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string host;//
        public int port;//
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string caPath;//
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string appId;//设备ID        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string appSecret;//
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _dhlc_device_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public string deviceName;//设备名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string token;//
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string deviceID;//设备ID
        public int objectIndex;//视频播放类对象的标识ID
        public int channelIndex;//播放设备通道号
        public int definitionMode;//分辨率模式，0-高清，1-标清
        public int hWnd; //视频窗口句柄
        public PlayCtrlType playType;//播放模式，0-实时视频，1-云录像回放，2-设备录像回放，按文件，3-设备录像回放，按时间
        public Int64 recordID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string fileName;//录像文件,playMode=1 有效
        public int beginTime;
        public int endTime;
        
    }

    public class QeuryRecord
    {
        public string token="";//授权Token
        public string deviceId = "";//设备序列号
        public string channelId = "";//通道号
        public string beginTime = "";//开始时间
        public string endTime = "";//结束时间
        public string queryRange = "";//第几条到第几条,数字取值范围为：[1,N]（N为正整数，且后者＞前者）; 差值取值范围为：[0, 29]
        public string type = "";//类型,为空时表示查询设备云录像片段，非空表示查询设备本地录像片段："Manual"(手动录像),"Event"(事件录像),"All"(所有录像)
    }

    public class RecordInfo
    {
        public string recordId = "";//录像Id
        public string deviceId = "";//设备序列号
        public string channelId = "";//通道号
        public string beginTime = "";//开始时间
        public string endTime = "";//结束时间
        public string size = "";//云录像的大小（单位byte）
        public string thumbUrl = "";//缩略图Url
        public string encryptMode = "";//加密模式（0：默认加密模式；1：用户加密模式）
        public string type = "";//类型
    }

    public static class LCCallBack
    {        
        //【播放业务码的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        //参数-code				： 状态码,具体详见见乐橙SDK说明书
        //参数-resultSource     ： 业务类型,见乐橙SDK说明书
        public delegate void DelegatePlayerResult(int index, string code, int resultSource); 

        //【分辨率改变的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        //参数-width			： 宽
        //参数-height			： 高
        public delegate void DelegateResolutionChanged(int index, int width, int height); 

        //【码流开始播放的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        public delegate void DelegatePlayBegan(int index); 

        //【接收数据长度的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        //参数-len				： 长度
        public delegate void DelegateReceiveData(int index, int len); 

        //【录像回放结束的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        public delegate void DelegatePlayFinished(int index); 

        //【解码时间戳的回调】
        //参数-index			： initVideoPlay传入的窗口标识ID
        //参数-time				： UTC时间
        public delegate void DelegatePlayerTime(int index, long time); 

        //【语音对讲业务码的回调】
        //参数-error			： 状态码
        //参数-type				： 业务类型
        public delegate void DelegateTalkResult(string error, int type); 

        //【下载数据长度的回调】
        //参数-index			： 该路下载的唯一标示ID
        //参数-datalen			： 下载的数据长度
        public delegate void DelegateDownloadReceiveData(int index, int datalen); 

        //【下载状态的回调】
        //参数-index			： 该路下载的唯一标示ID
        //参数-code				： 状态码
        //参数-type				： 业务类型
        public delegate void DelegateDownloadState(int index, string code, int type); 

        //【下载结束的回调】
        //参数-index			： 该路下载的唯一标示ID
        public delegate void DelegateDownloadEnd(int index); 
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct sLCCallBack
    {
        public LCCallBack.DelegatePlayerResult lpOnPlayerResult;
        public LCCallBack.DelegateResolutionChanged lpOnResolutionChanged;
        public LCCallBack.DelegatePlayBegan lpOnPlayBegan;
        public LCCallBack.DelegateReceiveData lpOnReceiveData;
        public LCCallBack.DelegatePlayFinished lpOnPlayFinished;
        public LCCallBack.DelegatePlayerTime lpOnPlayerTime;
        public LCCallBack.DelegateTalkResult lpOnTalkResult;
        public LCCallBack.DelegateDownloadReceiveData lpOnDownloadReceiveData;
        public LCCallBack.DelegateDownloadState lpOnDownloadState;
        public LCCallBack.DelegateDownloadEnd lpOnDownloadEnd;
    }

    public class LCSDK
    {
        const string dllName = @"LCSDK\LCOpenSDK.dll";        
        static LCHTTPS https = new LCHTTPS();
        
        

        //【注册消息回调函数】
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool LCOpenSDK_regeditCallback(sLCCallBack callBackFun, int regeditType);

        //【接口初始化】
        //参数-param				
        //	host        乐橙开放平台域名:openapi.lechange.cn
        //	port        乐橙开放平台端口:443
        //	caPath      CA根证书路径222.pem，根证书随SDK一同发布,最好用绝对路径,本DEMO路径为Application.StartupPath + "\\LCSDK\\222.pem";
        //	备注：当port=0时按以上默认值初始化
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void  LCOpenSDK_initOpenApi(OPEN_API_INIT_PARAM param);


        //【开启视频】
        //参数-hWnd		播放窗口的句柄
        //参数-devInfo	设备信息，见_dhlc_device_t定义
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int  LCOpenSDK_startStream(_dhlc_device_t devInfo);

        //【关闭视频】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_stopStream(int index);

        //【播放声音】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_playAudio(int index);

        //【关闭声音】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_stopAudio(int index);


        //【录像回放定位功能】
        //参数-index	视频播放类对象的标识ID
        //参数-seconds    ;//相对录像起始时间的偏移秒数
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_seek(int index,int seconds);

        //【播放控制】
        //参数-index	视频播放类对象的标识ID
        //参数-ctrlType    ;//控制类型，0：暂停播放，1：恢复播放
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_playControl(int index,int ctrlType);

        //【本地抓图】
        //参数-index	视频播放类对象的标识ID
        //参数-filePath    ;//图片文件名（包含路径）
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_snapShot(int index, byte[] filePath);

        //【开始本地录制】
        //参数-index	视频播放类对象的标识ID
        //参数-filePath    ;//图片文件名（包含路径）
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_startRecord(int index, byte[] filePath);

        //【停止本地录制】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_stopRecord(int index);

        //【开启语音对讲】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_playTalk(int index);

        //【停止语音对讲】
        //参数-index	视频播放类对象的标识ID
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_stopTalk(int index);    
    
        //【开启云录像下载】
        //参数-index	该路下载的唯一标示ID
        //参数-token    授权Token
        //参数-filePath  下载文件名（包含路径）
        //参数-deviceID    设备序列号
        //参数-recordID    云录像ID
        //参数-timeout    下载云录像切片的超时时长
        //返回值:		0表示成功，-1表示失败
        [DllImport(dllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int LCOpenSDK_startDownload(int index, byte[] token, byte[] filepath, byte[] deviceId, Int64 recordId, int timeout);

        #region HTTPS
        //【设置开发者appId和appSecret】
        //参数-appId		
        //参数-appSecret
        public static void LCOpenSDK_SetSystem(string appId, string appSecret)
        {
            https.SetSystem(appId, appSecret);
        }

        //【获取T0KEN】
        //参数-phone	    电话号码
        //参数-tokenType	token类型，0：用户，1：管理员
        //参数-token        返回内容
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_GetToken(string phone, int tokenType, ref string token)
        {
            return https.GetToken(phone, tokenType, ref token);
        }

        //【云台控制】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-channelId    通道号
        //参数-ctrlType     方向
        //参数-errorMsg	    错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_PTZCtrl(string token, string deviceId, int channelId, PTZCtrlType ctrlType, ref string errorMsg)
        {
            return https.PTZCtrl(token, deviceId, channelId, ctrlType,ref errorMsg);
        }

        //【发送短信验证码】
        //参数-phone	    手机号码
        //参数-errorMsg	    错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_GetUserBindSms(string phone, ref string errorMsg)
        {
            return https.GetUserBindSms(phone, ref errorMsg);
        }

        //【发送短信验证码】
        //参数-phone	    手机号码
        //参数-smsCode	    短信验证码
        //参数-errorMsg	    错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_UserBind(string phone, string smsCode, ref string errorMsg)
        {
            return https.UserBind(phone,smsCode, ref errorMsg);
        }

        //【获取设备在线状态】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-channelId	通道号
        //参数-errorInfo	错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_GetDeviceOnline(string token, string deviceId, int channelId, ref string errorInfo)
        {
            return https.GetDeviceOnline(token, deviceId, channelId, ref errorInfo);
        }

        //【设备是否已绑定】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-errorInfo	错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_CheckDeviceBindOrNot(string token, string deviceId, ref string errorInfo)
        {
            return https.CheckDeviceBindOrNot(token, deviceId,  ref errorInfo);
        }

        //【单个设备信息获取】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-deviceInfo	设备信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_GetBindDeviceInfo(string token, string deviceId, ref string deviceInfo)
        {
            return https.GetBindDeviceInfo(token, deviceId, ref deviceInfo);
        }

        //【分页获取绑定的设备列表】
        //参数-token	    授权Token
        //参数-queryRange	第几条到第几条,数字取值范围为：[1,N]（N为正整数，且后者＞前者）; 差值取值范围为：[0, 99]
        //参数-deviceList	设备列表
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_GetdeviceList(string token, string queryRange, ref string deviceList, int shareDevice = 0)
        {
            return https.GetdeviceList(token, queryRange, ref deviceList, shareDevice);

        }

        //【查询远程录像片段】
        //参数-qeuryRecord	    查询参数信息
        //参数-recordList	    录像片段列表
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_QueryRecordList(QeuryRecord qeuryRecord, ref string recordList)
        {
            return https.QueryRecordList(qeuryRecord, ref recordList);
        }

        //【设备绑定】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-code	        验证码
        //参数-errorMsg	    错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_BindDevice(string token, string deviceId, string code, ref string errorMsg)
        {
            return https.BindDevice(token, deviceId, code, ref errorMsg);
        }

        //【设备解绑】
        //参数-token	    授权Token
        //参数-deviceId	    设备序列号
        //参数-errorMsg	    错误信息
        //返回值:		    =false获取失败
        public static bool LCOpenSDK_UnBindDevice(string token, string deviceId,ref string errorMsg)
        {
            return https.UnBindDevice(token, deviceId, ref errorMsg);
        }
        #endregion
    }
}
