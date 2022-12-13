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
using Outlook_Work.Models.Entities;
using Outlook_Work.UserControls;
using Outlook_Work.Windows.Infomation;
using Outlook_Work.Windows.Menus;

namespace Outlook_Work.Windows.Lists
{
    /// <summary>
    /// Логика взаимодействия для ShowListOfReports.xaml
    /// </summary>
    public partial class ShowListOfReports : Window
    {
        public static OutlookWorkDatabaseContext context;
        Users user;
        int whichIsRole;

        public ShowListOfReports(Users liveUser)
        {
            user = liveUser;
            whichIsRole = user.UserCodeRole;
            context = OutlookWorkDatabaseContext.DbContext;

            InitializeComponent();
        }

        private void FullList()
        {
            reportListView.Items.Clear();
            List<Report> reports = new List<Report>();
            reports = context.Report.ToList();
            foreach (Report obj in reports)
            {
                reportListView.Items.Add(new ReportControl(obj, user));
            }
        }


        private void editorinfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (reportListView.SelectedItem != null)
            {
                Report report = ((ReportControl)reportListView.SelectedItem).report;
                InfoAboutReport.creatingForm = this;
                new InfoAboutReport(report, user).ShowDialog();
                FullList();
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите отчет","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void reportListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Report report = ((ReportControl)reportListView.SelectedItem).report;
            InfoAboutReport.creatingForm = this;
            new InfoAboutReport(report, user).ShowDialog();
            FullList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullList();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
