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

                label_title.Content = researcher.Title + " " + researcher.GivenName + " " + researcher.FamilyName;
                label_campus.Content = ResearcherController.ParseCampus(researcher.Campus);
                label_positionCurrent.Content = researcher.CurrentJobTitle();
                label_school.Content = researcher.Unit;
                PositionorStudent.Content = "Current Position:";
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
                label_performance.Content = Staff.calcPerformance(r) + "%";
                //label_publications.Content = researcher.PublicationsCount();
                label_supervisions.Content = Staff.SupervisionsCount(r.ID);


                String photo = researcher.Photo;

                var image = new Image();
                var fullFilePath = @photo;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
                pic.Children.Add(image);

            } else
			{
                Student researcher = ResearcherController.LoadStudent(r);
                label_title.Content = researcher.Title + " " + researcher.GivenName + " " + researcher.FamilyName;
                label_campus.Content = researcher.Campus;
                label_positionCurrent.Content = researcher.Degree;
                label_school.Content = researcher.Unit;
                PositionorStudent.Content = "Current Degree:";
                label_email.Content = researcher.Email;
                label_commenced.Content = researcher.Start;
                label_current.Content = researcher.CurrentJobStart();
                PrevPos.Visibility = System.Windows.Visibility.Hidden;
                PositionView.Visibility = System.Windows.Visibility.Hidden;
                label_tenure.Content = researcher.Tenure() + " Years";
                label_average.Visibility = System.Windows.Visibility.Hidden;
                yrAve.Visibility = System.Windows.Visibility.Hidden;
                label_performance.Visibility = System.Windows.Visibility.Hidden;
                perf.Visibility = System.Windows.Visibility.Hidden;
                //label_publications.Content = researcher.PublicationsCount();
                //label_supervisions.Visibility = System.Windows.Visibility.Hidden;
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

                var image = new Image();
                var fullFilePath = @photo;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
                pic.Children.Add(image);
            }
            
        }
        private void PositionsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PositionView.DataContext = e.AddedItems[0];

            }
        }


        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenSupervisions_Click(object sender, RoutedEventArgs e)
        {

            Staff staff = (Staff) staffmember;
            Supervisions open = new Supervisions(staff);

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
