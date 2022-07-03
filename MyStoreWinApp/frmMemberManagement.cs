using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessObject;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        string searchType = "";
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public MemberObject memberInfo { get; set; }
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dvgMemberList.CellDoubleClick += dvgMemberList_CellDoubleClick;
        }

        private void dvgMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMemDetails = new frmMemberDetails
            {
                Text = "Update Member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmMemDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }

        }

        private MemberObject GetMemberObject()
        {
            MemberObject mem = null;
            try
            {
                mem = new MemberObject
                {
                    MemberID = txtMemberID.Text,
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    City = cboCity.Text,
                    Country = cboCountry.Text,
                    Password = txtPassword.Text,
                    
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return mem;
        }

        public void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboCity.SelectedIndex = -1;
            cboCountry.SelectedIndex = -1;
            txtPassword.Text = string.Empty;
            
        }

        public void LoadMemberList()
        {
            
            var mem = memberRepository.GetMembers().OrderByDescending((x) => x.MemberName);
            try
            {
                cboCountryFilter.Text = null;
                cboCityFilter.Text = null;
                txtSearch.Clear();
                cboSearchFilter.Text = null;


                source = new BindingSource();
                source.DataSource = mem;
                
                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                cboCity.DataBindings.Clear();
                cboCountry.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                cboCity.DataBindings.Add("Text", source, "City");
                cboCountry.DataBindings.Add("Text", source, "Country");
                txtPassword.DataBindings.Add("Text", source, "Password");
                

                dvgMemberList.DataSource = null;
                dvgMemberList.DataSource = source;
                if (mem.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            /*
            DialogResult dr = MessageBox.Show("Are you sure logout ?? ", "Confirm logout", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
            }
            */
                this.Hide();
                new frmLogin().Show();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemDetails = new frmMemberDetails
            {
                Text = "Add Member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmMemDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();

                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Are u really wanna delete this member?", "Member Management - Delete Action",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            try
            {
                var mem = GetMemberObject();
                if (d == DialogResult.OK)
                {
                    memberRepository.RemoveAMember(mem.MemberID);
                }
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        public void LoadMemberListSearch(string keyword)
        {
            cboCountryFilter.Text = null;
            cboCityFilter.Text = null;
            var mem = memberRepository.SearchMemberByName("!"); 
            if (searchType == "Member Name")
            {
                mem = memberRepository.SearchMemberByName(keyword);
                
            }
            else if (searchType == "Member ID")
            {
                mem = memberRepository.SearchMemberByID(keyword);
            }

            try
            {
                source = new BindingSource();
                source.DataSource = mem;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                cboCity.DataBindings.Clear();
                cboCountry.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                cboCity.DataBindings.Add("Text", source, "City");
                cboCountry.DataBindings.Add("Text", source, "Country");
                txtPassword.DataBindings.Add("Text", source, "Password");
                

                dvgMemberList.DataSource = null;
                dvgMemberList.DataSource = source;
                if (mem.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list search");
            }
        }

        public void LoadMemberListByFilter(string keyword, string check)
        {
            
            var mem = memberRepository.FilterMemberByCity("!");
            if ("city".Equals(check))
            {
                
                mem = memberRepository.FilterMemberByCity(keyword);       
                txtSearch.Clear();
                cboCountryFilter.Text = null;
            }
            else
            {
                
                mem = memberRepository.FilterMemberByCountry(keyword);
                txtSearch.Clear();
                cboCityFilter.Text = null;

            }
            try
            {
                source = new BindingSource();
                source.DataSource = mem;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                cboCity.DataBindings.Clear();
                cboCountry.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                cboCity.DataBindings.Add("Text", source, "City");
                cboCountry.DataBindings.Add("Text", source, "Country");
                txtPassword.DataBindings.Add("Text", source, "Password");
                

                dvgMemberList.DataSource = null;
                dvgMemberList.DataSource = source;
                if (mem.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list search");
            }
        }


        private void frmMemberManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Are u sure u wanna exist?", "Member Management",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (d == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadMemberListSearch(txtSearch.Text); 
        }



        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cboSearchFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if ("Member Name".Equals(cboSearchFilter.Text))
            {
                searchType = "Member Name";
            }
            else if ("Member ID".Equals(cboSearchFilter.Text))
            {
                searchType = "Member ID";
            }
        }

        private void cboCityFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadMemberListByFilter(cboCityFilter.Text, "city");           
        }

        private void cboCountryFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadMemberListByFilter(cboCountryFilter.Text, "country");           
        } 
    }
}
