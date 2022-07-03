using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {
        public frmMemberDetails()
        {
            InitializeComponent();
        }

        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public MemberObject MemberInfo { get; set; }

        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            cboCountry.SelectedIndex = 0;
            cboCity.SelectedIndex = 0;
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtMemberName.Text = MemberInfo.MemberName.ToString();

                txtEmail.Text = MemberInfo.Email;               
                txtPassword.Text = MemberInfo.Password.ToString();
                cboCountry.SelectedItem = MemberInfo.Country;
                cboCity.SelectedItem = MemberInfo.City;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var mem = new MemberObject
                {
                    MemberID = txtMemberID.Text,
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Country = cboCountry.Text,
                    City = cboCity.Text,
                    Password = txtPassword.Text
                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.AddNewMember(mem);
                    MessageBox.Show("Congratulation! Add new member successfully!");
                }
                else
                {   
                    MemberRepository.UpdateAMember(mem);
                    MessageBox.Show("Congratulation! Update information successfully!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new member!" : "Update a member!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
