using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FilePickerTest6
{
    public sealed partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _resultString;

        public MainPageViewModel()
        {
        }

        [RelayCommand]
        private async void FileSelect()
        {
            try
            {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>{
                    { DevicePlatform.MacCatalyst, new [] { "json" } },
                    { DevicePlatform.macOS, new [] { "json"} },
                    { DevicePlatform.WinUI, new [] { ".json" } }
                    });
                PickOptions pickOptions = new PickOptions
                {
                    PickerTitle = "Choose an engine file.",
                    FileTypes = filePickerFileType
                };

                ResultString = "";
                FileResult result = await FilePicker.Default.PickAsync(pickOptions);
                if (result is not null)
                {
                    ResultString = result.FullPath;
                }
                else
                {
                    ResultString = "No file selected.";
                }
            }
            catch (Exception e)
            {
                ResultString = String.Format("Exception {0}", e.Message);
            }
        }
    }
}

