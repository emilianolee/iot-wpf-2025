using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using System.Data;

namespace WpfMqttSubApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private string _brokerHost;
        private string _databaseHost;

        // 속성 BrokerHost, DatabaseHost
        // 메서드 ConnectBrokerCommand, ConnectDatabaseCommand

        public MainViewModel(IDialogCoordinator coordinator)
        {
            this._dialogCoordinator = coordinator;

            BrokerHost = "211.119.12.59";
            DatabaseHost = "211.119.12.59";
        }

        public string BrokerHost
        {
            get => _brokerHost;
            set => SetProperty(ref _brokerHost, value);
        }

        public string DatabaseHost
        {
            get => _databaseHost;
            set => SetProperty(ref _databaseHost, value);
        }

        [RelayCommand]
        public async Task ConnectBroker()
        {
            await this._dialogCoordinator.ShowMessageAsync(this, "브로커 연결", "브로커 연결합니다!");
        }

        [RelayCommand]
        public async Task ConnectDatabase()
        {
            await this._dialogCoordinator.ShowMessageAsync(this, "DB 연결", "DB 연결합니다!");
        }
    }
}
