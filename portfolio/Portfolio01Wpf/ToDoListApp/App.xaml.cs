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
            var view = new ToDoView();
            view.DataContext = new ToDoViewModel();
            view.Show();
        }
    }

}
