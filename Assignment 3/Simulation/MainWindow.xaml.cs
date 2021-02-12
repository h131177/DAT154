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
using System.Windows.Threading;
using SpaceSim;

namespace Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<SpaceObject> solarSystem = new List<SpaceObject> { };
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            Star sun = new Star("Sun", 4370005.6, 0, 1392684, 0, "Gul", null);
            //Planet mercury = new Planet("Mercury", 57909227, 87.97, 0, 0, "Grå", sun);
            Planet venus = new Planet("Venus", 108200000, 224.70, 0, 0, "Oransje", sun);
            //Planet earth = new Planet("Earth", 149500000, 365.26, 0, 0, "Blå", sun);
            //Moon moon = new Moon("Moon", 384000, 27.32, 0, 0, "Grå", earth);

            solarSystem.Add(sun);
            //solarSystem.Add(mercury);
            solarSystem.Add(venus);
            //solarSystem.Add(earth);
            //solarSystem.Add(moon);

            comboBox.Items.Add(sun.name);
            //comboBox.Items.Add(mercury.name);
            //comboBox.Items.Add(venus.name);
            //comboBox.Items.Add(earth.name);
            //comboBox.Items.Add(moon.name);

            Ellipse earth = new Ellipse();
            SolidColorBrush red = new SolidColorBrush();
            red.Color = Color.FromRgb(255, 0, 0);
            addPlanet(earth, 30, 30, red, 400, 300, 0, 0);

            //Skalering
            //double venstreSideAvPlaneten = planetenSinX - hvorLangtSkalJegTegneTilVenstre;
            //double skjermX = (venstre / (hvorLangtSkalJegTegneTilHøyre - hvorLangtSkalJegTegneTilVenstre)) * breddenPåVinduetMitt;
            double x = 0;
            double y = 0;

            Thickness m = new Thickness(x, y, 0, 0);
            double time = 0;
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();
            

            void timer_Tick(object sender, EventArgs e)
            {
                //Løkke som oppdaterer posisjon
                //Oppdaterer x og y
                time++;
                x = (int)(myGrid.RenderSize.Width / 2 - earth.Width / 2 +
                    (Math.Cos(time * 5 * 3.1416 / 180) * venus.orbitalRadius*0.000001));
                y = (int)(myGrid.RenderSize.Height / 2 - earth.Height / 2 +
                    (Math.Sin(time * 5 * 3.1416 / 180) * venus.orbitalRadius*0.000001));

                m.Left = x;
                m.Top = y;
                //earth.Height += 50;
                earth.Margin = m;
                //testE.Height += 50;
            }

        }

        private void addPlanet(Ellipse myEllipse, int w, int h, SolidColorBrush color, int l, int t, int r, int b) {
            Thickness m = new Thickness(l, t, r, b);
            //myEllipse = new Ellipse();
            myEllipse.Stroke = System.Windows.Media.Brushes.Black;
            myEllipse.Fill = color;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Left;
            myEllipse.VerticalAlignment = VerticalAlignment.Top;
            myEllipse.Width = w;
            myEllipse.Height = h;
            myEllipse.Margin = m;
            myGrid.Children.Add(myEllipse);
        }

        /*private SpaceObject GetSpaceObject(String planet)
        {
        }*/
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            string planet = (string)comboBox.SelectedItem;
            SpaceObject spaceObject = new SpaceObject("test", 1, 1, 1, 1, "color", null);
            foreach (SpaceObject obj in solarSystem) {
                if(planet == obj.name) {
                    spaceObject = obj;
                }
            }

            textBox1.Text = planet;
            textBox2.Text = spaceObject.objectColor;
            textBox3.Text = spaceObject.objectRadius.ToString();
            textBox4.Text = spaceObject.orbitalPeriod.ToString();
            textBox5.Text = spaceObject.orbitalRadius.ToString();
            if(spaceObject.parent != null) {
                textBox6.Text = spaceObject.parent.name;
            }
            textBox7.Text = spaceObject.rotationalPeriod.ToString();

            //if som sjekker om planeten har måner
            //Dersom måner kaller hjelpemetode som oppretter måner dynamisk
        }
    }
}
