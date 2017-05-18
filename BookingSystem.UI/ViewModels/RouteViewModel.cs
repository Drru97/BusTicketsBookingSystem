using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        private RoutePoint _rp1;
        private RoutePoint _rp2;

        private RelayCommand _addRoute;
        private RelayCommand _removeRoute;

        public RoutePoint Rp1
        {
            get => _rp1;
            set
            {
                _rp1 = value;
                OnPropertyChanged(nameof(Rp1));
            }
        }

        public RoutePoint Rp2
        {
            get => _rp2;
            set
            {
                _rp2 = value;
                OnPropertyChanged(nameof(Rp2));
            }
        }

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
                               SelectedRoute = Routes.FirstOrDefault();
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
            get => _selectedRoute?.Length;
            set
            {
                _selectedRoute.Length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        public IEnumerable<RoutePoint> RoutePoints
        {
            get
            {
                var routePoints = _unitOfWork.RoutePointRepository.RoutePoints.ToList();
                Rp1 = routePoints.FirstOrDefault();
                Rp2 = routePoints.FirstOrDefault();
                return routePoints;
            }
        }

        public RouteViewModel()
        {
            Routes = new ObservableCollection<Route>(_unitOfWork.RouteRepository.Routes);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
