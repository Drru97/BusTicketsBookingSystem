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
        private bool HasDependencies => SelectedRoute.Journey.Any();
        public ObservableCollection<Route> Routes { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands Implementation

        private RelayCommand _addRouteCommand;
        private RelayCommand _removeRouteCommand;
        private RelayCommand _editRouteCommand;

        public RelayCommand AddRouteCommand
        {
            get
            {
                return _addRouteCommand ??
                       (_addRouteCommand = new RelayCommand(obj =>
                       {
                           var route = new Route
                           {
                               RoutePoint = SelectedRoute?.RoutePoint ?? _unitOfWork.RoutePointRepository.RoutePoints.FirstOrDefault(),
                               RoutePoint1 = SelectedRoute?.RoutePoint1 ?? _unitOfWork.RoutePointRepository.RoutePoints.FirstOrDefault(),
                               Length = default(int)
                           };
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
                return _removeRouteCommand ??
                       (_removeRouteCommand = new RelayCommand(obj =>
                       {
                           var route = obj as Route;
                           if (route != null)
                           {
                               Routes.Remove(route);
                               _unitOfWork.RouteRepository.RemoveRoute(route);
                               SelectedRoute = Routes.FirstOrDefault();
                           }
                       },
                       obj => Routes.Count > 0 && SelectedRoute != null && !HasDependencies));
            }
        }

        public RelayCommand EditRouteCommand
        {
            get
            {
                return _editRouteCommand ??
                       (_editRouteCommand = new RelayCommand(obj => Edit(), obj => SelectedRoute != null && SelectedRoute.Length > 0));
            }
        }

        #endregion

        #region Properties

        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            set
            {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));
            }
        }

        public RoutePoint DeparturePoint
        {
            get { return _selectedRoute.RoutePoint; }
            set
            {
                _selectedRoute.RoutePoint = value;
                OnPropertyChanged(nameof(DeparturePoint));
            }
        }

        public RoutePoint ArrivalPoint
        {
            get { return _selectedRoute.RoutePoint1; }
            set
            {
                _selectedRoute.RoutePoint1 = value;
                OnPropertyChanged(nameof(ArrivalPoint));
            }
        }

        public double? Length
        {
            get { return _selectedRoute?.Length; }
            set
            {
                _selectedRoute.Length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        public IEnumerable<RoutePoint> RoutePoints => _unitOfWork.RoutePointRepository.RoutePoints.ToList();

        #endregion

        public RouteViewModel()
        {
            Routes = new ObservableCollection<Route>(_unitOfWork.RouteRepository.Routes);
        }

        private void Edit()
        {
            _unitOfWork.RouteRepository.UpdateRoute(SelectedRoute);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
