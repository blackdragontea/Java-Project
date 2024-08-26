using QuanLyBanHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        public void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banhang.banhang a = new banhang.banhang();
            a.lb_quyen.Text = lb_quyen.Text;
            a.Show();
        }

        public void tàiKhoảnNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        public void mn_tkquanly_Click(object sender, EventArgs e)
        {
           
        }

        private void mn_admin_Click(object sender, EventArgs e)
        {
  
        }

        private void mn_doimk_Click(object sender, EventArgs e)
        {
            TaiKhoan.DoiMk a = new TaiKhoan.DoiMk();
            a.Show();
        }

        private void mn_dx_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dangnhap a = new Dangnhap();
                a.Show();
                this.Hide();
            }
        }

        private void ql_ncc_Click(object sender, EventArgs e)
        {
            quanly.nhacungcap a = new quanly.nhacungcap();
            a.Show();
        }

        private void ql_khachhang_Click(object sender, EventArgs e)
        {
           
        }

        private void ql_nhanvien_Click(object sender, EventArgs e)
        {
            frmTaiKhoan frm = new frmTaiKhoan();
            frm.ShowDialog();
        }

        private void bh_phieunhap_Click(object sender, EventArgs e)
        {
           
        }

        private void bh_nhapnhieu_Click(object sender, EventArgs e)
        {
           
        }

        private void bh_xuatle_Click(object sender, EventArgs e)
        {
           
        }

        private void bh_bannhieu_Click(object sender, EventArgs e)
        {
            banhang.banhang a = new banhang.banhang();
            a.lb_quyen.Text = lb_quyen.Text;
            a.Show();
        }

        private void thôngTinPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String tt = "";
            tt += "Phần mềm: Quản lý bán hàng \n";

            tt += "\n ";
            tt += "version: 0.0.1";
            tt += "\n\n";
            tt += " Bài tập lớn môn: ";
            tt += "\t";
            tt += "Quản lý dự án CNTT";
            tt += "\n";
            tt += "\nSinh viên thực hiện : Nguyễn Duy Đức";
            tt += "một thành viên của Club Thịt Chó Bách Khoa  \n\n";
            tt += "Trường HPC lớp 2622CNT04";
            tt += "\n";
            MessageBox.Show((tt), "Thông tin", MessageBoxButtons.OK);
        }

        private void main2_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
                
            quanly.sanpham a = new quanly.sanpham();
            a.lb_quyen.Text = lb_quyen.Text;
            a.Show();        
        }

        private void ql_phieunhap_Click(object sender, EventArgs e)
        {
           
        }

        private void ql_phieuxuat_Click(object sender, EventArgs e)
        {
          
        }

        private void hỗTrợToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void mn_thongtin_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quanly.frmThongTinDonHang ql = new quanly.frmThongTinDonHang();
            ql.Show();
        }

        private void mn_hethong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dangnhap a = new Dangnhap();
                a.Show();
                this.Hide();
            }
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cáchChếBiếnThịtChóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Đường dẫn URL bạn muốn mở
            string url = "https://www.google.com/search?q=C%C3%A1ch+ch%E1%BA%BF+bi%E1%BA%BFn+th%E1%BB%8Bt+ch%C3%B3";

            try
            {
                // Mở trình duyệt web mặc định của hệ điều hành và điều hướng đến URL
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể mở trình duyệt web.");
            }
        }

    }
}
