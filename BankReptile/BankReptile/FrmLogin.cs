using Common;
using Common.Log;
using Common.Types;
using Entity.WebApiResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BankReptile
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = CallApi.PostAPI($"{AppConfig.WebApi}/api/device/userLogin", $"account={txtUserName.Text}&password={txtPassWord.Text}");
            var model = JsonHelper.ToObject<UserLoginResponse>(result);
            if(model == null)
            {
                MessageBox.Show("登录异常！接口返回为空。");
                return;
            }
            if(model.status != 1)
            {
                MessageBox.Show(model.message);
                return;
            }
            MTLogger log = new MTLogger("token.log");
            log.WriteAll(model.result.token);

            this.Hide();

            FrmMain frmMain = new FrmMain();
            frmMain.Show();
        }
    }
}
