using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingSystem.DataAccess.Concrete;
using BookingSystem.Entities;
using BookingSystem.UI.Annotations;
using BookingSystem.UI.Commands;

namespace BookingSystem.UI.ViewModels
{
    public class BusViewModel : INotifyPropertyChanged
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Bus _selectedBus;
        public ObservableCollection<Bus> Buses { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addBusCommand;

        public RelayCommand AddBusCommand
        {
            get
            {
                return _addBusCommand ??
                       (_addBusCommand = new RelayCommand(obj =>
                       {
                           var bus = new Bus();
                           Buses.Insert(0, bus);
                           _unitOfWork.BusRepository.AddBus(bus);
                           SelectedBus = bus;
                       }));
            }
        }

        public Bus SelectedBus
        {
            get => _selectedBus;
            set
            {
                _selectedBus = value;
                OnPropertyChanged(nameof(SelectedBus));
            }
        }

        public string Model
        {
            get => _selectedBus.Model;
            set
            {
                _selectedBus.Model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string AutomoblieNumber
        {
            get => _selectedBus.AutomobileNumber;
            set
            {
                _selectedBus.AutomobileNumber = value;
                OnPropertyChanged(nameof(AutomoblieNumber));
            }
        }

        public string Vin
        {
            get => _selectedBus.Vin;
            set
            {
                _selectedBus.Vin = value;
                OnPropertyChanged(nameof(Vin));
            }
        }

        public int? PassengersCount
        {
            get => _selectedBus.PassengersCount;
            set
            {
                _selectedBus.PassengersCount = value;
                OnPropertyChanged(nameof(PassengersCount));
            }
        }

        public BusViewModel()
        {
            Buses = new ObservableCollection<Bus>(_unitOfWork.BusRepository.Buses);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
