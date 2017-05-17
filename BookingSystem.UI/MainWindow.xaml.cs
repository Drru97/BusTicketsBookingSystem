using System.Windows;
using BookingSystem.UI.ViewModels;

namespace BookingSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DriverViewModel();
        }
    }
}
