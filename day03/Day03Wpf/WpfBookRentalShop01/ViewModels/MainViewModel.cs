﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfBookRentalShop01.ViewModels;
using WpfBookRentalShop01.Views;

namespace WpfBookRentalShop01.ModelViews
{
    public partial class MainViewModel : ObservableObject
    {
        private string _greeting;

        public string Greeting
        {
            get => _greeting;
            set => SetProperty(ref _greeting, value);

        }

        private string _currentStatus;
        public string CurrentStatus
        {
            get => _currentStatus;
            set => SetProperty(ref _currentStatus, value);
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public MainViewModel()
        {
            Greeting = "BookRentalShop!";
        }

        #region '화면 기능(이벤트) 처리'

        [RelayCommand]
        public void AppExit()
        {
            MessageBox.Show("종료합니다");
        }

        [RelayCommand]
        public void ShowBookGenre()
        {
            //MessageBox.Show("책장르관리");
            var vm = new BookGenreViewModel();
            var v = new BookGenreView
            {
                DataContext = vm,
            };
            CurrentView = v;
            CurrentStatus = "책 장르 관리 화면";
        }

        [RelayCommand]
        public void ShowBooks()
        {
            //MessageBox.Show("책관리");
            var vm = new BooksViewModel();
            var v = new BooksView
            {
                DataContext = vm,
            };
            CurrentView = v;
            CurrentStatus = "책 관리 화면";
        }
        #endregion
    }
}
