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
using A2SDDWPF;
using A2SDD;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace A2SDDWPF
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
		
		enum ReportLevel { Poor, BelowExpectations, MeetingMinimum, StarPerformer }
		private ResearcherController researcher;
		private const string RESEARCHER_LIST_KEY = "ResearcherListView";
		public MainWindow()
		{
		InitializeComponent();

		researcher = (ResearcherController)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}

		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				ResearcherListView.DataContext = e.AddedItems[0];

			}
		}

	private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{

		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

		private String ListToString(List<Researcher> rl)
		{
			String x = "";
			foreach(Researcher r in rl)
			{
				x += ("" + r.FamilyName);
				x += (", " + r.GivenName);
				x += (" (" + r.Title + ")\n");
			}
			return x;
		}
		private void FilterClickPoor(object sender, RoutedEventArgs e)
		{
			Poor p = new Poor();
			p.Show();
		}

		private void FilterClickBelow(object sender, RoutedEventArgs e)
		{
			Below b = new Below();
			b.Show();
		}

		private void FilterClickMeeting(object sender, RoutedEventArgs e)
		{
			Meeting m = new Meeting();
			m.Show();
			/*List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "MeetingMinimum");
			String report = ListToString(rl);
			MessageBox.Show("Researchers with Performance Meeting Minimum Requirements:\n" + report);*/
		}

		private void FilterClickStar(object sender, RoutedEventArgs e)
		{
			Star s = new Star();
			s.Show();
			/*List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "StarPerformer");
			String report = ListToString(rl);
			MessageBox.Show("Researchers that are Star Performers:\n" + report);*/
		}
		
		private void FilterClickA(object sender, RoutedEventArgs e)
		{
		
			ObjectDataProvider listProvider = (ObjectDataProvider)FindResource("ResearcherListView");
			var x = listProvider.Data;
			ObservableCollection<Researcher> r = new ObservableCollection<Researcher>();

			r = (ObservableCollection<Researcher>)x;

			ObservableCollection<Researcher> filtered = new ObservableCollection<Researcher>();
			
			foreach (Researcher researcher in r)
			{
				if (researcher.Positions.Count == 0 || researcher.Positions[0].Level != EmploymentLevel.A)
				{
					filtered.Add(researcher);
				}
			}
			foreach (Researcher item in filtered)
			{
				((ObservableCollection<Researcher>)ResearcherListView.ItemsSource).Remove(item);
			}
		}
		private void FilterClickB(object sender, RoutedEventArgs e)
		{

		}
		private void FilterClickC(object sender, RoutedEventArgs e)
		{

		}
		private void FilterClickD(object sender, RoutedEventArgs e)
		{

		}
		private void FilterClickE(object sender, RoutedEventArgs e)
		{

		}
	}

}

