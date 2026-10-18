
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public class frmHospitalMain : Form
    {
        private MenuStrip menuStrip = new MenuStrip();
        private ToolStripStatusLabel lblStatus =
            new ToolStripStatusLabel("Sẵn sàng");
        private ToolStripProgressBar progressBar =
            new ToolStripProgressBar();

        public frmHospitalMain()
        {
            Text = "HỆ THỐNG QUẢN LÝ BỆNH VIỆN";
            WindowState = FormWindowState.Maximized;
            IsMdiContainer = true;

            TaoMenu();
            TaoThanhTrangThai();
        }

        private void TaoMenu()
        {
            var heThong = new ToolStripMenuItem("Hệ thống");
            var dangXuat = new ToolStripMenuItem("Đăng xuất");
            var napLai = new ToolStripMenuItem("Nạp lại dữ liệu");
            var thoat = new ToolStripMenuItem("Thoát");

            dangXuat.Click += (s, e) => DangXuat();
            napLai.Click += async (s, e) => await NapLaiDuLieu();
            thoat.Click += (s, e) => Close();

            heThong.DropDownItems.Add(dangXuat);
            heThong.DropDownItems.Add(napLai);
            heThong.DropDownItems.Add(new ToolStripSeparator());
            heThong.DropDownItems.Add(thoat);

            var quanLy = new ToolStripMenuItem("Quản lý");
            var benhNhan = new ToolStripMenuItem("Hồ sơ bệnh nhân");
            var bacSi = new ToolStripMenuItem("Danh mục bác sĩ");
            var khoaPhong = new ToolStripMenuItem("Danh mục khoa/phòng");

            benhNhan.Click += (s, e) =>
                MoFormCon<frmPatientProfiles>();

            bacSi.Click += (s, e) =>
                MoFormCon<frmDoctorSchedule>();

            khoaPhong.Click += (s, e) =>
                MoFormKhoaPhong();

            quanLy.DropDownItems.Add(benhNhan);
            quanLy.DropDownItems.Add(bacSi);
            quanLy.DropDownItems.Add(khoaPhong);

            var cuaSo = new ToolStripMenuItem("Cửa sổ");

            var xepTang = new ToolStripMenuItem("Xếp tầng");
            var ngang = new ToolStripMenuItem("Xếp ngang");
            var doc = new ToolStripMenuItem("Xếp dọc");
            var dongTatCa = new ToolStripMenuItem("Đóng tất cả");

            xepTang.Click += (s, e) =>
                LayoutMdi(MdiLayout.Cascade);

            ngang.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileHorizontal);

            doc.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileVertical);

            dongTatCa.Click += (s, e) =>
                DongTatCaFormCon();

            cuaSo.DropDownItems.Add(xepTang);
            cuaSo.DropDownItems.Add(ngang);
            cuaSo.DropDownItems.Add(doc);
            cuaSo.DropDownItems.Add(new ToolStripSeparator());
            cuaSo.DropDownItems.Add(dongTatCa);

            menuStrip.Items.Add(heThong);
            menuStrip.Items.Add(quanLy);
            menuStrip.Items.Add(cuaSo);

            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void TaoThanhTrangThai()
        {
            StatusStrip statusStrip = new StatusStrip();

            lblStatus.Text = "Nhân viên trực: Quản trị viên";
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.Size = new Size(150, 16);

            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(new ToolStripStatusLabel("  "));
            statusStrip.Items.Add(progressBar);

            Controls.Add(statusStrip);
        }

        private void MoFormCon<T>() where T : Form, new()
        {
            foreach (Form form in MdiChildren)
            {
                if (form is T)
                {
                    if (form.WindowState == FormWindowState.Minimized)
                        form.WindowState = FormWindowState.Normal;

                    form.Activate();
                    return;
                }
            }

            T child = new T();
            child.MdiParent = this;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
        }

        private void MoFormKhoaPhong()
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Text == "DANH MỤC KHOA / PHÒNG")
                {
                    if (form.WindowState == FormWindowState.Minimized)
                        form.WindowState = FormWindowState.Normal;

                    form.Activate();
                    return;
                }
            }

            Form child = new Form();
            child.Text = "DANH MỤC KHOA / PHÒNG";
            child.Size = new Size(800, 450);
            child.MdiParent = this;

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgv.Columns.Add("MaKhoa", "Mã khoa");
            dgv.Columns.Add("TenKhoa", "Tên khoa");
            dgv.Columns.Add("ViTri", "Vị trí");

            dgv.Rows.Add("K001", "Khoa Nội", "Tầng 1");
            dgv.Rows.Add("K002", "Khoa Ngoại", "Tầng 2");
            dgv.Rows.Add("K003", "Khoa Cấp cứu", "Tầng 1");
            dgv.Rows.Add("K004", "Khoa Nhi", "Tầng 3");

            child.Controls.Add(dgv);
            child.WindowState = FormWindowState.Maximized;
            child.Show();
        }

        private async Task NapLaiDuLieu()
        {
            progressBar.Value = 0;
            lblStatus.Text = "Đang nạp dữ liệu...";

            for (int i = 0; i <= 100; i += 10)
            {
                progressBar.Value = i;
                await Task.Delay(100);
            }

            lblStatus.Text = "Nạp dữ liệu hoàn tất";
        }

        private void DangXuat()
        {
            DongTatCaFormCon();
            lblStatus.Text = "Đã đăng xuất";
        }

        private void DongTatCaFormCon()
        {
            foreach (Form form in MdiChildren)
            {
                form.Close();
            }
        }
    }
}
