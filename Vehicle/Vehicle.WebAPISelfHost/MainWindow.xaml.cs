using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Web.Http;
using System.Web.Http.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.AspNet.SignalR;
using System.Web.Routing;
using Microsoft.AspNet.SignalR.Client;

namespace Vehicle.WebAPISelfHost
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

		private void HostButton_Click(object sender, RoutedEventArgs e)
		{
			WebApp.Start<OWINStartup>(url: mBaseAddress);
			
		}

		private void StartHubButton_Click(object sender, RoutedEventArgs e)
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
			WebApp.Start<OWINStartup>(url: mBaseSRAddress);
			mHubConnection = new HubConnection(mBaseSRAddress);
			mHubProxy = mHubConnection.CreateHubProxy("TestHub");
			//connection.Start().Wait();

			mHubConnection.Start().Wait();

			//This is the delegate
			mHubProxy.On("broadcastMessage", x =>
			{
				progress.Report(x);
			});

			//mHubProxy.Invoke("Send", "something", "nothing").Wait();
		}

		private void SendMessageButton_Click(object sender, RoutedEventArgs e)
		{
			//mHubConnection = new HubConnection(mBaseSRAddress);
			//mHubProxy = mHubConnection.CreateHubProxy("TestHub");
			//mHubConnection.Start().Wait();

			mHubProxy.Invoke("Send", "something", "nothing").Wait();
		}

		private void GetValuesButton_Click(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			
			var response = client.GetAsync(mBaseAddress + "api/engine").Result;

			MessageBox.Show(response.ToString());
		}

		private void StartEngineButton_Click(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();

			client.PostAsync(mBaseAddress + "api/engine/StartEngine", null);
			
		}

		private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			mHubProxy.Invoke("Send", MessageTextBox.Text, "nothing").Wait();
		}
	}
}
