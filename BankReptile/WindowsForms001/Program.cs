﻿using Common.Types;
using Common.Log;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsForms001
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

            if (args == null || args.Length == 0)
            {
             
                //测试阿里
                TaskInfo task = new TaskInfo
                {
                    UserName = "zwf1@vip.qq.com",
                    PassWord = "zw153768985",
                    code_id = 1,
                    token = "bf9e9122261b1167bec9d50e3f5660b4",
                    Channel = Channel.支付宝
                };

                args = new string[] { JsonHelper.ToJson(task) };
            }
            else
            {
                MessageBox.Show("数据：" + args[0]);
            }

            if (args.Length != 1)
            {
                MessageBox.Show("参数错误！");
                return;
            }

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            //异常处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                string json = System.Web.HttpUtility.UrlDecode(args[0]);
                TaskInfo info = JsonConvert.DeserializeObject<TaskInfo>(json);// JsonHelper.ToObject<TaskInfo>(json);

                FWebBrowser browser = new FWebBrowser(info, args[0]);
                Application.Run(browser);
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show("1sd" + ex.Message);
                    Process.GetCurrentProcess().Kill();
                }
                catch { }
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                MTLogger log = new MTLogger("JWebBrowser程序严重错误.log");

                Exception ex = e.ExceptionObject as Exception;

                if (ex != null)
                {
                    log.WriteLineWithTime("严重错误:" + ex.Message + ex.Source + ex.StackTrace);

                    //MessageBox.Show("发生了严重未处理异常，导致程序中止运行！" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //MessageBox.Show("发生了严重未处理异常，导致程序中止运行！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                MTLogger log = new MTLogger("JWebBrowser程序严重错误.log");

                log.WriteLineWithTime("错误源:" + e.Exception.Source.ToString());
                log.WriteLog(e.Exception);
                log.WriteLineWithTime(e.Exception.StackTrace);
                //MessageBox.Show("发生了一个未捕获的异常，错误信息："+e.Exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { }
        }

    }
}
