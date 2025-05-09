using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace WpfBasicApp01.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        // 메시지박스, 다이얼로그를 위한 변수
        private readonly IDialogCoordinator _dialogCoordinator;

        private string _greeting;
        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                NotifyOfPropertyChange(() => Greeting);    // 속성 값 바뀐 것을 알려줘야 함
            }
        } 

        public MainViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            Greeting = "Hola, Caliburn.Micro!!";
        }

        public async void SayHello()
        {
            Greeting = "Hola a todos ^-^ ";

            // WinForms 방식
            //MessageBox.Show("Hola a todos ~ !!", "Greeting", MessageBoxButton.OK, MessageBoxImage.Information);
            await _dialogCoordinator.ShowMessageAsync(this, "Greeting", "Hola a todos ~! ~!");

        }
    }
}
