using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingSystem.DataAccess.Concrete;
using BookingSystem.Entities;
using BookingSystem.UI.Annotations;
using BookingSystem.UI.Commands;

namespace BookingSystem.UI.ViewModels
{
    public class DriverViewModel : INotifyPropertyChanged
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Driver _selectedDriver;
        public ObservableCollection<Driver> Drivers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addDriverCommand;
        private RelayCommand _removeDriverCommand;

        private string _newDriverFirstName;
        private string _newDriverLastName;

        public RelayCommand AddDriverCommand
        {
            get
            {
                return _addDriverCommand ??
                       (_addDriverCommand = new RelayCommand(obj =>
                       {
                           var driver = new Driver
                           {
                               FirstName = _newDriverFirstName,
                               LastName = _newDriverLastName
                           };
                           Drivers.Insert(0, driver);
                           _unitOfWork.DriverRepository.AddDriver(driver);
                           SelectedDriver = driver;
                       }));
            }
        }

        public RelayCommand RemoveDriverCommand
        {
            get
            {
                return _removeDriverCommand ??
                       (_removeDriverCommand = new RelayCommand(obj =>
                           {
                               var driver = obj as Driver;
                               if (driver != null)
                               {
                                   Drivers.Remove(driver);
                                   _unitOfWork.DriverRepository.RemoveDriver(driver);
                               }
                           },
                           obj => Drivers.Count > 0));
            }
        }

        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set
            {
                _selectedDriver = value;
                OnPropertyChanged(nameof(SelectedDriver));
            }
        }

        public string FirstName
        {
            get => SelectedDriver.FirstName;
            set
            {
                SelectedDriver.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => SelectedDriver.LastName;
            set
            {
                SelectedDriver.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public DateTime? Birthdate
        {
            get => SelectedDriver.Birthdate;
            set
            {
                SelectedDriver.Birthdate = value;
                OnPropertyChanged(nameof(Birthdate));
            }
        }

        public DriverViewModel()
        {
            Drivers = new ObservableCollection<Driver>(_unitOfWork.DriverRepository.Drivers);
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
