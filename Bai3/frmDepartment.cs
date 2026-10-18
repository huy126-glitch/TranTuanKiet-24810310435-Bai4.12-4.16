
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai3
{
    public class frmDepartment : Form
    {
        private DataGridView dgvDepartments = new DataGridView();

        private BindingList<DepartmentInfo> departments =
            new BindingList<DepartmentInfo>();

        public frmDepartment()
        {
            Text = "DANH MỤC KHOA / PHÒNG";
            Size = new Size(800, 450);
            StartPosition = FormStartPosition.CenterScreen;

            dgvDepartments.Dock = DockStyle.Fill;
            dgvDepartments.AutoGenerateColumns = true;
            dgvDepartments.ReadOnly = true;
            dgvDepartments.AllowUserToAddRows = false;
            dgvDepartments.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvDepartments.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(dgvDepartments);

            NapDuLieu();
        }

        private void NapDuLieu()
        {
            departments.Add(new DepartmentInfo
                {
                    DepartmentId = "K001",
                    DepartmentName = "Khoa Nội",
                    Location = "Tầng 1"
                });

            departments.Add(new DepartmentInfo
                {
                    DepartmentId = "K002",
                    DepartmentName = "Khoa Ngoại",
                    Location = "Tầng 2"
                });

            departments.Add(new DepartmentInfo
                {
                    DepartmentId = "K003",
                    DepartmentName = "Khoa Cấp cứu",
                    Location = "Tầng 1"
                });

            departments.Add(new DepartmentInfo
                {
                    DepartmentId = "K004",
                    DepartmentName = "Khoa Nhi",
                    Location = "Tầng 3"
                });

            dgvDepartments.DataSource = departments;
        }
    }

    public class DepartmentInfo
    {
        public string DepartmentId{ get; set; } = "";
        public string DepartmentName{ get; set; } = "";
        public string Location{ get; set; } = "";
    }
}
