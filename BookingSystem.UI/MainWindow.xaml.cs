using System.Windows.Input;
using BookingSystem.UI.ViewModels;

namespace BookingSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Schedule_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageJourneys.DataContext = new JourneyViewModel();
        }

        private void Routes_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageRoutes.DataContext = new RouteViewModel();
        }

        private void Tickets_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageTickets.DataContext = new TicketViewModel();
        }
    }
}
