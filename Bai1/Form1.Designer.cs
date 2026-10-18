
namespace Bai1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblMemberId;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPackage;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblFee;

        private System.Windows.Forms.TextBox txtMemberId;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtFee;

        private System.Windows.Forms.ComboBox cboPackage;
        private System.Windows.Forms.DateTimePicker dtpStartDate;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblMemberId = new System.Windows.Forms.Label();
            lblFullName = new System.Windows.Forms.Label();
            lblPhone = new System.Windows.Forms.Label();
            lblPackage = new System.Windows.Forms.Label();
            lblStartDate = new System.Windows.Forms.Label();
            lblFee = new System.Windows.Forms.Label();

            txtMemberId = new System.Windows.Forms.TextBox();
            txtFullName = new System.Windows.Forms.TextBox();
            txtPhone = new System.Windows.Forms.TextBox();
            txtFee = new System.Windows.Forms.TextBox();

            cboPackage = new System.Windows.Forms.ComboBox();
            dtpStartDate = new System.Windows.Forms.DateTimePicker();

            btnAdd = new System.Windows.Forms.Button();
            btnEdit = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();

            dgvMembers = new System.Windows.Forms.DataGridView();
            errorProvider =
                new System.Windows.Forms.ErrorProvider(components);

            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();

            SuspendLayout();

            // Form
            Text = "Quản lý dịch vụ phòng Gym và hội viên";
            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            ClientSize = new System.Drawing.Size(1000, 650);
            MinimumSize = new System.Drawing.Size(850, 550);
            Font = new System.Drawing.Font("Segoe UI", 10F);

            // Labels
            lblMemberId.Text = "Mã hội viên:";
            lblFullName.Text = "Họ và tên:";
            lblPhone.Text = "Số điện thoại:";
            lblPackage.Text = "Gói tập:";
            lblStartDate.Text = "Ngày đăng ký:";
            lblFee.Text = "Phí dịch vụ:";

            System.Windows.Forms.Label[] labels =
            {
                lblMemberId, lblFullName, lblPhone,
                lblPackage, lblStartDate, lblFee
            };

            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Location =
                    new System.Drawing.Point(25, 25 + i * 48);
                labels[i].Size =
                    new System.Drawing.Size(125, 28);
            }

            // TextBox
            txtMemberId.Location = new System.Drawing.Point(155, 25);
            txtFullName.Location = new System.Drawing.Point(155, 73);
            txtPhone.Location = new System.Drawing.Point(155, 121);
            txtFee.Location = new System.Drawing.Point(155, 265);

            txtMemberId.Size = new System.Drawing.Size(260, 30);
            txtFullName.Size = new System.Drawing.Size(260, 30);
            txtPhone.Size = new System.Drawing.Size(260, 30);
            txtFee.Size = new System.Drawing.Size(260, 30);

            txtMemberId.ReadOnly = true;

            // ComboBox
            cboPackage.Location = new System.Drawing.Point(155, 169);
            cboPackage.Size = new System.Drawing.Size(260, 30);
            cboPackage.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            cboPackage.Items.AddRange(new object[]
            {
                "Theo tháng",
                "6 tháng",
                "1 năm"
            });

            // DateTimePicker
            dtpStartDate.Location = new System.Drawing.Point(155, 217);
            dtpStartDate.Size = new System.Drawing.Size(260, 30);
            dtpStartDate.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Buttons
            btnAdd.Text = "Thêm mới";
            btnEdit.Text = "Sửa thông tin";
            btnDelete.Text = "Xóa hội viên";
            btnRefresh.Text = "Làm mới";

            btnAdd.Location = new System.Drawing.Point(455, 25);
            btnEdit.Location = new System.Drawing.Point(600, 25);
            btnDelete.Location = new System.Drawing.Point(455, 73);
            btnRefresh.Location = new System.Drawing.Point(600, 73);

            System.Windows.Forms.Button[] buttons =
            {
                btnAdd, btnEdit, btnDelete, btnRefresh
            };

            foreach (System.Windows.Forms.Button button in buttons)
            {
                button.Size = new System.Drawing.Size(130, 36);
                button.Cursor = System.Windows.Forms.Cursors.Hand;
            }

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvMembers.Location = new System.Drawing.Point(25, 330);
            dgvMembers.Size = new System.Drawing.Size(950, 290);

            dgvMembers.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            dgvMembers.AutoGenerateColumns = false;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.ReadOnly = true;
            dgvMembers.MultiSelect = false;
            dgvMembers.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colMemberId",
                    HeaderText = "Mã HV",
                    DataPropertyName = "MemberId"
                });

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colFullName",
                    HeaderText = "Tên hội viên",
                    DataPropertyName = "FullName"
                });

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colPhone",
                    HeaderText = "SĐT",
                    DataPropertyName = "Phone"
                });

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colPackage",
                    HeaderText = "Gói tập",
                    DataPropertyName = "Package"
                });

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colStartDate",
                    HeaderText = "Ngày ĐK",
                    DataPropertyName = "StartDate",
                    DefaultCellStyle =
                        new System.Windows.Forms.DataGridViewCellStyle
                        {
                            Format = "dd/MM/yyyy"
                        }
                });

            dgvMembers.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    Name = "colFee",
                    HeaderText = "Phí dịch vụ",
                    DataPropertyName = "Fee",
                    DefaultCellStyle =
                        new System.Windows.Forms.DataGridViewCellStyle
                        {
                            Format = "N0"
                        }
                });

            dgvMembers.SelectionChanged +=
                dgvMembers_SelectionChanged;

            // ErrorProvider
            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle =
                System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // Add controls
            Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblMemberId, lblFullName, lblPhone,
                lblPackage, lblStartDate, lblFee,
                txtMemberId, txtFullName, txtPhone, txtFee,
                cboPackage, dtpStartDate,
                btnAdd, btnEdit, btnDelete, btnRefresh,
                dgvMembers
            });

            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
