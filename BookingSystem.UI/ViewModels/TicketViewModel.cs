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
    public class TicketViewModel
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private Ticket _selectedTicket;
        public ObservableCollection<Ticket> Tickets { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _removeTicketCommand;

        public RelayCommand RemoveTicketCommand
        {
            get
            {
                return _removeTicketCommand ??
                       (_removeTicketCommand = new RelayCommand(obj =>
                       {
                           var ticket = obj as Ticket;
                           if (ticket != null)
                           {
                               Tickets.Remove(ticket);
                               _unitOfWork.TicketRepository.RemoveTicket(ticket);
                               SelectedTicket = Tickets.FirstOrDefault();
                           }
                       },
                       obj => Tickets.Count > 0 && SelectedTicket != null));
            }
        }

        public Ticket SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                _selectedTicket = value;
                OnPropertyChanged(nameof(SelectedTicket));
            }
        }

        public TicketViewModel()
        {
            Tickets = new ObservableCollection<Ticket>(_unitOfWork.TicketRepository.Tickets);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
