
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bai2
{
    public partial class Form1 : Form
    {
        private BindingList<BookModel> books =
            new BindingList<BookModel>();

        private BindingSource bindingSource =
            new BindingSource();

        private int nextId = 1;
        private string currentCoverPath = "";

        public Form1()
        {
            InitializeComponent();

            bindingSource.DataSource = books;
            dgvBooks.DataSource = bindingSource;

            books.ListChanged += Books_ListChanged;

            txtBookId.Text = GenerateBookId();

            UpdateStatus();
        }

        // Sinh mã sách
        private string GenerateBookId()
        {
            return "S" + nextId.ToString("D3");
        }

        // Cập nhật thống kê
        private void Books_ListChanged(
            object sender, ListChangedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            lblTotalBooks.Text =
                "Tổng số đầu sách: " + books.Count;

            decimal total = books.Sum(
                book => book.TotalValue);

            lblTotalValue.Text =
                "Tổng giá trị kho: " +
                total.ToString("N0") + " VNĐ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text =
                "Thời gian: " +
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // Thêm sách
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtBookName.Text.Trim();
            string author = txtAuthor.Text.Trim();

            int quantity;
            decimal price;

            if (name == "")
            {
                MessageBox.Show("Vui lòng nhập tên sách.");
                txtBookName.Focus();
                return;
            }

            if (author == "")
            {
                MessageBox.Show("Vui lòng nhập tên tác giả.");
                txtAuthor.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity)
                || quantity <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.");
                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtUnitPrice.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out price) || price <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0.");
                txtUnitPrice.Focus();
                return;
            }

            BookModel book = new BookModel
            {
                BookId = GenerateBookId(),
                BookName = name,
                Author = author,
                Quantity = quantity,
                UnitPrice = price,
                CoverPath = currentCoverPath
            };

            books.Add(book);
            nextId++;

            bindingSource.ResetBindings(false);

            ClearForm();

            MessageBox.Show(
                "Thêm sách thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Xóa sách đang chọn
        private void btnDelete_Click(object sender, EventArgs e)
        {
            BookModel selected = GetSelectedBook();

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sách "
                + selected.BookName + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                books.Remove(selected);
                bindingSource.ResetBindings(false);
                ClearForm();
            }
        }

        // Lấy sách đang chọn
        private BookModel GetSelectedBook()
        {
            if (dgvBooks.CurrentRow == null)
                return null;

            return dgvBooks.CurrentRow.DataBoundItem as BookModel;
        }

        // Chọn dòng trên lưới
        private void dgvBooks_SelectionChanged(
            object sender, EventArgs e)
        {
            BookModel book = GetSelectedBook();

            if (book == null)
                return;

            txtBookId.Text = book.BookId;
            txtBookName.Text = book.BookName;
            txtAuthor.Text = book.Author;
            txtQuantity.Text = book.Quantity.ToString();
            txtUnitPrice.Text = book.UnitPrice.ToString(
                CultureInfo.CurrentCulture);

            currentCoverPath = book.CoverPath;
            ShowCover(currentCoverPath);
        }

        // Tải ảnh bìa
        private void btnLoadCover_Click(
            object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh bìa sách";
                dialog.Filter =
                    "Tệp ảnh (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentCoverPath = dialog.FileName;
                    ShowCover(currentCoverPath);
                }
            }
        }

        // Hiển thị ảnh an toàn
        private void ShowCover(string path)
        {
            if (picBookCover.Image != null)
            {
                System.Drawing.Image oldImage = picBookCover.Image;
                picBookCover.Image = null;
                oldImage.Dispose();
            }

            if (string.IsNullOrWhiteSpace(path) ||
                !File.Exists(path))
            {
                return;
            }

            try
            {
                using (FileStream stream = new FileStream(
                    path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image source =
                    System.Drawing.Image.FromStream(stream))
                {
                    picBookCover.Image =
                        new System.Drawing.Bitmap(source);
                }
            }
            catch
            {
                MessageBox.Show("Không thể mở ảnh bìa.");
            }
        }

        // Mở CSV
        private void menuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Mở danh sách sách";
                dialog.Filter = "CSV (*.csv)|*.csv";
                dialog.Multiselect = false;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string[] lines = File.ReadAllLines(
                        dialog.FileName, Encoding.UTF8);

                    BindingList<BookModel> imported =
                        new BindingList<BookModel>();

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i]))
                            continue;

                        string[] fields = ParseCsvLine(lines[i]);

                        if (fields.Length != 6)
                        {
                            throw new FormatException(
                                "Dữ liệu không hợp lệ tại dòng " +
                                (i + 1) + ".");
                        }

                        int quantity;
                        decimal price;

                        if (!int.TryParse(fields[3], out quantity) ||
                            !decimal.TryParse(
                                fields[4],
                                NumberStyles.Number,
                                CultureInfo.InvariantCulture,
                                out price))
                        {
                            throw new FormatException(
                                "Số lượng hoặc đơn giá không hợp lệ " +
                                "tại dòng " + (i + 1) + ".");
                        }

                        imported.Add(new BookModel
                        {
                            BookId = fields[0],
                            BookName = fields[1],
                            Author = fields[2],
                            Quantity = quantity,
                            UnitPrice = price,
                            CoverPath = fields[5]
                        });
                    }

                    books.ListChanged -= Books_ListChanged;

                    books.Clear();

                    foreach (BookModel book in imported)
                        books.Add(book);

                    books.ListChanged += Books_ListChanged;

                    nextId = 1;

                    foreach (BookModel book in books)
                    {
                        string number = book.BookId.Length > 1
                            ? book.BookId.Substring(1)
                            : "";

                        int id;

                        if (int.TryParse(number, out id) &&
                            id >= nextId)
                        {
                            nextId = id + 1;
                        }
                    }

                    bindingSource.ResetBindings(false);
                    UpdateStatus();
                    ClearForm();

                    MessageBox.Show(
                        "Mở CSV thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Không thể đọc file CSV.\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Lưu CSV
        private void menuSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Lưu danh sách sách";
                dialog.Filter = "CSV (*.csv)|*.csv";
                dialog.DefaultExt = "csv";
                dialog.AddExtension = true;
                dialog.FileName = "DanhSachSach.csv";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (StreamWriter writer = new StreamWriter(
                        dialog.FileName, false, new UTF8Encoding(true)))
                    {
                        writer.WriteLine(
                            "BookId,BookName,Author,Quantity,UnitPrice,CoverPath");

                        foreach (BookModel book in books)
                        {
                            string line = string.Join(",", new string[]
                            {
                                EscapeCsv(book.BookId),
                                EscapeCsv(book.BookName),
                                EscapeCsv(book.Author),
                                book.Quantity.ToString(
                                    CultureInfo.InvariantCulture),
                                book.UnitPrice.ToString(
                                    CultureInfo.InvariantCulture),
                                EscapeCsv(book.CoverPath)
                            });

                            writer.WriteLine(line);
                        }
                    }

                    MessageBox.Show(
                        "Lưu CSV thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Không thể lưu file.\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Đóng gói trường CSV
        private string EscapeCsv(string value)
        {
            if (value == null)
                value = "";

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        // Đọc một dòng CSV, có hỗ trợ dấu phẩy trong dấu nháy
        private string[] ParseCsvLine(string line)
        {
            System.Collections.Generic.List<string> fields =
                new System.Collections.Generic.List<string>();

            StringBuilder current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            if (inQuotes)
                throw new FormatException("Dòng CSV có dấu nháy chưa đóng.");

            fields.Add(current.ToString());

            return fields.ToArray();
        }

        // Tìm kiếm sách theo mã hoặc tên
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = ShowSearchDialog();

            if (keyword == null)
                return;

            keyword = keyword.Trim();

            if (keyword == "")
            {
                bindingSource.DataSource = books;
                return;
            }

            BindingList<BookModel> result =
                new BindingList<BookModel>(
                    books.Where(book =>
                        book.BookId.IndexOf(
                            keyword,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||
                        book.BookName.IndexOf(
                            keyword,
                            StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList());

            bindingSource.DataSource = result;
        }

        // Hộp thoại nhập từ khóa
        private string ShowSearchDialog()
        {
            using (Form dialog = new Form())
            using (Label label = new Label())
            using (TextBox input = new TextBox())
            using (Button ok = new Button())
            using (Button cancel = new Button())
            {
                dialog.Text = "Tìm kiếm sách";
                dialog.FormBorderStyle =
                    FormBorderStyle.FixedDialog;
                dialog.StartPosition =
                    FormStartPosition.CenterParent;
                dialog.ClientSize = new System.Drawing.Size(350, 125);
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;

                label.Text = "Nhập mã sách hoặc tên sách:";
                label.SetBounds(12, 12, 320, 23);

                input.SetBounds(12, 40, 325, 25);

                ok.Text = "Tìm";
                ok.DialogResult = DialogResult.OK;
                ok.SetBounds(165, 78, 80, 30);

                cancel.Text = "Hủy";
                cancel.DialogResult = DialogResult.Cancel;
                cancel.SetBounds(255, 78, 80, 30);

                dialog.Controls.AddRange(
                    new Control[] { label, input, ok, cancel });

                dialog.AcceptButton = ok;
                dialog.CancelButton = cancel;

                return dialog.ShowDialog(this) == DialogResult.OK
                    ? input.Text
                    : null;
            }
        }

        // Làm mới
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.DataSource = books;
            ClearForm();
        }

        private void ClearForm()
        {
            txtBookId.Text = GenerateBookId();
            txtBookName.Clear();
            txtAuthor.Clear();
            txtQuantity.Clear();
            txtUnitPrice.Clear();

            currentCoverPath = "";
            ShowCover("");

            dgvBooks.ClearSelection();
        }

        // Thoát chương trình
        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer1.Stop();

            if (picBookCover.Image != null)
            {
                picBookCover.Image.Dispose();
                picBookCover.Image = null;
            }

            base.OnFormClosed(e);
        }
    }
}
