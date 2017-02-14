using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LC_SDK
{
    public class LCHTTPS
    {
        static string AppId = "";
        static string AppSecret = "";

        public void SetSystem(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

        private string GetPostRequest(string posturl, string postData)
        {
            string content = "";
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                content = sr.ReadToEnd();
                int f = 0;
            }
            catch (Exception ex)
            {

            }
            return content;
        }

        /// <summary>
        /// JSON对象名称排序  
        /// </summary>
        /// <summary>
        /// JSON格式化重新排序
        /// </summary>
        /// <param name="jobj">原始JSON JToken.Parse(string json);</param>
        /// <param name="obj">初始值Null</param>
        /// <returns></returns>
        public string SortJson(JToken jobj, JToken obj)
        {
            if (obj == null)
            {
                obj = new JObject();
            }

            List<JToken> list = jobj.ToList<JToken>();
            if (jobj.Type == JTokenType.Object)//非数组
            {
                List<string> listsort = new List<string>();
                foreach (var item in list)
                {
                    string name = JProperty.Load(item.CreateReader()).Name;
                    listsort.Add(name);
                }
                listsort.Sort();
                List<JToken> listTemp = new List<JToken>();
                foreach (var item in listsort)
                {
                    listTemp.Add(list.Where(p => JProperty.Load(p.CreateReader()).Name == item).FirstOrDefault());
                }
                list = listTemp;
                //list.Sort((p1, p2) => JProperty.Load(p1.CreateReader()).Name.GetAnsi() - JProperty.Load(p2.CreateReader()).Name.GetAnsi());

                foreach (var item in list)
                {
                    JProperty jp = JProperty.Load(item.CreateReader());
                    if (item.First.Type == JTokenType.Object)
                    {
                        JObject sub = new JObject();
                        (obj as JObject).Add(jp.Name, sub);
                        SortJson(item.First, sub);
                    }
                    else if (item.First.Type == JTokenType.Array)
                    {
                        JArray arr = new JArray();
                        if (obj.Type == JTokenType.Object)
                        {
                            (obj as JObject).Add(jp.Name, arr);
                        }
                        else if (obj.Type == JTokenType.Array)
                        {
                            (obj as JArray).Add(arr);
                        }
                        SortJson(item.First, arr);
                    }
                    else if (item.First.Type != JTokenType.Object && item.First.Type != JTokenType.Array)
                    {
                        (obj as JObject).Add(jp.Name, item.First);
                    }
                }
            }
            else if (jobj.Type == JTokenType.Array)//数组
            {
                foreach (var item in list)
                {
                    List<JToken> listToken = item.ToList<JToken>();
                    List<string> listsort = new List<string>();
                    foreach (var im in listToken)
                    {
                        string name = JProperty.Load(im.CreateReader()).Name;
                        listsort.Add(name);
                    }
                    listsort.Sort();
                    List<JToken> listTemp = new List<JToken>();
                    foreach (var im2 in listsort)
                    {
                        listTemp.Add(listToken.Where(p => JProperty.Load(p.CreateReader()).Name == im2).FirstOrDefault());
                    }
                    list = listTemp;

                    listToken = list;
                    // listToken.Sort((p1, p2) => JProperty.Load(p1.CreateReader()).Name.GetAnsi() - JProperty.Load(p2.CreateReader()).Name.GetAnsi());
                    JObject item_obj = new JObject();
                    foreach (var token in listToken)
                    {
                        JProperty jp = JProperty.Load(token.CreateReader());
                        if (token.First.Type == JTokenType.Object)
                        {
                            JObject sub = new JObject();
                            (obj as JObject).Add(jp.Name, sub);
                            SortJson(token.First, sub);
                        }
                        else if (token.First.Type == JTokenType.Array)
                        {
                            JArray arr = new JArray();
                            if (obj.Type == JTokenType.Object)
                            {
                                (obj as JObject).Add(jp.Name, arr);
                            }
                            else if (obj.Type == JTokenType.Array)
                            {
                                (obj as JArray).Add(arr);
                            }
                            SortJson(token.First, arr);
                        }
                        else if (item.First.Type != JTokenType.Object && item.First.Type != JTokenType.Array)
                        {
                            if (obj.Type == JTokenType.Object)
                            {
                                (obj as JObject).Add(jp.Name, token.First);
                            }
                            else if (obj.Type == JTokenType.Array)
                            {
                                item_obj.Add(jp.Name, token.First);
                            }
                        }
                    }
                    if (obj.Type == JTokenType.Array)
                    {
                        (obj as JArray).Add(item_obj);
                    }

                }
            }
            string ret = obj.ToString(Formatting.None);
            return ret;
        }

        private JObject GetSystemJsonArray(JObject paramsList)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long time = (long)(DateTime.Now - startTime).TotalSeconds;
            string stringTemp = "";
            StringBuilder str = new StringBuilder();
            foreach (var item in paramsList)
            {
                str.Append(item.Key + ":" + item.Value + ",");
            }
            stringTemp = str.ToString();
            stringTemp += "time:";
            stringTemp += time.ToString();
            stringTemp += ",";
            stringTemp += "nonce:";
            stringTemp += "12345678901234567890123456789012";
            stringTemp += ",";
            stringTemp += "appSecret:";
            stringTemp += AppSecret;

            byte[] result = Encoding.Default.GetBytes(stringTemp);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            stringTemp = BitConverter.ToString(output).Replace("-", "");
            JObject systemList = new JObject(
                            new JProperty("sign", stringTemp),
                            new JProperty("appId", AppId),
                            new JProperty("nonce", "12345678901234567890123456789012"),
                            new JProperty("time", time.ToString()),
                            new JProperty("ver", "1.0")
                        );
            return systemList;
        }

        public bool GetToken(string phone, int tokenType, ref string token)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/";
                if (tokenType == 1)
                    url += "accessToken";
                else
                    url += "userToken";
                JObject jsonObj = new JObject();
                JObject paramsList = new JObject(
                                new JProperty("phone", phone)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);

                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                string stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    if (tokenType == 1)
                        token = jsonObj["result"]["data"]["accessToken"].ToString();
                    else
                        token = jsonObj["result"]["data"]["userToken"].ToString();
                    return true;
                }
                else
                {
                    token = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }


        public bool GetUserBindSms(string phone, ref string errorMsg)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/userBindSms";
                
                JObject jsonObj = new JObject();
                JObject paramsList = new JObject(
                                new JProperty("phone", phone)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);

                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                string stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    return true;
                }
                else
                {
                    errorMsg = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool UserBind(string phone, string smsCode, ref string errorMsg)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/userBind";

                JObject jsonObj = new JObject();
                JObject paramsList = new JObject(
                                new JProperty("phone", phone),
                                new JProperty("smsCode", smsCode)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);

                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                string stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    return true;
                }
                else
                {
                    errorMsg = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool PTZCtrl(string token, string deviceId, int channelId, PTZCtrlType ctrlType, ref string errorMsg)
        {
            try
            {
                int iH, iV, iZ;
                int duration = 100;
                iH = iV = 0;
                iZ = 1;
                errorMsg = "";
                switch (ctrlType)
                {
                    case PTZCtrlType.PTZ_CONTROL_LEFT:
                        iH = -5;
                        iV = 0;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_RIGHT:
                        iH = 5;
                        iV = 0;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_UP:
                        iH = 0;
                        iV = 5;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_DOWN:
                        iH = 0;
                        iV = -5;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_LEFTUP:
                        iH = -5;
                        iV = 5;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_LEFTDOWN:
                        iH = -5;
                        iV = -5;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_RIGHTUP:
                        iH = 5;
                        iV = 5;
                        break;
                    case PTZCtrlType.PTZ_CONTROL_RIGHTDOWN:
                        iH = 5;
                        iV = -5;
                        break;
                }

                string url = "https://openapi.lechange.cn/openapi/controlPTZ";

                JObject jsonObj = new JObject();
                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("duration", duration.ToString()),
                                new JProperty("channelId", channelId.ToString()),
                                new JProperty("h", iH),
                                new JProperty("operation", "move"),
                                new JProperty("token", token),
                                new JProperty("v", iV),
                                new JProperty("z", iZ)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                string stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    return true;
                }
                else
                {
                    errorMsg = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool GetDeviceOnline(string token, string deviceId, int channelId, ref string errorInfo)
        {
            try
            {
                errorInfo = "";
                string url = "https://openapi.lechange.cn/openapi/deviceOnline";
                
                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";

                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);

                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    bool onLine = jsonObj["result"]["data"]["onLine"].ToString() == "1" ? true : false;
                    if (onLine)
                    {
                        int channelCount = jsonObj["result"]["data"]["channels"].Count();
                        for (int i = 0; i < channelCount; i++)
                        {
                            if (jsonObj["result"]["data"]["channels"][i]["channelId"].ToString() == channelId.ToString())
                            {
                                onLine = jsonObj["result"]["data"]["channels"][i]["onLine"].ToString() == "1" ? true : false;
                                break;
                            }
                        }
                    }
                    return onLine;
                }
                else
                {
                    errorInfo = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool CheckDeviceBindOrNot(string token, string deviceId,ref string errorInfo)
        {
            try
            {
                errorInfo = "";
                string url = "https://openapi.lechange.cn/openapi/checkDeviceBindOrNot";

                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";

                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);

                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");

                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    bool onLine = jsonObj["result"]["data"]["isMine"].ToString() == "true" ? true : false;                    
                    return onLine;
                }
                else
                {
                    errorInfo = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool BindDevice(string token, string deviceId, string code, ref string errorMsg)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/bindDevice";

                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";
                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("code", code),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");

                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    return true;
                }
                else
                {
                    errorMsg = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool UnBindDevice(string token, string deviceId, ref string errorMsg)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/unBindDevice";

                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";
                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");

                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    return true;
                }
                else
                {
                    errorMsg = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool GetBindDeviceInfo(string token, string deviceId, ref string deviceInfo)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/bindDeviceInfo";

                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";
                JObject paramsList = new JObject(
                                new JProperty("deviceId", deviceId),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    deviceInfo = jsonObj["result"]["data"].ToString();
                    return true;
                }
                else
                {
                    deviceInfo = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool GetdeviceList(string token, string queryRange, ref string deviceList,int shareDevice=0)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/deviceList";
                if (shareDevice==1)
                    url = "https://openapi.lechange.cn/openapi/shareDeviceList";
                JObject jsonObj = new JObject();
                string stringTemp = "";
                JObject paramsList = new JObject(
                                new JProperty("queryRange", queryRange),
                                new JProperty("token", token)
                            );
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    deviceList = jsonObj["result"]["data"].ToString();
                    return true;
                }
                else
                {
                    deviceList = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }

        public bool QueryRecordList(QeuryRecord qeuryRecord, ref string recordList)
        {
            try
            {
                string url = "https://openapi.lechange.cn/openapi/queryCloudRecords";

                JObject jsonObj = new JObject();
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long time = (long)(DateTime.Now - startTime).TotalSeconds;
                string stringTemp = "";
                JObject paramsList = new JObject(
                                new JProperty("token", qeuryRecord.token),
                                new JProperty("deviceId", qeuryRecord.deviceId),
                                new JProperty("channelId", qeuryRecord.channelId),
                                new JProperty("beginTime", qeuryRecord.beginTime),
                                new JProperty("endTime", qeuryRecord.endTime),
                                new JProperty("queryRange", qeuryRecord.queryRange)
                            );
                if (qeuryRecord.type != "")//查询设备本地录像片段
                {
                    url = "https://openapi.lechange.cn/openapi/queryLocalRecords";
                    paramsList.Add("type", qeuryRecord.type);
                }
                JToken jtoken = JToken.Parse(paramsList.ToString());
                string strJson = SortJson(jtoken, null);
                paramsList = (JObject)JsonConvert.DeserializeObject(strJson);
                JObject systemList = GetSystemJsonArray(paramsList);
                jsonObj.Add("system", systemList);
                jsonObj.Add("params", paramsList);
                jsonObj.Add("id", "88");
                stringTemp = jsonObj.ToString();
                stringTemp = stringTemp.Replace("\r\n", "");
                
                string resultContent = GetPostRequest(url, stringTemp);
                jsonObj = (JObject)JsonConvert.DeserializeObject(resultContent);
                string errcode = jsonObj["result"]["code"].ToString();
                string errmsg = jsonObj["result"]["msg"].ToString();
                if (errcode == "0")
                {
                    recordList = jsonObj["result"]["data"].ToString();
                    return true;
                }
                else
                {
                    recordList = errmsg;
                }
            }
            catch (Exception ex)
            {
                int ii = 0;
            }
            return false;
        }
    }
}
