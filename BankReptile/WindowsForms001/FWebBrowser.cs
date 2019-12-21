using Common;
using Common.Types;
using Entity;
using Entity.WebApiResponse;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static WindowsForms001.InternetSet;

namespace WindowsForms001
{

    public partial class FWebBrowser : Form
    {
        private bool IsLogin = false;
        private ActionInvoke invoke;

        private TaskInfo TaskInfo
        {
            get; set;
        }
        public FWebBrowser(TaskInfo info, string args)
        {
            InitializeComponent();

            invoke = new ActionInvoke(this);
            TaskInfo = info;

            txtAccount.Text = info.UserName;
            txtPassword.Text = info.PassWord;
            txtPassword.TextBox.PasswordChar = '*';


            WfmLogin();

        }

        /// <summary>
        /// 登录操作
        /// </summary>
        private void WfmLogin()
        {
            webBrowser1.Navigate("https://auth.alipay.com/login/index.htm");
            Thread.Sleep(5000);
            //加载完毕后触发事件webBrowser1_DocumentCompleted
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        /// <summary>
        /// 页面登录成功
        /// 进行数据上传操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpadteDate_Click(object sender, EventArgs e)
        {
            UpdateJsonData();

        }
        private void Start()
        {
            new Thread(() =>
            {
                Thread.Sleep(3000);
                while (true)
                {
                    try
                    {
                        WfmRefresh();
                    }
                    catch
                    {

                    }
                    //35秒刷新下页面。保持通讯状态
                    Thread.Sleep(35000);
                }
            }).Start();

            //心跳包通讯
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {

                        //判断是否登录
                        WebBrowserToStandard();

                        //判断用户是否登录成功
                        if (IsLogin)
                        {
                            invoke.Invoke(() =>
                            {
                                lblMessage.Text = "登录成功";
                                lblMessage.ForeColor = Color.Green;
                            });

                            SetMessage(MessageType.状态, "登录成功");
                            //上报心跳包并获取PageIndex   未实现
                            //获取到的PageIndex保存进入Pages

                            ///api/device/ping
                            //心跳包请求ID
                            string result = CallApi.PostAPI($"{AppConfig.WebApi}/api/device/ping", $"code_id={TaskInfo.code_id}&token={TaskInfo.token}");
                            var model = JsonHelper.ToObject<PingRespnse>(result);
                            if (model.status == 1)
                            {
                                if (model.result.need_data == 1)
                                {
                                    //等于1 则进行数据 抓取上传操作
                                    UpdateJsonData();
                                }
                            }
                        }
                        else
                        {
                            invoke.Invoke(() =>
                            {
                                lblMessage.Text = "未登录";
                                lblMessage.ForeColor = Color.Red;
                            });

                            SetMessage(MessageType.状态, "未登录");
                        }
                    }
                    catch
                    {

                    }
                    Thread.Sleep(15000);//15秒保持心跳通讯
                }
            }).Start();

        }

        /// <summary>
        /// 
        /// 上传解析
        /// </summary>
        private void UpdateJsonData()
        {
            //获取数据采集页面
            //HtmlAgilityPack.HtmlDocument doc = (HtmlAgilityPack.HtmlDocument)webBrowser1.Document.DomDocument;
            HtmlElement doc = webBrowser1.Document.GetElementById("container");
            string docmaHtml = doc.InnerHtml;

            GetDealPostDate(docmaHtml);
        }


        /// <summary>
        /// 跳转到查询交易明细页面
        /// </summary>
        private void WebBrowserToStandard()
        {
            try
            {
                //获取页面内容。然后解析
                var txtIsLoginConent = webBrowser1.Document.GetElementById("container");
                if (txtIsLoginConent == null)
                {
                    IsLogin = false;
                }
                else
                {
                    IsLogin = true;
                    var txtIsLoginTable = webBrowser1.Document.GetElementById("tradeRecordsIndex");

                    if (txtIsLoginTable == null)
                    {
                        webBrowser1.Navigate("https://consumeprod.alipay.com/record/standard.htm");
                        Thread.Sleep(5000);
                    }

                }
            }
            catch (Exception ex)
            {
                IsLogin = false;
            }
        }

        /// <summary>
        /// 页面加载完毕执行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //设置账户和密码
            HtmlElement txtUserName = webBrowser1.Document.GetElementById("J-input-user");
            HtmlElement txtPassWord = webBrowser1.Document.GetElementById("password_rsainput");

            if (txtUserName == null || txtPassWord == null)
                return;

            txtUserName.SetAttribute("value", TaskInfo.UserName);
            txtPassWord.SetAttribute("value", TaskInfo.PassWord);

            ////获取按钮登录
            HtmlElement txtBtnLogin = webBrowser1.Document.GetElementById("J-login-btn");
            txtBtnLogin.InvokeMember("click");
            Thread.Sleep(5000);

            Start();
        }

        /// <summary>
        /// 手动刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            WfmRefresh();
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        private void WfmRefresh()
        {
            webBrowser1.Document.ExecCommand("Refresh", false, null);//真正意义上的F5刷新
        }

        private void SetMessage(MessageType messageType, string message)
        {
            if (messageType != MessageType.状态)
            {
                message = $"账户{TaskInfo.UserName}提醒您：{message}";
                MessageBox.Show(message);
            }



        }


        private void lblShowPassWord_Click(object sender, EventArgs e)
        {
            if (lblShowPassWord.Text == "显示密码")
            {
                txtPassword.TextBox.PasswordChar = '\0';
                lblShowPassWord.Text = "隐藏密码";
            }
            else
            {
                txtPassword.TextBox.PasswordChar = '*';
                lblShowPassWord.Text = "显示密码";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        #region MyRegion

        /// <summary>
        /// 页面内容转化DataTable进行上传操作
        /// </summary>
        /// <param name="str"></param>
        private void GetDealPostDate(string str)
        {
            DataTable dtstr = GetQueryDataTable(str, "");

            if (dtstr != null)
            {  //有数据则进行数据上传操作
                this.dataGridView1.DataSource = dtstr;
                //上传操作
                string strDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dtstr, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.Converters.DataTableConverter());
                string strData = "{\"data\":" + strDataJson + "}";
                string result = CallApi.PostAPI($"{AppConfig.WebApi}/api/device/postData", $"code_id={TaskInfo.code_id}&page=1&token={TaskInfo.token}&data={strData}");
                var model = JsonHelper.ToObject<DefaultReponse>(result);
                if (model.status == 1)
                {
                    SetMessage(MessageType.默认, TaskInfo.UserName + "交易记录上传成功");
                }
            }

        }
        /// <summary>
        /// html解析转化为DataTable
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="TableID"></param>
        /// <param name="IsSkipTitle"></param>
        /// <returns></returns>
        private static DataTable GetQueryDataTable(string Text, string TableID, bool IsSkipTitle = false)
        {
            try
            {
                DataTable dt = new DataTable();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Text);
                HtmlNodeCollection Table = doc.DocumentNode.SelectNodes("//*[@id='tradeRecordsIndex']");
                if (Table.Count > 0) //判断是否存在
                {
                    HtmlNodeCollection Th = null;
                    int index = 0;
                    HtmlNodeCollection HtmlTr = Table[0].SelectNodes("tbody")[0].SelectNodes("tr");
                    if (HtmlTr.Count > 1) //判断没有数据的
                    {
                        //HtmlNodeCollection Ths = Table[0].SelectNodes("thead")[0].SelectNodes("tr")[0].SelectNodes("th");
                        //if (Ths != null) //创建表字段
                        //{
                        //    Th = Ths;
                        //    for (int i = 0; i < Th.Count; i++)
                        //    {
                        //        dt.Columns.Add(Th[i].InnerText, System.Type.GetType("System.String")); //动态加列
                        //    }
                        //}
                        dt.Columns.Add("order_no", System.Type.GetType("System.String"));
                        dt.Columns.Add("trading_time", System.Type.GetType("System.String"));
                        dt.Columns.Add("order_money", System.Type.GetType("System.String"));
                        dt.Columns.Add("reciprocal_account", System.Type.GetType("System.String"));
                        dt.Columns.Add("reciprocal_name", System.Type.GetType("System.String"));
                        dt.Columns.Add("lei_6", System.Type.GetType("System.String"));

                        foreach (HtmlNode row in Table[0].SelectNodes("tbody")[0].SelectNodes("tr"))
                        {
                            if (IsSkipTitle && index == 0) //去掉表头
                            {
                                index++;
                                continue;
                            }

                            HtmlNodeCollection tds = row.SelectNodes("td");
                            if (tds != null)
                            {
                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < tds.Count; i++)
                                {
                                    //自动匹配抓取
                                    //if (!string.IsNullOrEmpty(Th[i].InnerText))
                                    //    dr[Th[i].InnerText] = tds[i].InnerText;

                                    switch (i)
                                    {
                                        case 0:
                                            dr["trading_time"] = GetTimeStamp();
                                            dr["lei_6"] = 1;
                                            dr["reciprocal_account"] = "";
                                            break;
                                        case 1:

                                            break;
                                        case 2:
                                            dr["reciprocal_name"] = tds[i].SelectNodes("p")[0].InnerText.Trim() + "|" + tds[i].SelectNodes("p")[1].InnerText.Trim();
                                            dr["order_no"] = tds[i].SelectNodes("div")[0].SelectNodes("a")[0].Attributes["title"].Value;
                                            break;
                                        case 3:
                                            dr["order_money"] = tds[i].InnerText.Trim();
                                            break;
                                        default:
                                            break;
                                    }


                                }


                                dt.Rows.Add(dr);
                            }
                            index++;
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        #endregion
    }
}
