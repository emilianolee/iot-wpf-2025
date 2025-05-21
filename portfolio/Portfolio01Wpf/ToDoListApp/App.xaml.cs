using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using ToDoListApp.ViewModels;
using ToDoListApp.Views;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var coordinator = DialogCoordinator.Instance;
            var viewModels = new ToDoViewModel
            var view = new ToDoView();
            view.DataContext = new ToDoViewModel(coordinator);
            view.Show();
        }
    }

}
