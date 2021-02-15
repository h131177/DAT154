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

            Star sun = new Star("Sun", 4370005.6, 0, 1392684, 0, "yellow", null);
            Planet mercury = new Planet("Mercury", 57909227, 87.97, 0, 0, "green", sun);
            Planet venus = new Planet("Venus", 108200000, 224.70, 0, 0, "orange", sun);
            Planet earth = new Planet("Earth", 149500000, 365.26, 0, 0, "red", sun);
            Moon moon = new Moon("Moon", 384000, 27.32, 0, 0, "grey", earth);

            //solarSystem.Add(sun);
            solarSystem.Add(mercury);
            solarSystem.Add(venus);
            solarSystem.Add(earth);
            //solarSystem.Add(moon);

            comboBox.Items.Add(sun.name);
            comboBox.Items.Add(mercury.name);
            comboBox.Items.Add(venus.name);
            comboBox.Items.Add(earth.name);
            comboBox.Items.Add(moon.name);

            List<Ellipse> planets = new List<Ellipse> { };
            Ellipse planet;
            Ellipse parent;
            SolidColorBrush color;
            double x = 0;
            double y = 0;
            double time = 0;
            Tuple<double, double> t;

            foreach (SpaceObject obj in solarSystem)
            {
                planet = new Ellipse();
                parent = planet;
                color = new SolidColorBrush();
                bool isMooon = false;
                if(obj.name == "earth")
                {
                    parent = planet;
                }
                if(obj.name == "moon")
                {
                    isMooon = true;
                    //planet.Parent = parent;
                }
                planets.Add(planet);
                if(obj.objectColor == "red") {
                    color.Color = Color.FromRgb(255, 0, 0);
                } else if(obj.objectColor == "green") {
                    color.Color = Color.FromRgb(0, 255, 0);
                } else if(obj.objectColor == "yellow") {
                    color.Color = Color.FromRgb(255, 225, 53);
                } else if(obj.objectColor == "grey") {
                    color.Color = Color.FromRgb(132, 132, 130);
                } else if(obj.objectColor == "orange") {
                    color.Color = Color.FromRgb(255, 126, 0);
                }
                t = obj.calculatePosition(time);
                x = t.Item1;
                y = t.Item2;
          
                addPlanet(planet, 30, 30, color, x, y, isMooon);
            }

            //Skalering
            //double venstreSideAvPlaneten = planetenSinX - hvorLangtSkalJegTegneTilVenstre;
            //double skjermX = (venstre / (hvorLangtSkalJegTegneTilHøyre - hvorLangtSkalJegTegneTilVenstre)) * breddenPåVinduetMitt;
            
            Thickness m = new Thickness(x, y, 0, 0);

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();
            

            void timer_Tick(object sender, EventArgs e)
            {
                //Løkke som oppdaterer posisjon
                //Oppdaterer x og y
                time++;
                int i = 0;
                foreach (SpaceObject obj in solarSystem) {
                    t = obj.calculatePosition(time);
                    x = t.Item1;
                    y = t.Item2;
                    m.Left = x;
                    m.Top = y;
                    planets[i].Margin = m;

                    i++;
                }
            }
        }

        private void addPlanet(Ellipse myEllipse, int w, int h, SolidColorBrush color, double l, double t, bool isM) {
            Thickness m = new Thickness(l, t, 0, 0);
            myEllipse.Stroke = System.Windows.Media.Brushes.Black;
            myEllipse.Fill = color;
            if (isM)
            {
                //Kode for få månen til å rotere rundt jorda TODO
                //myEllipse.HorizontalAlignment = HorizontalAlignment.Center;
                //myEllipse.VerticalAlignment = VerticalAlignment.Center;
                //myEllipse.Parent;
            } else
            {
                myEllipse.HorizontalAlignment = HorizontalAlignment.Center;
                myEllipse.VerticalAlignment = VerticalAlignment.Center;
            }
            
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
