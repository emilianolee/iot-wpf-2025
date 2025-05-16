using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
    public partial class ToDoViewModel : ObservableObject
    {
        private string _inputTitle;
        public string InputTitle
        {
            get => _inputTitle;
            set => SetProperty(ref _inputTitle, value);
        }

        private string _inputTime;
        public string InputTime
        {
            get => _inputTime;
            set => SetProperty(ref _inputTime, value);
        }

        public ObservableCollection<ToDoItem> Tasks { get; } = new();

        [RelayCommand]
        public void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(InputTitle))
            {
                Tasks.Add(new ToDoItem
                {
                    Title = InputTitle,
                    ScheduledTime = InputTime,
                    IsCompleted = false
                });

                InputTitle = string.Empty;
                InputTime = string.Empty;
            }
        }

        [RelayCommand]
        public void DeleteDoneTasks()
        {
            for (int i = Tasks.Count - 1; i >= 0; i--)
            {
                if (Tasks[i].IsCompleted)
                {
                    Tasks.RemoveAt(i);
                }
            }
        }
    }
}
