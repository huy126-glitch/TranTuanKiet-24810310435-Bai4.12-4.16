
namespace Bai2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuAdd;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripMenuItem menuSearch;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbAdd;

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalBooks;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalValue;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;

        private System.Windows.Forms.Label lblBookId;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Label lblCover;

        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtUnitPrice;

        private System.Windows.Forms.PictureBox picBookCover;
        private System.Windows.Forms.Button btnLoadCover;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Timer timer1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            menuStrip1 = new System.Windows.Forms.MenuStrip();
            menuFile = new System.Windows.Forms.ToolStripMenuItem();
            menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            menuSave = new System.Windows.Forms.ToolStripMenuItem();
            menuExit = new System.Windows.Forms.ToolStripMenuItem();
            menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            menuSearch = new System.Windows.Forms.ToolStripMenuItem();

            toolStrip1 = new System.Windows.Forms.ToolStrip();
            tsbOpen = new System.Windows.Forms.ToolStripButton();
            tsbSave = new System.Windows.Forms.ToolStripButton();
            tsbAdd = new System.Windows.Forms.ToolStripButton();

            statusStrip1 = new System.Windows.Forms.StatusStrip();
            lblTotalBooks = new System.Windows.Forms.ToolStripStatusLabel();
            lblTotalValue = new System.Windows.Forms.ToolStripStatusLabel();
            lblTime = new System.Windows.Forms.ToolStripStatusLabel();

            lblBookId = new System.Windows.Forms.Label();
            lblBookName = new System.Windows.Forms.Label();
            lblAuthor = new System.Windows.Forms.Label();
            lblQuantity = new System.Windows.Forms.Label();
            lblUnitPrice = new System.Windows.Forms.Label();
            lblCover = new System.Windows.Forms.Label();

            txtBookId = new System.Windows.Forms.TextBox();
            txtBookName = new System.Windows.Forms.TextBox();
            txtAuthor = new System.Windows.Forms.TextBox();
            txtQuantity = new System.Windows.Forms.TextBox();
            txtUnitPrice = new System.Windows.Forms.TextBox();

            picBookCover = new System.Windows.Forms.PictureBox();
            btnLoadCover = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnSearch = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();

            dgvBooks = new System.Windows.Forms.DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);

            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBookCover).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();

            // MenuStrip
            menuFile.Text = "File";
            menuOpen.Text = "Mở file CSV";
            menuSave.Text = "Lưu file CSV";
            menuExit.Text = "Thoát";

            menuFile.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuOpen, menuSave,
                    new System.Windows.Forms.ToolStripSeparator(),
                    menuExit
                });

            menuEdit.Text = "Edit";
            menuAdd.Text = "Thêm";
            menuDelete.Text = "Xóa";
            menuSearch.Text = "Tìm kiếm";

            menuEdit.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuAdd, menuDelete, menuSearch
                });

            menuStrip1.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuFile, menuEdit
                });

            menuOpen.Click += menuOpen_Click;
            menuSave.Click += menuSave_Click;
            menuExit.Click += menuExit_Click;
            menuAdd.Click += btnAdd_Click;
            menuDelete.Click += btnDelete_Click;
            menuSearch.Click += btnSearch_Click;

            // ToolStrip
            tsbOpen.Text = "Mở file";
            tsbSave.Text = "Lưu file";
            tsbAdd.Text = "Thêm mới";

            tsbOpen.Image = System.Drawing.SystemIcons.Application.ToBitmap();
            tsbSave.Image = System.Drawing.SystemIcons.Information.ToBitmap();
            tsbAdd.Image = System.Drawing.SystemIcons.Question.ToBitmap();

            tsbOpen.DisplayStyle =
                System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            tsbSave.DisplayStyle =
                System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            tsbAdd.DisplayStyle =
                System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;

            toolStrip1.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    tsbOpen, tsbSave, tsbAdd
                });

            tsbOpen.Click += menuOpen_Click;
            tsbSave.Click += menuSave_Click;
            tsbAdd.Click += btnAdd_Click;

            // StatusStrip
            lblTotalBooks.Text = "Tổng số đầu sách: 0";
            lblTotalValue.Text = "Tổng giá trị kho: 0";
            lblTime.Text = "";

            statusStrip1.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    lblTotalBooks, lblTotalValue, lblTime
                });

            lblTotalValue.Margin =
                new System.Windows.Forms.Padding(25, 3, 0, 2);

            lblTime.Spring = true;
            lblTime.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // Form
            Text = "Quản lý tài liệu sách và báo cáo nhà sách";
            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            ClientSize = new System.Drawing.Size(1100, 760);
            MinimumSize = new System.Drawing.Size(950, 650);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            MainMenuStrip = menuStrip1;

            // Labels
            lblBookId.Text = "Mã sách:";
            lblBookName.Text = "Tên sách:";
            lblAuthor.Text = "Tác giả:";
            lblQuantity.Text = "Số lượng:";
            lblUnitPrice.Text = "Đơn giá:";
            lblCover.Text = "Ảnh bìa:";

            System.Windows.Forms.Label[] labels =
            {
                lblBookId, lblBookName, lblAuthor,
                lblQuantity, lblUnitPrice, lblCover
            };

            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Location =
                    new System.Drawing.Point(25, 105 + i * 43);
                labels[i].Size =
                    new System.Drawing.Size(100, 28);
            }

            // TextBoxes
            txtBookId.Location = new System.Drawing.Point(125, 105);
            txtBookName.Location = new System.Drawing.Point(125, 148);
            txtAuthor.Location = new System.Drawing.Point(125, 191);
            txtQuantity.Location = new System.Drawing.Point(125, 234);
            txtUnitPrice.Location = new System.Drawing.Point(125, 277);

            txtBookId.Size = new System.Drawing.Size(240, 28);
            txtBookName.Size = new System.Drawing.Size(240, 28);
            txtAuthor.Size = new System.Drawing.Size(240, 28);
            txtQuantity.Size = new System.Drawing.Size(240, 28);
            txtUnitPrice.Size = new System.Drawing.Size(240, 28);

            txtBookId.ReadOnly = true;

            // PictureBox
            picBookCover.Location = new System.Drawing.Point(430, 105);
            picBookCover.Size = new System.Drawing.Size(170, 190);
            picBookCover.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            picBookCover.SizeMode =
                System.Windows.Forms.PictureBoxSizeMode.Zoom;

            btnLoadCover.Text = "Tải ảnh bìa";
            btnLoadCover.Location = new System.Drawing.Point(430, 303);
            btnLoadCover.Size = new System.Drawing.Size(170, 32);
            btnLoadCover.Click += btnLoadCover_Click;

            // Buttons
            btnAdd.Text = "Thêm mới";
            btnDelete.Text = "Xóa sách";
            btnSearch.Text = "Tìm kiếm";
            btnRefresh.Text = "Làm mới";

            btnAdd.Location = new System.Drawing.Point(640, 105);
            btnDelete.Location = new System.Drawing.Point(640, 148);
            btnSearch.Location = new System.Drawing.Point(640, 191);
            btnRefresh.Location = new System.Drawing.Point(640, 234);

            System.Windows.Forms.Button[] buttons =
            {
                btnAdd, btnDelete, btnSearch, btnRefresh
            };

            foreach (System.Windows.Forms.Button button in buttons)
            {
                button.Size = new System.Drawing.Size(125, 32);
                button.Cursor = System.Windows.Forms.Cursors.Hand;
            }

            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvBooks.Location = new System.Drawing.Point(25, 365);
            dgvBooks.Size = new System.Drawing.Size(1050, 330);
            dgvBooks.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.ReadOnly = true;
            dgvBooks.MultiSelect = false;
            dgvBooks.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.RowHeadersVisible = false;

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Mã sách",
                    DataPropertyName = "BookId",
                    Name = "colBookId"
                });

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Tên sách",
                    DataPropertyName = "BookName",
                    Name = "colBookName"
                });

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Tác giả",
                    DataPropertyName = "Author",
                    Name = "colAuthor"
                });

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Số lượng",
                    DataPropertyName = "Quantity",
                    Name = "colQuantity"
                });

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Đơn giá",
                    DataPropertyName = "UnitPrice",
                    Name = "colUnitPrice",
                    DefaultCellStyle =
                        new System.Windows.Forms.DataGridViewCellStyle
                        {
                            Format = "N0"
                        }
                });

            dgvBooks.Columns.Add(
                new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Ảnh bìa",
                    DataPropertyName = "CoverPath",
                    Name = "colCoverPath"
                });

            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;

            // Timer
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            Controls.Add(menuStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);

            Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblBookId, lblBookName, lblAuthor,
                lblQuantity, lblUnitPrice, lblCover,
                txtBookId, txtBookName, txtAuthor,
                txtQuantity, txtUnitPrice,
                picBookCover, btnLoadCover,
                btnAdd, btnDelete, btnSearch, btnRefresh,
                dgvBooks
            });

            menuStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            toolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            statusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;

            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBookCover).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
