
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai3
{
    public class frmDoctorSchedule : Form
    {
        private DataGridView dgvSchedule = new DataGridView();
        private BindingList<DoctorShift> schedules =
            new BindingList<DoctorShift>();

        public frmDoctorSchedule()
        {
            TaoGiaoDien();
            NapDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "LỊCH TRỰC BÁC SĨ";
            Size = new Size(800, 450);

            dgvSchedule.Dock = DockStyle.Fill;
            dgvSchedule.AutoGenerateColumns = true;
            dgvSchedule.ReadOnly = true;
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSchedule.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(dgvSchedule);
        }

        private void NapDuLieu()
        {
            schedules.Add(new DoctorShift
            {
                DoctorId = "BS001",
                DoctorName = "Nguyễn Văn An",
                Department = "Khoa Nội",
                Shift = "Ca sáng",
                Date = DateTime.Today.ToString("dd/MM/yyyy")
            });

            schedules.Add(new DoctorShift
            {
                DoctorId = "BS002",
                DoctorName = "Trần Thị Bình",
                Department = "Khoa Ngoại",
                Shift = "Ca chiều",
                Date = DateTime.Today.ToString("dd/MM/yyyy")
            });

            schedules.Add(new DoctorShift
            {
                DoctorId = "BS003",
                DoctorName = "Lê Văn Cường",
                Department = "Khoa Cấp cứu",
                Shift = "Ca đêm",
                Date = DateTime.Today.ToString("dd/MM/yyyy")
            });

            dgvSchedule.DataSource = schedules;
        }
    }

    public class DoctorShift
    {
        public string DoctorId { get; set; } = "";
        public string DoctorName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Shift { get; set; } = "";
        public string Date { get; set; } = "";
    }
}

