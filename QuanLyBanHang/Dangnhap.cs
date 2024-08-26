using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
  
namespace QuanLyBanHang
{
    public partial class Dangnhap : Form
    {
        public static string sqlcon = @"Data Source=LAPTOP-H9963E62\DUYDUC;Initial Catalog=ClubThitChoBachKhoa_DuyDucBanHang;Integrated Security=True";

        public static SqlConnection mycon;
        public static SqlCommand com;
        public static SqlDataAdapter ad;
        public static DataTable dt;
        public static SqlCommandBuilder bd;

        public static string getNameUser(string fullname )
        {
            return fullname;
        }

        public static string username;
        public Dangnhap()
        {
            InitializeComponent();
        }
        public static void Chuoiketnoi(string chuoi, DataGridView db1)
        {
            try
            {

                ad = new SqlDataAdapter(chuoi, sqlcon);
                dt = new DataTable();
                bd = new SqlCommandBuilder(ad);
                ad.Fill(dt);
                db1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối " + ex, "Thông báo ! ");

            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                txt_mk.UseSystemPasswordChar = true;

            }
            else
                txt_mk.UseSystemPasswordChar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string sql1 = "Select count(*) from taikhoan where username='" + txt_tk.Text.Trim() + "' and password='" + txt_mk.Text.Trim() + "' ";       
            if (txt_tk.Text == "" || txt_mk.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin! ", "Thông báo", MessageBoxButtons.OK);
            }

            else
            {
                int a = 0;
                mycon = new SqlConnection(sqlcon);
                mycon.Open();

                com = new SqlCommand(sql1, mycon);
                a = (int)com.ExecuteScalar();


                
                
          
                if (a == 0)
                {
                    string t = "Tài khoản hoặc mật khẩu không đúng ";
                    MessageBox.Show((t), "Thông báo", MessageBoxButtons.OK);
                }
                else {
                    string sql = "Select isAdmin from taikhoan where username='" + txt_tk.Text.Trim() + "'";
                    mycon = new SqlConnection(sqlcon);
                    mycon.Open();
                    SqlCommand getIsAdmin = new SqlCommand(sql, mycon);
                    int getRole = (int)getIsAdmin.ExecuteScalar();

                    if (getRole == 0)
                {

                    // MessageBox.Show("Đăng nhập thành công admin!", "Thông báo ", MessageBoxButtons.OK);
                    main a1 = new main();
                    banhang.banhang bh = new banhang.banhang();

                    a1.lb_quyen.Text = GetFullname(0,txt_tk.Text.Trim()) + " (Quản lí)";
                    bh.lb_quyen.Text = GetFullname(0, txt_tk.Text.Trim()) + " (Quản lí)";

                    a1.Show();
                    this.Hide();                  
                }
                else
                {
                    //MessageBox.Show("Đăng nhập thành công", "Thông báo ", MessageBoxButtons.OK);
                    banhang.banhang bh = new banhang.banhang();
                    bh.lb_quyen.Text = GetFullname(1, txt_tk.Text.Trim()) + " (Nhân viên)";
                    bh.Show();
                    this.Hide();

                    }
                }           
    }
}

        public static string GetFullname(int isAdmin,string username)
        {
            string sqlGetFullname = "Select fullname from taikhoan where isAdmin=" +isAdmin+ " and username='"+ username+"'";
            mycon = new SqlConnection(sqlcon);
            mycon.Open();
            SqlCommand getFullname = new SqlCommand(sqlGetFullname, mycon);
            string Fullname = getFullname.ExecuteScalar().ToString();     

            return Fullname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Thông báo ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string t = "Làm cốc trà sữa rồi đi ngủ bao giờ nhớ ra mật khẩu thì quay lại! ";
            MessageBox.Show((t), "Thông báo", MessageBoxButtons.OK);
        }
    }
}
