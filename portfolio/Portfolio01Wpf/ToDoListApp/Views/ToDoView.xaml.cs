using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

            var vm = new ToDoViewModel(DialogCoordinator.Instance);
            this.DataContext = vm;
            vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(vm.InputTitle))
                {
                    Dispatcher.InvokeAsync(() =>
                    {
                        if (TaskListBox.Items.Count > 0)
                        {
                            TaskListBox.ScrollIntoView(TaskListBox.Items[TaskListBox.Items.Count - 1]);
                        }
                    });
                }
            };

            //DataContext = new ToDoViewModel();
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
