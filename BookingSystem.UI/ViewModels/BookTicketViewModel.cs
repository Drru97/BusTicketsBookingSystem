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
    public class BookTicketViewModel : INotifyPropertyChanged
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Journey _selectedJourney;
        private ObservableCollection<Journey> _journeys = new ObservableCollection<Journey>();
        public IEnumerable<RoutePoint> RoutePoints => _unitOfWork.RoutePointRepository.RoutePoints.ToList();

        public event PropertyChangedEventHandler PropertyChanged;
        private bool PassengerInfoError => string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName);

        private string _firstName;
        private string _lastName;

        private RoutePoint _departurePoint;
        private RoutePoint _arrivalPoint;
        private DateTime? _selectedDate;

        #region Commands Implementation

        private RelayCommand _bookTicketCommand;
        private RelayCommand _buyTicketCommand;
        private RelayCommand _filterCommand;
        private RelayCommand _resetCommand;

        public RelayCommand BookTicketCommand
        {
            get
            {
                return _bookTicketCommand ??
                       (_bookTicketCommand = new RelayCommand(obj =>
                       {
                           CreateTicket();
                       },
                       obj => !PassengerInfoError));
            }
        }

        public RelayCommand BuyTicketCommand
        {
            get
            {
                return _buyTicketCommand ??
                       (_buyTicketCommand = new RelayCommand(obj =>
                       {
                           CreateTicket();
                       },
                       obj => !PassengerInfoError));
            }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return _filterCommand ??
                       (_filterCommand = new RelayCommand(obj => Filter(DerarturePoint, ArrivalPoint, SelectedDate)));
            }
        }

        public RelayCommand ResetCommand
        {
            get
            {
                return _resetCommand ??
                       (_resetCommand = new RelayCommand(obj => Reset()));
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Journey> Journeys
        {
            get => _journeys;
            set
            {
                _journeys = value;
                OnPropertyChanged(nameof(Journeys));
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public RoutePoint DerarturePoint
        {
            get => _departurePoint;
            set
            {
                _departurePoint = value;
                OnPropertyChanged(nameof(DerarturePoint));
            }
        }

        public RoutePoint ArrivalPoint
        {
            get => _arrivalPoint;
            set
            {
                _arrivalPoint = value;
                OnPropertyChanged(nameof(ArrivalPoint));
            }
        }

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        #endregion

        public BookTicketViewModel()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
            SelectedJourney = Journeys.FirstOrDefault();
        }

        #region Methods

        private void CreateTicket()
        {
            var passenger = new Passenger
            {
                FirstName = FirstName,
                LastName = LastName
            };

            var ticket = new Ticket
            {
                Journey = SelectedJourney,
                Passenger = passenger,
                Price = default(decimal),
                Seat = default(int),
                PurchaseDateTime = DateTime.Now
            };

            _unitOfWork.PassengerRepository.AddPassenger(passenger);
            _unitOfWork.TicketRepository.AddTicket(ticket);
        }

        private void Filter(RoutePoint depart, RoutePoint arrival, DateTime? departDateTime)
        {
            if (depart != null)
            {
                Journeys = new ObservableCollection<Journey>(_unitOfWork
                    .JourneyRepository
                    .Journeys
                    .Where(x => x.Route.RoutePoint == depart));
            }
            if (arrival != null)
            {
                Journeys = new ObservableCollection<Journey>(_unitOfWork
                    .JourneyRepository
                    .Journeys
                    .Where(x => x.Route.RoutePoint1 == arrival));
            }
            if (departDateTime != null)
            {
                Journeys = new ObservableCollection<Journey>(_unitOfWork
                    .JourneyRepository
                    .Journeys
                    .Where(x => x?.DepartureTime?.Date == departDateTime.Value.Date));
            }
            if (depart != null && arrival != null)
            {
                Journeys = new ObservableCollection<Journey>(_unitOfWork
                    .JourneyRepository
                    .Journeys
                    .Where(x => x.Route.RoutePoint == depart)
                    .Where(x => x.Route.RoutePoint1 == arrival));
            }
            if (depart != null && arrival != null && departDateTime != null)
            {
                Journeys = new ObservableCollection<Journey>(_unitOfWork
                    .JourneyRepository
                    .Journeys
                    .Where(x => x.Route.RoutePoint == depart)
                    .Where(x => x.Route.RoutePoint1 == arrival)
                    .Where(x => x.DepartureTime != null && x.DepartureTime.Value.Date == departDateTime.Value.Date));
            }
        }

        private void Reset()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
            DerarturePoint = null;
            ArrivalPoint = null;
            SelectedDate = null;
        }

        #endregion

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
