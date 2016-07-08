using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Maker.Serial;
using Microsoft.Maker.RemoteWiring;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Blink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IStream connection;
        RemoteDevice arduino;
        public MainPage()
        {
            this.InitializeComponent();
            connection = new NetworkSerial(new Windows.Networking.HostName("192.168.1.170"), 3030);
            arduino = new RemoteDevice(connection);
            connection.ConnectionEstablished += OnConnectionEstablished;

            connection.begin(115200, SerialConfig.SERIAL_8N1);
        }

        private void OnConnectionEstablished()
        {
            var action = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(() =>
                {
                OnButton.IsEnabled = true;
                OffButton.IsEnabled = true;
            }));
        }

        private void OnButton_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(6, PinState.HIGH);
        }

        private void OffButton_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(6, PinState.LOW);
        }
    }
}
