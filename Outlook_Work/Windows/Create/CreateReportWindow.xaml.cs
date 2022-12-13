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

namespace Outlook_Work.Windows.Create
{
    /// <summary>
    /// Логика взаимодействия для CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {
        public static Window currentWindow { get; set; }

        public static int Itysy = 0;

        OutlookWorkDatabaseContext context;
        Order order;
        Users user;

        public CreateReportWindow(Order order, Users user)
        {
            InitializeComponent();
            this.context = OutlookWorkDatabaseContext.DbContext;
            this.order = order;
            this.user = user;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            if(user.UserCodeRole == 4)
            {
                Itysy = 1;
                Close();
            }
        }
    }
}
