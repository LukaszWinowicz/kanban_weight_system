using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfDashboardApp
{
    public class ReadDataViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ReadData> _readData;
        public ObservableCollection<ReadData> ReadData
        {
            get { return _readData; }
            set
            {
                _readData = value;
                OnPropertyChanged(nameof(ReadData));
            }
        }

        public ReadDataViewModel()
        {
            LoadReadData();
        }

        private void LoadReadData()
        {
            using (var context = new AppDbContext())
            {
                var data = context.ReadDatas.ToList();
                ReadData = new ObservableCollection<ReadData>(data);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
