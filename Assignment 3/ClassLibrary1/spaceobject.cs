using System; 
namespace SpaceSim { 
    public class SpaceObject{ 
        public String name { get; set; }
        public double orbitalRadius { get; set; }
        public double orbitalPeriod { get; set; }
        public double objectRadius { get; set; }
        public double rotationalPeriod { get; set; }
        public String objectColor { get; set; }
        public SpaceObject parent { get; set; }

        public SpaceObject(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) { 
            this.name = name;
            this.orbitalRadius = orbitalRadius;
            this.orbitalPeriod = orbitalPeriod;
            this.objectRadius = objectRadius;
            this.rotationalPeriod = rotationalPeriod;
            this.objectColor = objectColor;
            this.parent = parent;
        }   
        public virtual void Draw() { 
            Console.WriteLine(name); 
        } 

        public virtual Tuple<double, double> calculatePosition(double time) {
            // orbital_radius
            // orbital_period
            double x = 0;
            double y = 0;
            //double angularVelocity = ((2 * Math.PI) / orbitalPeriod);
            //x = Math.Round(Math.Cos(angularVelocity * time) * 20 * Math.Cbrt(orbitalRadius));
            //y = Math.Round(Math.Sin(angularVelocity * time) * 20 * Math.Cbrt(orbitalRadius));

            x = (int)(1000 / 2 - 30 / 2 +
                    (Math.Cos(time * orbitalPeriod * 0.04 * 3.1416 / 180) * orbitalRadius));
            y = (int)(500 / 2 - 30 / 2 +
                (Math.Sin(time * orbitalPeriod * 0.04 * 3.1416 / 180) * orbitalRadius));
            x *= 0.000003;
            y *= 0.000003;
            return new Tuple<double, double>(x,y);
        }

    } 
    public class Star: SpaceObject { 
        public Star(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) : 
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent) { 
        } 
        public override void Draw() { 
            Console.Write("Star  : "); 
            base.Draw(); 
        } 
    } 
    public class Planet: SpaceObject { 
        public Planet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        { 
        } 
        public override void Draw() { 
            Console.Write("Planet: "); 
            base.Draw(); 
        } 
    } 
    public class Moon: Planet{ 
        public Moon(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        { 
        } 
        public override void Draw() { 
            Console.Write("Moon  : "); 
            base.Draw(); 
        } 
    } 
    public class Comet: SpaceObject {
        public Comet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        {
        }
        public override void Draw() {
            Console.Write("Comet : ");
            base.Draw();
        }
    }
    public class Asteroid : SpaceObject
    {
        public Asteroid(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        {
        }
        public override void Draw()
        {
            Console.Write("Asteroid : ");
            base.Draw();
        }
    }
    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        {
        }
        public override void Draw()
        {
            Console.Write("Asteroid Belt : ");
            base.Draw();
        }
    }
    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, String objectColor, SpaceObject parent) :
            base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor, parent)
        {
        }
        public override void Draw()
        {
            Console.Write("Dwarf Planet : ");
            base.Draw();
        }
    }
}

