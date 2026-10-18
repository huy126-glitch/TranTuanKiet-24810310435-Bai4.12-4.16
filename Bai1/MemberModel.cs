
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bai1
{
    public class MemberModel : INotifyPropertyChanged
    {
        private string _memberId = "";
        private string _fullName = "";
        private string _phone = "";
        private string _package = "";
        private DateTime _startDate = DateTime.Today;
        private decimal _fee;

        public string MemberId
        {
            get { return _memberId; }
            set
            {
                _memberId = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string Package
        {
            get { return _package; }
            set
            {
                _package = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public decimal Fee
        {
            get { return _fee; }
            set
            {
                _fee = value;
                OnPropertyChanged();
            }
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
