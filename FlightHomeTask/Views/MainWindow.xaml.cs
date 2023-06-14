using FlightHomeTask.Commands;
using FlightHomeTask.DbFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FlightHomeTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FlightsDbEntities _context = new FlightsDbEntities();
            var result1 = from a in _context.Citieesses
                          select a;
            CityGrid.ItemsSource=result1.ToList();


            var result2 = from a in _context.SScheduleesses
                          select a;
            ScheduleGrid.ItemsSource=result2.ToList();


            var result3 = from a in _context.Airplaneesses
                          select a;
            AirplaneGrid.ItemsSource=result3.ToList();

            var result4 = from a in _context.FlightTypes
                          select a.Type;
            fligthTypeComboBox.ItemsSource=result4.ToList();
           
        }





        private void CityGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new FlightsDbEntities())
            {

                var item = CityGrid.SelectedItem as Citieess;
                var city = context.Citieesses.Include(nameof(Citieess.SScheduleesses)).FirstOrDefault(c => c.Id==item.Id);
                if (city!=null)
                {
                    var schedules = city.SScheduleesses.ToList();
                    ScheduleGrid.ItemsSource = schedules;
                }


            }
        }

        private void ScheduleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new FlightsDbEntities())
            {

                var item = ScheduleGrid.SelectedItem as SScheduleess;
                var schedule = context.Citieesses.Include(nameof(SScheduleess.Airplaneesses)).FirstOrDefault(s => s.Id==item.Id);
                if (schedule!=null)
                {
                    var airplanes = schedule.SScheduleesses.ToList();
                    ScheduleGrid.ItemsSource = airplanes;
                }


            }
        }

        private void AirplaneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
