using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesseractDemo.ClassAndInterface
{
    class BusyIndicator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {  _isBusy = value;
                    PropertyChanged?.Invoke(this,
                                            new PropertyChangedEventArgs("IsBusy"));

                }
            }
        }

        public BusyIndicator()
        {
            _isBusy = false;
        }
          

    }
}
