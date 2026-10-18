
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bai2
{
    public class BookModel : INotifyPropertyChanged
    {
        private string _bookId = "";
        private string _bookName = "";
        private string _author = "";
        private int _quantity;
        private decimal _unitPrice;
        private string _coverPath = "";

        public string BookId
        {
            get { return _bookId; }
            set { _bookId = value; OnPropertyChanged(); }
        }

        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; OnPropertyChanged(); }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public string CoverPath
        {
            get { return _coverPath; }
            set { _coverPath = value; OnPropertyChanged(); }
        }

        public decimal TotalValue
        {
            get { return Quantity * UnitPrice; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
