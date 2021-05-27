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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using A2SDD;

namespace A2SDDWPF
{
	/// <summary>
	/// Interaction logic for ResearcherDetailsView.xaml
	/// </summary>
	public partial class ResearcherDetailsView : Window
	{
        public Staff staffmember { get; set; }
        
		public ResearcherDetailsView(Researcher r)
		{
			InitializeComponent();

            if (r.EmployeeType == EmployeeType.Staff)
			{
                Staff researcher = ResearcherController.LoadStaff(r);

                staffmember = researcher;

                label_name.Content = researcher.GivenName + " " + researcher.FamilyName;
                label_title.Content = researcher.Title;
                label_campus.Content = ResearcherController.ParseCampus(researcher.Campus);
                label_positionCurrent.Content = researcher.CurrentJobTitle();
                label_school.Content = researcher.Unit;
                //PositionorStudent.Content = "Current Position:";
                label_email.Content = researcher.Email;
                label_commenced.Content = researcher.Start;
                label_current.Content = researcher.CurrentJobStart();
                if (researcher.Positions.Count > 1)
				{
                    PositionView.ItemsSource = researcher.GetPositions();
                } else
				{
                    ObservableCollection<string> noPositions = new ObservableCollection<string>();
                    noPositions.Add("No previous positions");
                    PositionView.ItemsSource = noPositions;
				}

                ObservableCollection<Publication> publications = PublicationsController.LoadPublicationsFor(researcher);

                if (publications.Count != 0)
				{
                    PublicationView.ItemsSource = publications;
                } else
				{
                    ObservableCollection<string> noPositions = new ObservableCollection<string>();
                    noPositions.Add("No publications");
                }
                label_tenure.Content = researcher.Tenure() + " Years";
                label_average.Content = Staff.ThreeYearAverage(r.ID);
                label_performance.Content = Staff.calcPerformance(researcher) + "%";
                label_publications.Content = researcher.PublicationsCount();
                label_supervisions.Content = Staff.SupervisionsCount(r.ID);


                String photo = researcher.Photo;
                var fullFilePath = @photo;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                pic_researcher.Source = bitmap;

            } else
			{
                Student researcher = ResearcherController.LoadStudent(r);
                label_name.Content = researcher.GivenName + " " + researcher.FamilyName;
                label_title.Content = researcher.Title;
                label_campus.Content = researcher.Campus;
                label_positionCurrent.Content = researcher.Degree;
                label_school.Content = researcher.Unit;
                conditional_job.Content = "Degree:";
                label_email.Content = researcher.Email;
                label_commenced.Content = researcher.Start;
                label_current.Content = researcher.CurrentJobStart();
                //PrevPos.Visibility = System.Windows.Visibility.Hidden;
                PositionView.Visibility = System.Windows.Visibility.Hidden;
                label_tenure.Content = researcher.Tenure() + " Years";
                label_average.Content = "N/A";
                label_performance.Content = "N/A";
                label_publications.Content = "N/A";
                label_supervisions.Content = "N/A";
                OpenSupervisions.Visibility = System.Windows.Visibility.Collapsed;

                ObservableCollection<Publication> publications = PublicationsController.LoadPublicationsFor(researcher);

                if (publications.Count != 0)
                {
                    PublicationView.ItemsSource = publications;
                }
                else
                {
                    ObservableCollection<string> noPositions = new ObservableCollection<string>();
                    noPositions.Add("No publications");
                }

                String photo = researcher.Photo;

                var fullFilePath = @photo;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                pic_researcher.Source = bitmap;
            }
            
        }
        private void PositionsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PositionView.DataContext = e.AddedItems[0];

            }
        }

        private void PublicationListViewMouseClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Publication selected = (Publication)listBox.SelectedItems[0];

            PublicationsDetailsView p = new PublicationsDetailsView(selected);
            p.Show();

        }


        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenSupervisions_Click(object sender, RoutedEventArgs e)
        {

            Supervisions open = new Supervisions(staffmember);
            open.Show();
        }

        private void OpenPublications_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SupervisionsPopUp_Opened(object sender, EventArgs e)
        {

        }

        private void PublicationsPopUp_Opened(object sender, EventArgs e)
        {
            
        }
    }
}
