using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BookingSystem.DataAccess.Concrete;
using BookingSystem.Entities;
using BookingSystem.UI.Annotations;
using BookingSystem.UI.Commands;
using BookingSystem.UI.Helpers;

namespace BookingSystem.UI.ViewModels
{
    public class TicketsViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Journey _selectedJourney;
        public ObservableCollection<Journey> Journeys { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _bookTicketCommand;
        private RelayCommand _buyTicketCommand;

        //public RelayCommand BookTicketCommand
        //{
        //    get
        //    {
        //        return _bookTicketCommand ??
        //               (_bookTicketCommand = new RelayCommand());
        //    }
        //}

        public RelayCommand BuyTicketCommand
        {
            get { return _buyTicketCommand; }
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

        public IEnumerable<int> Seats
        {
            get
            {
                var seats = new List<int>();
                for (int i = 0; i < 30; i++)
                    seats.Add(i);
                return seats;
            }
        }

        public TicketsViewModel()
        {
            Journeys = new ObservableCollection<Journey>(_unitOfWork.JourneyRepository.Journeys);
            SelectedJourney = Journeys.FirstOrDefault();
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
