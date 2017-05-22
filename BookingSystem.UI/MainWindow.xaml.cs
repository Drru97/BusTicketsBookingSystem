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

        private void RoutePoints_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageRoutePoints.DataContext = new RoutePointsViewModel();
        }

        private void Buses_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageBuses.DataContext = new BusViewModel();
        }

        private void Drivers_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageDrivers.DataContext = new DriverViewModel();
        }
    }
}
