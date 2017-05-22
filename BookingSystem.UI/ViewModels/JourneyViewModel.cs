using System;
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
        private bool HasDependencies => SelectedJourney.Ticket.Any();
        public ObservableCollection<Journey> Journeys { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands Implementation

        private RelayCommand _addJourneyCommand;
        private RelayCommand _removeJourneyCommand;
        private RelayCommand _editJourneyCommand;

        public RelayCommand AddJourneyCommand
        {
            get
            {
                return _addJourneyCommand ??
                       (_addJourneyCommand = new RelayCommand(obj =>
                       {
                           var journey = new Journey
                           {
                               Route = SelectedJourney?.Route ?? _unitOfWork.RouteRepository.Routes.FirstOrDefault(),
                               Driver = SelectedJourney?.Driver ?? _unitOfWork.DriverRepository.Drivers.FirstOrDefault(),
                               Bus = SelectedJourney?.Bus ?? _unitOfWork.BusRepository.Buses.FirstOrDefault(),
                               DepartureTime = SelectedJourney?.DepartureTime ?? DateTime.Now,
                               ArrivalTime = SelectedJourney?.ArrivalTime ?? DateTime.Now
                           };
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
                           obj => Journeys.Count > 0 && SelectedJourney != null && !HasDependencies));
            }
        }

        public RelayCommand EditJourneyCommand
        {
            get
            {
                return _editJourneyCommand ??
                       (_editJourneyCommand = new RelayCommand(obj => Edit(), obj => SelectedJourney != null));
            }
        }

        #endregion

        #region Properties

        public Journey SelectedJourney
        {
            get { return _selectedJourney; }
            set
            {
                _selectedJourney = value;
                OnPropertyChanged(nameof(SelectedJourney));
            }
        }

        public DateTime? DepartureDateTime
        {
            get { return SelectedJourney.DepartureTime; }
            set
            {
                SelectedJourney.DepartureTime = value;
                OnPropertyChanged(nameof(DepartureDateTime));
            }
        }

        public DateTime? ArrivalDateTime
        {
            get { return SelectedJourney.ArrivalTime; }
            set
            {
                SelectedJourney.ArrivalTime = value;
                OnPropertyChanged(nameof(ArrivalDateTime));
            }
        }

        public IEnumerable<Route> Routes => _unitOfWork.RouteRepository.Routes.ToList();

        public IEnumerable<Bus> Buses => _unitOfWork.BusRepository.Buses.ToList();

        public IEnumerable<Driver> Drivers => _unitOfWork.DriverRepository.Drivers.ToList();

        #endregion

        public JourneyViewModel()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
        }

        private void Edit()
        {
            _unitOfWork.JourneyRepository.UpdateJourney(SelectedJourney);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
