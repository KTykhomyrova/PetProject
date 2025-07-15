using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackHub.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string greeting = "Hello, world!";

        [RelayCommand]
        private void SayHello()
        {
            Greeting = "Hello from ViewModel!";
        }
    }
}
