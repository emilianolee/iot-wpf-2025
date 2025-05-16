using MahApps.Metro.Controls;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows.Threading;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Views
{
    /// <summary>
    /// ToDoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToDoView : MetroWindow
    {
        public ToDoView()
        {
            InitializeComponent();

            DataContext = new ToDoViewModel();
            DateTextBlock.Text = DateTime.Now.ToString("yyyy-MM-dd ddd");
        }

        private void TaskTitleTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = (ToDoViewModel)this.DataContext;
                if (vm.AddTaskCommand.CanExecute(null))
                {
                    vm.AddTaskCommand.Execute(null);
                }
            }
        }
    }
}
