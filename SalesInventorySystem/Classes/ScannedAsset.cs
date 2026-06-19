using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class ScannedAsset : INotifyPropertyChanged
    {
        private string _assetName = "Pending...";
        private string _status = "Scanning";

        public string EPC { get; set; }

        public string AssetName
        {
            get => _assetName;
            set { _assetName = value; OnPropertyChanged(nameof(AssetName)); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
