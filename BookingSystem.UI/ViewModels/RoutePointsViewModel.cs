using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingSystem.DataAccess.Concrete;
using BookingSystem.Entities;
using BookingSystem.UI.Annotations;
using BookingSystem.UI.Commands;

namespace BookingSystem.UI.ViewModels
{
    public class RoutePointsViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private RoutePoint _selectedRoutePoint;
        public ObservableCollection<RoutePoint> RoutePoints { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addRoutePointCommand;

        public RelayCommand AddRoutePointCommand
        {
            get
            {
                return _addRoutePointCommand ??
                       (_addRoutePointCommand = new RelayCommand(obj =>
                       {
                           var routePoint = new RoutePoint();
                           RoutePoints.Insert(0, routePoint);
                           _unitOfWork.RoutePointRepository.AddRoutePoint(routePoint);
                           SelectedRoutePoint = routePoint;
                       }));
            }
        }

        public RoutePoint SelectedRoutePoint
        {
            get => _selectedRoutePoint;
            set
            {
                _selectedRoutePoint = value;
                OnPropertyChanged(nameof(SelectedRoutePoint));
            }
        }

        public string Country
        {
            get => _selectedRoutePoint.Country;
            set
            {
                _selectedRoutePoint.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string State
        {
            get => _selectedRoutePoint.State;
            set
            {
                _selectedRoutePoint.State = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public string Region
        {
            get => _selectedRoutePoint.Region;
            set
            {
                _selectedRoutePoint.Region = value;
                OnPropertyChanged(nameof(Region));
            }
        }

        public string City
        {
            get => _selectedRoutePoint.City;
            set
            {
                _selectedRoutePoint.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public RoutePointsViewModel()
        {
            RoutePoints = new ObservableCollection<RoutePoint>(_unitOfWork.RoutePointRepository.RoutePoints);
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
