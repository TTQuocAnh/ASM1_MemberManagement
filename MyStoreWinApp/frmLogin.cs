using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppSetting.Json;
using DataAccess;
using BusinessObject;
using DataAccess.Repository;


namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        BindingSource source;
        IMemberRepository memberRepository = new MemberRepository();
        public MemberObject memberInfo { get; set; }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            string fileName = "appsetting.json";
            string json = File.ReadAllText(fileName);  // đọc text từ tập tin JSON

            // deserialize từ chuỗi đọc ở tập tin JSOn --> đối tượng DefaultAccount
            var adminAccount = JsonSerializer.Deserialize<AccountDefault>(json);

            string email = adminAccount.Email;
            string password = adminAccount.Password;
            string role = adminAccount.Role;


            

            if (txtUserName.Text == email && txtPassword.Text == password)
            {
                new frmMemberManagement().Show();
                this.Hide();
            }else
            {

                memberInfo = memberRepository.GetMemberByEmail(txtUserName.Text);
                if (memberInfo != null)
                {
                    if (memberInfo.Email == txtUserName.Text && memberInfo.Password == txtPassword.Text)
                    {
                        frmMemberDetails frmMemDetails = new frmMemberDetails
                        {
                            Text = "Your information",
                            InsertOrUpdate = true,
                            MemberInfo = memberInfo,
                            MemberRepository = memberRepository
                            

                        };
                        frmMemDetails.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Oops! Your password is incorrect, please try again!");
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Oops! Your account doesn't exist, please try again!");
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                    
            }
            
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
