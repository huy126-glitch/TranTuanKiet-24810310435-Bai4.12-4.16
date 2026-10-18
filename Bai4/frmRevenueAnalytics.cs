
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4
{
    public class frmRevenueAnalytics : Form
    {
        private DateTimePicker dtpFromDate = new DateTimePicker();
        private DateTimePicker dtpToDate = new DateTimePicker();
        private ComboBox cboBranch = new ComboBox();

        private Button btnStatistics = new Button();
        private Button btnCancel = new Button();

        private ProgressBar pgbLoading = new ProgressBar();
        private Label lblProcessingStatus = new Label();

        private DataGridView dgvRevenueData = new DataGridView();

        private CancellationTokenSource cts;

        public frmRevenueAnalytics()
        {
            TaoGiaoDien();
            NapDuLieuChiNhanh();
        }

        private void TaoGiaoDien()
        {
            Text = "BẢNG ĐIỀU KHIỂN THỐNG KÊ DOANH THU";
            Size = new Size(1050, 650);
            MinimumSize = new Size(850, 500);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "THỐNG KÊ DOANH THU";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 55;

            GroupBox grpCriteria = new GroupBox();
            grpCriteria.Text = "Tiêu chí thống kê";
            grpCriteria.Dock = DockStyle.Top;
            grpCriteria.Height = 125;

            Label lblFrom = new Label();
            lblFrom.Text = "Từ ngày:";
            lblFrom.Location = new Point(20, 30);
            lblFrom.AutoSize = true;

            dtpFromDate.Location = new Point(85, 26);
            dtpFromDate.Width = 150;
            dtpFromDate.Format = DateTimePickerFormat.Short;

            Label lblTo = new Label();
            lblTo.Text = "Đến ngày:";
            lblTo.Location = new Point(260, 30);
            lblTo.AutoSize = true;

            dtpToDate.Location = new Point(330, 26);
            dtpToDate.Width = 150;
            dtpToDate.Format = DateTimePickerFormat.Short;
            dtpToDate.Value = DateTime.Today;

            Label lblBranch = new Label();
            lblBranch.Text = "Chi nhánh:";
            lblBranch.Location = new Point(505, 30);
            lblBranch.AutoSize = true;

            cboBranch.Location = new Point(580, 26);
            cboBranch.Width = 180;
            cboBranch.DropDownStyle = ComboBoxStyle.DropDownList;

            btnStatistics.Text = "Thống kê doanh thu";
            btnStatistics.Location = new Point(85, 70);
            btnStatistics.Size = new Size(170, 35);
            btnStatistics.Click += async (s, e) =>
                await ThongKeDoanhThu();

            btnCancel.Text = "Hủy thao tác";
            btnCancel.Location = new Point(270, 70);
            btnCancel.Size = new Size(130, 35);
            btnCancel.Enabled = false;
            btnCancel.Click += (s, e) => HuyThaoTac();

            grpCriteria.Controls.Add(lblFrom);
            grpCriteria.Controls.Add(dtpFromDate);
            grpCriteria.Controls.Add(lblTo);
            grpCriteria.Controls.Add(dtpToDate);
            grpCriteria.Controls.Add(lblBranch);
            grpCriteria.Controls.Add(cboBranch);
            grpCriteria.Controls.Add(btnStatistics);
            grpCriteria.Controls.Add(btnCancel);

            Panel pnlStatus = new Panel();
            pnlStatus.Dock = DockStyle.Top;
            pnlStatus.Height = 65;
            pnlStatus.Padding = new Padding(10);

            lblProcessingStatus.Text = "Sẵn sàng thống kê";
            lblProcessingStatus.Dock = DockStyle.Top;
            lblProcessingStatus.Height = 25;

            pgbLoading.Dock = DockStyle.Bottom;
            pgbLoading.Height = 20;
            pgbLoading.Minimum = 0;
            pgbLoading.Maximum = 100;
            pgbLoading.Value = 0;

            pnlStatus.Controls.Add(lblProcessingStatus);
            pnlStatus.Controls.Add(pgbLoading);

            GroupBox grpResult = new GroupBox();
            grpResult.Text = "Chi tiết giao dịch doanh thu";
            grpResult.Dock = DockStyle.Fill;
            grpResult.Padding = new Padding(8);

            dgvRevenueData.Dock = DockStyle.Fill;
            dgvRevenueData.ReadOnly = true;
            dgvRevenueData.AllowUserToAddRows = false;
            dgvRevenueData.AllowUserToDeleteRows = false;
            dgvRevenueData.AutoGenerateColumns = true;
            dgvRevenueData.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvRevenueData.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvRevenueData.MultiSelect = false;

            grpResult.Controls.Add(dgvRevenueData);

            Controls.Add(grpResult);
            Controls.Add(pnlStatus);
            Controls.Add(grpCriteria);
            Controls.Add(lblTitle);
        }

        private void NapDuLieuChiNhanh()
        {
            cboBranch.Items.Add("Tất cả chi nhánh");
            cboBranch.Items.Add("Chi nhánh Hà Nội");
            cboBranch.Items.Add("Chi nhánh Hải Phòng");
            cboBranch.Items.Add("Chi nhánh Đà Nẵng");

            cboBranch.SelectedIndex = 0;

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
        }

        private async Task ThongKeDoanhThu()
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show(
                    "Ngày bắt đầu không được lớn hơn ngày kết thúc!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cts != null)
            {
                return;
            }

            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            btnStatistics.Enabled = false;
            btnCancel.Enabled = true;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
            cboBranch.Enabled = false;

            pgbLoading.Value = 0;
            lblProcessingStatus.Text = "Đang truy xuất và tổng hợp dữ liệu...";

            try
            {
                IProgress<int> progress = new Progress<int>(value =>
                {
                    pgbLoading.Value = Math.Max(
                        pgbLoading.Minimum,
                        Math.Min(value, pgbLoading.Maximum));

                    lblProcessingStatus.Text =
                        "Đang xử lý... " + value + "%";
                });

                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;
                string branch = cboBranch.SelectedItem.ToString();

                List<RevenueInfo> result = await Task.Run(async () =>
                {
                    List<RevenueInfo> data =
                        new List<RevenueInfo>();

                    // Giả lập công việc kéo dài khoảng 5 giây.
                    for (int i = 1; i <= 50; i++)
                    {
                        token.ThrowIfCancellationRequested();

                        await Task.Delay(100, token);

                        progress.Report(i * 2);
                    }

                    token.ThrowIfCancellationRequested();

                    Random random = new Random(12345);

                    for (int i = 1; i <= 30; i++)
                    {
                        token.ThrowIfCancellationRequested();

                        DateTime date = fromDate.AddDays(
                            random.Next(
                                (toDate - fromDate).Days + 1));

                        string selectedBranch;

                        if (branch == "Tất cả chi nhánh")
                        {
                            string[] branches =
                            {
                                "Chi nhánh Hà Nội",
                                "Chi nhánh Hải Phòng",
                                "Chi nhánh Đà Nẵng"
                            };

                            selectedBranch = branches[
                                random.Next(branches.Length)];
                        }
                        else
                        {
                            selectedBranch = branch;
                        }

                        data.Add(new RevenueInfo
                        {
                            TransactionId = "GD" +
                                i.ToString("D3"),
                            TransactionDate = date,
                            Branch = selectedBranch,
                            Description = "Giao dịch bán hàng",
                            Amount = random.Next(100, 10000) * 1000m
                        });
                    }

                    token.ThrowIfCancellationRequested();

                    return data;
                }, token);

                dgvRevenueData.DataSource = new BindingList<RevenueInfo>(
                    result);

                pgbLoading.Value = 100;
                lblProcessingStatus.Text =
                    "Thống kê hoàn tất! Tổng số giao dịch: " +
                    result.Count;

                MessageBox.Show(
                    "Thống kê doanh thu thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                pgbLoading.Value = 0;
                lblProcessingStatus.Text = "Đã hủy thao tác!";

                MessageBox.Show(
                    "Đã hủy thao tác!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                pgbLoading.Value = 0;
                lblProcessingStatus.Text = "Có lỗi xảy ra";

                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cts.Dispose();
                cts = null;

                btnStatistics.Enabled = true;
                btnCancel.Enabled = false;

                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                cboBranch.Enabled = true;
            }
        }

        private void HuyThaoTac()
        {
            if (cts != null)
            {
                lblProcessingStatus.Text = "Đang hủy thao tác...";
                btnCancel.Enabled = false;
                cts.Cancel();
            }
        }
    }

    public class RevenueInfo
    {
        public string TransactionId { get; set; } = "";
        public DateTime TransactionDate { get; set; }
        public string Branch { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
