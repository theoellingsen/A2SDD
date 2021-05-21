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

namespace A2SDDWPF
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		enum ReportLevel { Poor, BelowExpectations, MeetingMinimum, StarPerformer }
		private Researcher researcher;
		private const string RESEARCHER_LIST_KEY = "ResearcherListView";
		public MainWindow()
		{
		InitializeComponent();

			researcher = (Researcher)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}

		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ResearcherListView.Items.Add(e);
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
			List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "Poor");
			String report = ListToString(rl);
			MessageBox.Show("Poor Performers:\n"+ report);
			
		}

		private void FilterClickBelow(object sender, RoutedEventArgs e)
		{
			List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "BelowExpectations");
			String report = ListToString(rl);
			MessageBox.Show("Researchers with Performance Below Expectations:\n" + report);
		}

		private void FilterClickMeeting(object sender, RoutedEventArgs e)
		{
			List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "MeetingMinimum");
			String report = ListToString(rl);
			MessageBox.Show("Researchers with Performance Meeting Minimum Requirements:\n" + report);
		}

		private void FilterClickStar(object sender, RoutedEventArgs e)
		{
			List<Researcher> rl = Database.LoadReseacherListView();
			rl = Report.OrderByPerformance(rl, "StarPerformers");
			String report = ListToString(rl);
			MessageBox.Show("Researchers that are Star Performers:\n" + report);
		}
	}
}
