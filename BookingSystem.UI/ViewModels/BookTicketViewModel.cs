using System;
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
    public class BookTicketViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Journey _selectedJourney;
        public ObservableCollection<Journey> Journeys { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool PassengerInfoError => string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName);

        public int? SeatsCount => _selectedJourney.Bus.PassengersCount;

        private string _firstName;
        private string _lastName;
        private int _seat;
        private ObservableCollection<int> _seats;

        private RelayCommand _bookTicketCommand;
        private RelayCommand _buyTicketCommand;

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

        public int Seat
        {
            get => _seat;
            set
            {
                _seat = value;
                OnPropertyChanged(nameof(Seat));
            }
        }

        public ObservableCollection<int> Seats
        {
            get => _seats;
            set
            {
                _seats = GetSeats();
                OnPropertyChanged(nameof(Seats));
            }
        }

        public ObservableCollection<int> GetSeats()
        {
            _seats = new ObservableCollection<int>();

            for (int i = 1; i <= SelectedJourney.Bus.PassengersCount; i++)
            {
                _seats.Add(i);
            }

            return _seats;
        }

        public BookTicketViewModel()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
            SelectedJourney = Journeys.FirstOrDefault();
        }

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

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
