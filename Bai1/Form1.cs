
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Form1 : Form
    {
        private BindingList<MemberModel> members =
            new BindingList<MemberModel>();

        private BindingSource bindingSource =
            new BindingSource();

        private MemberModel selectedMember = null;
        private int nextId = 1;

        public Form1()
        {
            InitializeComponent();

            bindingSource.DataSource = members;
            dgvMembers.DataSource = bindingSource;

            txtMemberId.Text = GenerateMemberId();
            cboPackage.SelectedIndex = 0;
        }

        // Sinh mã hội viên tự động
        private string GenerateMemberId()
        {
            return "HV" + nextId.ToString("D3");
        }

        // Xóa thông báo lỗi
        private void ClearErrors()
        {
            errorProvider.Clear();
        }

        // Kiểm tra dữ liệu nhập
        private bool ValidateInput()
        {
            ClearErrors();

            bool valid = true;

            string name = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string feeText = txtFee.Text.Trim();

            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(name))
            {
                errorProvider.SetError(
                    txtFullName,
                    "Họ tên không được để trống.");

                valid = false;
            }
            else if (!Regex.IsMatch(
                name,
                @"^[\p{L}]+(?:[ '-][\p{L}]+)*$"))
            {
                errorProvider.SetError(
                    txtFullName,
                    "Họ tên chỉ được chứa chữ cái và khoảng trắng.");

                valid = false;
            }

            // Kiểm tra số điện thoại
            if (!Regex.IsMatch(phone, @"^\d{10}$"))
            {
                errorProvider.SetError(
                    txtPhone,
                    "Số điện thoại phải gồm đúng 10 chữ số.");

                valid = false;
            }

            // Kiểm tra gói tập
            if (cboPackage.SelectedIndex < 0)
            {
                errorProvider.SetError(
                    cboPackage,
                    "Vui lòng chọn gói tập.");

                valid = false;
            }

            // Kiểm tra phí dịch vụ
            decimal fee;

            bool feeValid = decimal.TryParse(
                feeText,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out fee);

            if (!feeValid || fee <= 0)
            {
                errorProvider.SetError(
                    txtFee,
                    "Phí dịch vụ phải là số nguyên dương.");

                valid = false;
            }

            return valid;
        }

        // Lấy dữ liệu từ các Control
        private MemberModel GetInputData(string memberId)
        {
            decimal fee = decimal.Parse(
                txtFee.Text.Trim(),
                CultureInfo.InvariantCulture);

            return new MemberModel
            {
                MemberId = memberId,
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Package = cboPackage.Text,
                StartDate = dtpStartDate.Value.Date,
                Fee = fee
            };
        }

        // Thêm mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            MemberModel member =
                GetInputData(GenerateMemberId());

            members.Add(member);
            nextId++;

            bindingSource.ResetBindings(false);

            MessageBox.Show(
                "Thêm hội viên thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearForm();
        }

        // Sửa thông tin
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedMember == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn hội viên cần sửa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateInput())
                return;

            // Cập nhật đối tượng trong BindingList
            selectedMember.FullName = txtFullName.Text.Trim();
            selectedMember.Phone = txtPhone.Text.Trim();
            selectedMember.Package = cboPackage.Text;
            selectedMember.StartDate = dtpStartDate.Value.Date;

            selectedMember.Fee = decimal.Parse(
                txtFee.Text.Trim(),
                CultureInfo.InvariantCulture);

            bindingSource.ResetBindings(false);

            MessageBox.Show(
                "Cập nhật hội viên thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Xóa hội viên
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMember == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn hội viên cần xóa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa hội viên "
                + selectedMember.FullName + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                members.Remove(selectedMember);

                selectedMember = null;

                bindingSource.ResetBindings(false);

                ClearForm();
            }
        }

        // Làm mới Form
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMembers.ClearSelection();

            selectedMember = null;

            ClearForm();
        }

        // Chọn dòng để hiển thị thông tin
        private void dgvMembers_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null ||
                dgvMembers.CurrentRow.IsNewRow ||
                dgvMembers.SelectedRows.Count == 0)
            {
                return;
            }

            selectedMember =
                dgvMembers.SelectedRows[0].DataBoundItem
                as MemberModel;

            if (selectedMember == null)
                return;

            txtMemberId.Text = selectedMember.MemberId;
            txtFullName.Text = selectedMember.FullName;
            txtPhone.Text = selectedMember.Phone;

            cboPackage.SelectedItem = selectedMember.Package;

            dtpStartDate.Value = selectedMember.StartDate;

            txtFee.Text = selectedMember.Fee.ToString(
                CultureInfo.InvariantCulture);

            ClearErrors();
        }

        // Đưa Form về trạng thái nhập mới
        private void ClearForm()
        {
            ClearErrors();

            selectedMember = null;

            txtMemberId.Text = GenerateMemberId();

            txtFullName.Clear();
            txtPhone.Clear();
            txtFee.Clear();

            if (cboPackage.Items.Count > 0)
                cboPackage.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Today;

            dgvMembers.ClearSelection();
        }
    }
}
