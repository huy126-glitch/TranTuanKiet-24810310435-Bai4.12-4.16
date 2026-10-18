
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bai3
{
    public class frmPatientProfiles : Form
    {
        private TextBox txtSearch = new TextBox();
        private DataGridView dgvPatients = new DataGridView();
        private BindingList<PatientInfo> patients =
            new BindingList<PatientInfo>();

        public frmPatientProfiles()
        {
            TaoGiaoDien();
            NapDuLieuMau();
        }

        private void TaoGiaoDien()
        {
            Text = "HỒ SƠ BỆNH NHÂN";
            Size = new Size(900, 550);

            Label lblSearch = new Label
            {
                Text = "Tìm tên bệnh nhân:",
                Location = new Point(20, 22),
                AutoSize = true
            };

            txtSearch.Location = new Point(150, 18);
            txtSearch.Width = 250;

            Button btnSearch = new Button
            {
                Text = "Tìm kiếm",
                Location = new Point(420, 16),
                Width = 100
            };
            btnSearch.Click += (s, e) => TimKiem();

            Button btnAdd = new Button
            {
                Text = "Thêm bệnh nhân",
                Location = new Point(540, 16),
                Width = 130
            };
            btnAdd.Click += (s, e) => ThemBenhNhan();

            dgvPatients.Location = new Point(20, 60);
            dgvPatients.Size = new Size(840, 420);
            dgvPatients.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom |
                AnchorStyles.Left | AnchorStyles.Right;
            dgvPatients.AutoGenerateColumns = true;
            dgvPatients.ReadOnly = true;
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.MultiSelect = false;
            dgvPatients.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.AddRange(new Control[]
            {
                lblSearch, txtSearch, btnSearch, btnAdd, dgvPatients
            });
        }

        private void NapDuLieuMau()
        {
            patients.Add(new PatientInfo
            {
                PatientId = "BN001",
                FullName = "Nguyễn Văn Minh",
                Age = 35,
                Gender = "Nam",
                Diagnosis = "Cảm cúm"
            });

            patients.Add(new PatientInfo
            {
                PatientId = "BN002",
                FullName = "Trần Thị Hoa",
                Age = 28,
                Gender = "Nữ",
                Diagnosis = "Viêm họng"
            });

            patients.Add(new PatientInfo
            {
                PatientId = "BN003",
                FullName = "Lê Văn Nam",
                Age = 42,
                Gender = "Nam",
                Diagnosis = "Đau dạ dày"
            });

            dgvPatients.DataSource = patients;
        }

        private void TimKiem()
        {
            string keyword = txtSearch.Text.Trim();

            if (keyword == "")
            {
                dgvPatients.DataSource = patients;
                return;
            }

            var result = patients
                .Where(p => p.FullName.Contains(
                    keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dgvPatients.DataSource = result;
        }

        private void ThemBenhNhan()
        {
            string name = txtSearch.Text.Trim();

            if (name == "")
            {
                MessageBox.Show("Hãy nhập tên bệnh nhân vào ô tìm kiếm.");
                txtSearch.Focus();
                return;
            }

            string id = "BN" +
                (patients.Count + 1).ToString("D3");

            patients.Add(new PatientInfo
            {
                PatientId = id,
                FullName = name,
                Age = 0,
                Gender = "Chưa cập nhật",
                Diagnosis = "Chưa cập nhật"
            });

            dgvPatients.DataSource = patients;
            txtSearch.Clear();
        }
    }

    public class PatientInfo
    {
        public string PatientId { get; set; } = "";
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public string Gender { get; set; } = "";
        public string Diagnosis { get; set; } = "";
    }
}
