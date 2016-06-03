using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNet.SignalR.Client;

namespace Vehicle.WPFTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		string mBaseAddress = "http://localhost:9000/";
		string mBaseSRAddress = "http://localhost:8081/";
		private IHubProxy mHubProxy;
		private HubConnection mHubConnection;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void ConnectButton_Click(object sender, RoutedEventArgs e)
		{
			Progress<string> progress;
			progress = new Progress<string>();
			progress.ProgressChanged += Progress_ProgressChanged;
			StartHub(progress);
		}

		private void Progress_ProgressChanged(object sender, string e)
		{
			MessageTextBox.Text = e;
		}

		private void StartHub(IProgress<string> progress)
		{
			mHubConnection = new HubConnection(mBaseSRAddress);
			mHubProxy = mHubConnection.CreateHubProxy("TestHub");
			//connection.Start().Wait();

			mHubConnection.Start().Wait();

			//This is the delegate
			mHubProxy.On("broadcastMessage", x =>
			{
				progress.Report(x);
			});

		}

		private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			mHubProxy.Invoke("Send", MessageTextBox.Text, "nothing").Wait();
		}
	}
}
