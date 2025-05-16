using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoListApp.Models
{
    public class ToDoItem : ObservableObject
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _scheduledTime;
        public string ScheduledTime
        {
            get => _scheduledTime;
            set => SetProperty(ref _scheduledTime, value);
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }
    }
}
