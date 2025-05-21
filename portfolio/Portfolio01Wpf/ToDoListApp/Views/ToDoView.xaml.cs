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

            var vm = new ToDoViewModel();

            DataContext = new ToDoViewModel();
            DateTextBlock.Text = DateTime.Now.ToString("yyyy-MM-dd ddd");
        }

        private void ScrollToLastItem()
        {
            if (TaskListBox.Items.Count > 0)
            {
                TaskListBox.ScrollIntoView(TaskListBox.Items[TaskListBox.Items.Count - 1]);
            }
        }
        private void TimeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // ViewModel의 AddTaskCommand 실행
                if (DataContext is ToDoViewModel vm && vm.AddTaskCommand.CanExecute(null))
                {
                    vm.AddTaskCommand.Execute(null);
                }

                // 이벤트 처리 완료 명시 -> 기본 엔터 동작 막기
                e.Handled = true;

                // 제목 입력창으로 포커스 이동 (다시 시간으로 가지 않도록 Dispatcher 사용)
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TaskTitleTextBox.Focus();
                    TaskTitleTextBox.SelectAll(); // 입력한 텍스트 전체 선택
                }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            }
        }

        private void TaskTitleTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = this.DataContext as ToDoViewModel;
                if (vm != null && vm.AddTaskCommand.CanExecute(null))
                {
                    vm.AddTaskCommand.Execute(null);
                    ScrollToLastItem();  // 자동 스크롤
                }
            }
        }
    }
}
