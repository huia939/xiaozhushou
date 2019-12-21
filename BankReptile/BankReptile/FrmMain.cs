using Common;
using Common.Log;
using Common.Types;
using Entity;
using Entity.WebApiResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BankReptile
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            MTLogger log = new MTLogger("token.log");
            Token = log.ReadContent();

            Bind();
        }

        private string Token
        {
            get; set;
        }

        private void Bind()
        {
            string result = CallApi.PostAPI($"{AppConfig.WebApi}/api/device/getBankList", $"token={Token}");

            var model = JsonHelper.ToObject<GetBankListResponse>(result);
            if (model == null)
            {
                MessageBox.Show("获取银行列表异常！接口返回为空。");
                this.Close();
                return;
            }
            if (model.status != 1)
            {
                MessageBox.Show(model.message);
                this.Close();
                return;
            }

            dgvBanks.DataSource = model.result;
            for (int i = 3; i < dgvBanks.ColumnCount; i++)  //隐藏dataGridView1控件中不需要的列字段,从第3列开始隐藏
            {
                dgvBanks.Columns[i].Visible = false;
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void dgvBanks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("您单击的是第" + (e.RowIndex + 1) + "行第" + (e.ColumnIndex + 1) + "列！");
            //MessageBox.Show("单元格的内容是：" + dgvBanks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void dgvBanks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show("单元格的内容是：" + dgvBanks.Rows[e.RowIndex].Cells[5].Value.ToString());
            //点击进行数据操作
            try
            {
              
                string jsonTO = "{\"UserName\":\"zwf1@vip.qq.com\",\"PassWord\":\"zw153768985\",\"code_id\":\"1\",\"token\":\"bf9e9122261b1167bec9d50e3f5660b4\",\"Channel\":\"1\"}";

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "WindowsForms001.exe"; //启动的应用程序名称
                startInfo.Arguments =  null;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

                Process p1 = new Process();
                p1.StartInfo.UseShellExecute = false;

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
