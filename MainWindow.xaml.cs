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
			errorMessage();
		

			researcher = (ResearcherController)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
		}

		private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				ResearcherListView.DataContext = e.AddedItems[0];

			}
		}

		public void errorMessage()
		{
			if (ResearcherController.LoadResearchers().Count == 0)
			{
				MessageBox.Show("ERROR: Database could not connect.\nPlease check your connection.\nPress ok to quit the program.");
				Environment.Exit(1);
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
		}

		private void FilterClickStar(object sender, RoutedEventArgs e)
		{
			Star s = new Star();
			s.Show();
		}
		
		private void FilterClickA(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.FilterBy(EmploymentLevel.A);
		}
		private void FilterClickB(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.FilterBy(EmploymentLevel.B);
		}
		private void FilterClickC(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.FilterBy(EmploymentLevel.C);
		}
		private void FilterClickD(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.FilterBy(EmploymentLevel.D);
		}
		private void FilterClickE(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.FilterBy(EmploymentLevel.E);
		}
		private void FilterClickStudent(object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.Students();
		}

		private void FilterClickAll (object sender, RoutedEventArgs e)
		{
			ResearcherListView.ItemsSource = ResearcherController.LoadResearchers();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string content = SearchBox.Text;
			ResearcherListView.ItemsSource = ResearcherController.FilterSearch(content);
		}
		private void ButtonReset_Click(object sender, RoutedEventArgs e)
		{
			
			ResearcherListView.ItemsSource = ResearcherController.LoadResearchers();
			SearchBox.Text = "";
			
		}

		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void ListViewMouseClick(object sender, MouseButtonEventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			Researcher selected = (Researcher) listBox.SelectedItems[0];

			ResearcherDetailsView r = new ResearcherDetailsView(selected);
			r.Show();

		}
	}

}

