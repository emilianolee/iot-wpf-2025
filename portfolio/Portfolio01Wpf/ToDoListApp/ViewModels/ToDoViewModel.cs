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
            set
            {
                // 빈 문자열이면 바로 반영
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _inputTime, string.Empty);
                    return;
                }

                // 사용자가 직접 '시'만 입력하거나 숫자를 지워 '시'만 남은 경우 처리
                if (value == "시간")
                {
                    SetProperty(ref _inputTime, string.Empty);
                    return ;
                }

                // 숫자만 있는지 정규식 체크
                var onlyNum = System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$");

                if (onlyNum)
                {
                    // 숫자만 있으면 뒤에 "시간"을 붙임!
                    value += "시간";
                }

                SetProperty(ref _inputTime, value);
            }
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
