﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private bool HasDependencies => SelectedRoutePoint.Route.Any() || SelectedRoutePoint.Route1.Any();
        public ObservableCollection<RoutePoint> RoutePoints { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands Implementation

        private RelayCommand _addRoutePointCommand;
        private RelayCommand _removeRoutePointRelayCommand;
        private RelayCommand _editRoutePointCommand;

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

        public RelayCommand RemoveRoutePointCommand
        {
            get
            {
                return _removeRoutePointRelayCommand ??
                       (_removeRoutePointRelayCommand = new RelayCommand(obj =>
                           {
                               var routePoint = obj as RoutePoint;
                               if (routePoint != null)
                               {
                                   RoutePoints.Remove(routePoint);
                                   _unitOfWork.RoutePointRepository.RemoveRoutePoint(routePoint);
                               }
                           },
                           obj => RoutePoints.Count > 0 && SelectedRoutePoint != null && !HasDependencies));
            }
        }

        public RelayCommand EditRoutePointCommand
        {
            get
            {
                return _editRoutePointCommand ??
                       (_editRoutePointCommand = new RelayCommand(obj => Edit(), obj => SelectedRoutePoint != null));
            }
        }

        #endregion

        #region Properties

        public RoutePoint SelectedRoutePoint
        {
            get { return _selectedRoutePoint; }
            set
            {
                _selectedRoutePoint = value;
                OnPropertyChanged(nameof(SelectedRoutePoint));
            }
        }

        public string Country
        {
            get { return _selectedRoutePoint.Country; }
            set
            {
                _selectedRoutePoint.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string State
        {
            get { return _selectedRoutePoint.State; }
            set
            {
                _selectedRoutePoint.State = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public string Region
        {
            get { return _selectedRoutePoint.Region; }
            set
            {
                _selectedRoutePoint.Region = value;
                OnPropertyChanged(nameof(Region));
            }
        }

        public string City
        {
            get { return _selectedRoutePoint.City; }
            set
            {
                _selectedRoutePoint.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        #endregion

        public RoutePointsViewModel()
        {
            RoutePoints = new ObservableCollection<RoutePoint>(_unitOfWork.RoutePointRepository.RoutePoints);
        }

        private void Edit()
        {
            _unitOfWork.RoutePointRepository.UpdateRoutePoint(SelectedRoutePoint);
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
