using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingSystem.DataAccess.Concrete;
using BookingSystem.Entities;
using BookingSystem.UI.Annotations;
using BookingSystem.UI.Commands;

namespace BookingSystem.UI.ViewModels
{
    public class RouteViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Route _selectedRoute;
        public ObservableCollection<Route> Routes { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addRoute;
        private RelayCommand _removeRoute;

        public RelayCommand AddRouteCommand
        {
            get
            {
                return _addRoute ??
                       (_addRoute = new RelayCommand(obj =>
                       {
                           var route = new Route();
                           Routes.Insert(0, route);
                           _unitOfWork.RouteRepository.AddRoute(route);
                           SelectedRoute = route;
                       }));
            }
        }

        public RelayCommand RemoveRouteCommand
        {
            get
            {
                return _removeRoute ??
                       (_removeRoute = new RelayCommand(obj =>
                       {
                           var route = obj as Route;
                           if (route != null)
                           {
                               Routes.Remove(route);
                               _unitOfWork.RouteRepository.RemoveRoute(route);
                           }
                       },
                       obj => Routes.Count > 0));
            }
        }

        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));
            }
        }

        public RoutePoint DeparturePoint
        {
            get => _selectedRoute.RoutePoint;
            set
            {
                _selectedRoute.RoutePoint = value;
                OnPropertyChanged(nameof(DeparturePoint));
            }
        }

        public RoutePoint ArrivalPoint
        {
            get => _selectedRoute.RoutePoint1;
            set
            {
                _selectedRoute.RoutePoint1 = value;
                OnPropertyChanged(nameof(ArrivalPoint));
            }
        }

        public double? Length
        {
            get => _selectedRoute.Length;
            set
            {
                _selectedRoute.Length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
