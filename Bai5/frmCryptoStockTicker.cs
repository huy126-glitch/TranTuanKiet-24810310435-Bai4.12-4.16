
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai5
{
    public class frmCryptoStockTicker : Form
    {
        private Button btnStart = new Button();
        private Button btnPause = new Button();

        private Label lblBTCPrice = new Label();
        private Label lblTitle = new Label();

        private DataGridView dgvHistoryLog = new DataGridView();

        private StatusStrip statusStrip = new StatusStrip();
        private ToolStripStatusLabel lblLastUpdate =
            new ToolStripStatusLabel("Lần cập nhật cuối: --:--:--");
        private ToolStripStatusLabel lblSyncCount =
            new ToolStripStatusLabel("Số lượt đồng bộ: 0");

        private BindingList<PriceHistory> history =
            new BindingList<PriceHistory>();

        private CancellationTokenSource cts;
        private Task monitorTask;

        private decimal currentPrice = 65000m;
        private int syncCount = 0;
        private bool isRunning = false;

        public frmCryptoStockTicker()
        {
            TaoGiaoDien();
            HienThiGiaBanDau();
        }

        private void TaoGiaoDien()
        {
            Text = "GIÁM SÁT TỶ GIÁ TÀI CHÍNH THỜI GIAN THỰC";
            Size = new Size(1000, 650);
            MinimumSize = new Size(800, 450);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitle.Text = "GIÁ BTC/USD THỜI GIAN THỰC";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 55;
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            Panel pnlControl = new Panel();
            pnlControl.Dock = DockStyle.Top;
            pnlControl.Height = 65;

            btnStart.Text = "Bắt đầu giám sát";
            btnStart.Location = new Point(20, 15);
            btnStart.Size = new Size(170, 35);
            btnStart.Click += async (s, e) => await BatDauGiamSat();

            btnPause.Text = "Tạm dừng";
            btnPause.Location = new Point(205, 15);
            btnPause.Size = new Size(130, 35);
            btnPause.Enabled = false;
            btnPause.Click += async (s, e) => await TamDung();

            pnlControl.Controls.Add(btnStart);
            pnlControl.Controls.Add(btnPause);

            Panel pnlPrice = new Panel();
            pnlPrice.Dock = DockStyle.Top;
            pnlPrice.Height = 110;
            pnlPrice.BackColor = Color.WhiteSmoke;

            Label lblBTC = new Label();
            lblBTC.Text = "Bitcoin (BTC/USD)";
            lblBTC.Dock = DockStyle.Top;
            lblBTC.Height = 35;
            lblBTC.Font = new Font("Arial", 12, FontStyle.Bold);
            lblBTC.TextAlign = ContentAlignment.MiddleCenter;

            lblBTCPrice.Dock = DockStyle.Fill;
            lblBTCPrice.Font = new Font("Arial", 25, FontStyle.Bold);
            lblBTCPrice.TextAlign = ContentAlignment.MiddleCenter;

            pnlPrice.Controls.Add(lblBTCPrice);
            pnlPrice.Controls.Add(lblBTC);

            GroupBox grpHistory = new GroupBox();
            grpHistory.Text = "Lịch sử biến động giá";
            grpHistory.Dock = DockStyle.Fill;
            grpHistory.Padding = new Padding(8);

            dgvHistoryLog.Dock = DockStyle.Fill;
            dgvHistoryLog.ReadOnly = true;
            dgvHistoryLog.AllowUserToAddRows = false;
            dgvHistoryLog.AllowUserToDeleteRows = false;
            dgvHistoryLog.AutoGenerateColumns = true;
            dgvHistoryLog.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistoryLog.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvHistoryLog.MultiSelect = false;

            dgvHistoryLog.DataSource = history;
            grpHistory.Controls.Add(dgvHistoryLog);

            statusStrip.Items.Add(lblLastUpdate);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblSyncCount);

            Controls.Add(grpHistory);
            Controls.Add(pnlPrice);
            Controls.Add(pnlControl);
            Controls.Add(lblTitle);
            Controls.Add(statusStrip);
        }

        private void HienThiGiaBanDau()
        {
            lblBTCPrice.Text = currentPrice.ToString("N2") + " USD";
            lblBTCPrice.ForeColor = Color.Green;
        }

        private async Task BatDauGiamSat()
        {
            if (isRunning)
                return;

            isRunning = true;

            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            btnStart.Enabled = false;
            btnPause.Enabled = true;

            lblLastUpdate.Text = "Đang chờ cập nhật...";

            monitorTask = Task.Run(async () =>
            {
                Random random = new Random();

                try
                {
                    while (true)
                    {
                        await Task.Delay(2000, token);

                        token.ThrowIfCancellationRequested();

                        decimal change = (decimal)(random.NextDouble() * 400 - 200);
                        decimal newPrice = currentPrice + change;

                        if (newPrice <= 0)
                            newPrice = currentPrice;

                        token.ThrowIfCancellationRequested();

                        CapNhatGiaoDien(newPrice);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Vòng lặp kết thúc khi người dùng tạm dừng.
                }
            }, token);

            await monitorTask;

            if (cts != null)
            {
                cts.Dispose();
                cts = null;
            }

            monitorTask = null;
            isRunning = false;

            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }

        private async Task TamDung()
        {
            if (cts == null || !isRunning)
                return;

            btnPause.Enabled = false;

            cts.Cancel();

            if (monitorTask != null)
                await monitorTask;

            lblLastUpdate.Text = "Đã tạm dừng";
        }

        private void CapNhatGiaoDien(decimal newPrice)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => CapNhatGiaoDien(newPrice)));
                return;
            }

            decimal oldPrice = currentPrice;
            currentPrice = newPrice;
            syncCount++;

            if (newPrice > oldPrice)
            {
                lblBTCPrice.ForeColor = Color.Green;
                lblBTCPrice.Text = newPrice.ToString("N2") + " USD  ▲";
            }
            else if (newPrice < oldPrice)
            {
                lblBTCPrice.ForeColor = Color.Red;
                lblBTCPrice.Text = newPrice.ToString("N2") + " USD  ▼";
            }
            else
            {
                lblBTCPrice.ForeColor = Color.Black;
                lblBTCPrice.Text = newPrice.ToString("N2") + " USD";
            }

            history.Insert(0, new PriceHistory
            {
                UpdateTime = DateTime.Now,
                Price = newPrice,
                Change = newPrice - oldPrice,
                Direction = newPrice > oldPrice
                    ? "Tăng"
                    : newPrice < oldPrice ? "Giảm" : "Không đổi"
            });

            // Giữ tối đa 100 dòng lịch sử.
            if (history.Count > 100)
                history.RemoveAt(history.Count - 1);

            lblLastUpdate.Text =
                "Lần cập nhật cuối: " +
                DateTime.Now.ToString("HH:mm:ss");

            lblSyncCount.Text =
                "Số lượt đồng bộ: " + syncCount;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (cts != null)
                cts.Cancel();

            base.OnFormClosing(e);
        }
    }

    public class PriceHistory
    {
        public DateTime UpdateTime { get; set; }

        public decimal Price { get; set; }

        public decimal Change { get; set; }

        public string Direction { get; set; } = "";
    }
}
