using Outlook_Work.Models.Entities;
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
using Outlook_Work.Windows.Create;

namespace Outlook_Work.Windows.InfoApplication
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutApplication.xaml
    /// </summary>
    public partial class InfoAboutApplication : Window
    {
        Order order;
        int whichIsRole;


        OutlookWorkDatabaseContext context;

        public static Window creatingForm { get; set; }

        public InfoAboutApplication(Order onOrder, int idRole)
        {
            whichIsRole = idRole;
            context = OutlookWorkDatabaseContext.DbContext;
            order = onOrder;
            InitializeComponent();
        }



        private void CheckCurrentOrder()
        {
            if (whichIsRole == 1 || whichIsRole == 3 || whichIsRole == 4)
            {
                Users user = context.Users.Where(u => u.Iduser == order.CodeEmployeer).FirstOrDefault();
                contentEmployeerLabel.Content = "Сотрудник:" + " " + user.UserName + " " + user.UserSurname;
                if (whichIsRole == 3 && order.CodeHeadDepartament == null)
                {
                    addButton.IsEnabled = true;
                    addButton.Visibility = Visibility;
                    addButton.Content = "Принять заявку";
                    deleteButton.IsEnabled = true;
                    deleteButton.Visibility = Visibility;
                    deleteButton.Content = "Отклонить заявку";
                }
                else if (whichIsRole == 4)
                {
                    if(order.CodeStatus == 1 || order.CodeStatus == 2)
                    {

                    }
                    else
                    {
                        addButton.IsEnabled = true;
                        addButton.Visibility = Visibility;
                        addButton.Content = "Оформить отчет";
                    }
                }
                else if (order.CodeHeadDepartament !=null){
                    Users userHead = context.Users.Where(u => u.Iduser == order.CodeHeadDepartament).FirstOrDefault();
                    headofDepartLabel.Content = "Начальник отдела: " + userHead.UserSurname + " " +user.UserName[0] + ".";
                }
            }

            Status status = context.Status.Where(w => w.Idstatus == order.CodeStatus).FirstOrDefault();
            statusLabel.Content = status.StatusName;

            TextRange richText = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
            contentRichTextBox.AppendText(order.OrderContent.ToString());
        }

        private void editorinfoButton_TimeChanged(object sender, MaterialDesignThemes.Wpf.TimeChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentOrder();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
           if(whichIsRole == 3)
            {
                Window next = new DepartmentSelectionWindow(order);
                next.ShowDialog();
                if (DepartmentSelectionWindow.Itys == 1)
                {
                    DepartmentSelectionWindow.Itys = 0;
                    Close();
                }
            }
           else if(whichIsRole == 4)
            {
                Users user = context.Users.Where(u => u.Iduser == order.CodeHeadDepartament).FirstOrDefault();
                Window nextWindow = new CreateReportWindow(order,user);
                nextWindow.ShowDialog();
                if (CreateReportWindow.Itysy == 1)
                {
                    CreateReportWindow.Itysy = 0;
                    Close();
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(whichIsRole == 3)
            {
                order.CodeStatus = 2;
                context.SaveChanges();
                Close();
            }
        }
    }
}
