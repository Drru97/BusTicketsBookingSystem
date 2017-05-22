using System;
using System.Windows;
using BookingSystem.DataAccess.Concrete;

namespace BookingSystem.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unitOfWork.AdministratorRepository.Login(UsernameBox.Text, PasswordBox.Password))
                {
                    var adminWindow = new MainWindow();
                    adminWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PassengerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var userWindow = new UserWindow();
                userWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
