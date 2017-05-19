﻿using System;
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
    public class JourneyViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Journey _selectedJourney;
        public ObservableCollection<Journey> Journeys { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addJourneyCommand;
        private RelayCommand _removeJourneyCommand;

        public RelayCommand AddJourneyCommand
        {
            get
            {
                return _addJourneyCommand ??
                       (_addJourneyCommand = new RelayCommand(obj =>
                       {
                           var journey = new Journey();
                           Journeys.Insert(0, journey);
                           _unitOfWork.JourneyRepository.AddJourney(journey);
                           SelectedJourney = journey;
                       }));
            }
        }

        public RelayCommand RemoveJourneyCommand
        {
            get
            {
                return _removeJourneyCommand ??
                       (_removeJourneyCommand = new RelayCommand(obj =>
                           {
                               var journey = obj as Journey;
                               if (journey != null)
                               {
                                   Journeys.Remove(journey);
                                   _unitOfWork.JourneyRepository.RemoveJourney(journey);
                                   SelectedJourney = Journeys.FirstOrDefault();
                               }
                           },
                           obj => Journeys.Count > 0
                       ));
            }
        }

        public Journey SelectedJourney
        {
            get => _selectedJourney;
            set
            {
                _selectedJourney = value;
                OnPropertyChanged(nameof(SelectedJourney));
            }
        }

        public DateTime? DepartureDateTime
        {
            get => SelectedJourney.DepartureTime;
            set
            {
                SelectedJourney.DepartureTime = value;
                OnPropertyChanged(nameof(DepartureDateTime));
            }
        }

        public DateTime? ArrivalDateTime
        {
            get => SelectedJourney.ArrivalTime;
            set
            {
                SelectedJourney.ArrivalTime = value;
                OnPropertyChanged(nameof(ArrivalDateTime));
            }
        }

        public IEnumerable<Route> Routes
        {
            get
            {
                var routes = _unitOfWork.RouteRepository.Routes.ToList();
                return routes;
            }
        }

        public IEnumerable<Bus> Buses
        {
            get
            {
                var buses = _unitOfWork.BusRepository.Buses.ToList();
                return buses;
            }
        }

        public IEnumerable<Driver> Drivers
        {
            get
            {
                var drivers = _unitOfWork.DriverRepository.Drivers.ToList();
                return drivers;
            }
        }

        public JourneyViewModel()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
            SelectedJourney = _unitOfWork.JourneyRepository.Journeys.FirstOrDefault();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}