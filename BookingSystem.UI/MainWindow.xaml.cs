using System.Windows;
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

        private void Schedule_Clicked(object sender, RoutedEventArgs e)
        {
            ManageJourneys.DataContext = new JourneyViewModel();
        }

        private void Routes_Clicked(object sender, MouseButtonEventArgs e)
        {
            ManageRoutes.DataContext = new RouteViewModel();
        }
    }
}
