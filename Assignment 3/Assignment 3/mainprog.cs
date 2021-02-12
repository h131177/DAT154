using System; 
using System.Collections.Generic; 
using SpaceSim; 
class Astronomy { 
    public static void Main() { 
        /*List<SpaceObject> solarSystem = new List<SpaceObject>
        { 
            new Star("Sun"), 
            new Planet("Mercury"),
            new Planet("Venus"),
            new Planet("Earth"),
            new Moon("The Moon")
        };*/

        Star sun = new Star("Sun", 4370005.6, 0, 1392684, 0, "Gul", null);
        Planet mercury = new Planet("Mercury", 57909227, 87.97, 0, 0, "Grå", sun);
        Planet venus = new Planet("Venus", 108200000, 224.70, 0, 0, "Oransje", sun);
        Planet earth = new Planet("Earth", 149500000, 365.26, 0, 0, "Blå", sun);
        Moon moon = new Moon("Moon", 384000, 27.32, 0, 0, "Grå", earth);

        //this.orbitalRadius = orbitalRadius;
        //this.orbitalPeriod = orbitalPeriod;
        //this.objectRadius = objectRadius;
        //this.rotationalPeriod = rotationalPeriod;
        //this.objectColor = objectColor;
        //this.parent = parent;

        //new Moon("The Moon"),
        //new DvarfPlanet("Pluto"),
        //new AsteroidBelt("Asteroidebeltet"),
        //new Asteroid("4 Vesta"),
        //new Asteroid("Ceres")

        List<SpaceObject> solarSystem = new List<SpaceObject> { };
        solarSystem.Add(sun);
        solarSystem.Add(mercury);
        solarSystem.Add(venus);
        solarSystem.Add(earth);
        solarSystem.Add(moon);


        foreach (SpaceObject obj in solarSystem) {
            obj.Draw(); 
        } 
        Console.ReadLine();

        Console.WriteLine("Skriv inn tidspunkt: ");
        double time = Double.Parse(Console.ReadLine());

        Console.WriteLine("Navn på SpaceObject: ");
        String name = Console.ReadLine();
        bool found = false;
        int counter = 1;
        foreach (SpaceObject obj in solarSystem) { 
            if(obj.name == name)
            {
                write(obj, time, solarSystem);
                found = true;
            } else if(!found && counter == solarSystem.Count)
            {
                write(sun, time, solarSystem);
            }
            counter++;
        }
    }

    private static void write(SpaceObject obj, double time, List<SpaceObject> solarSystem)
    {
        Console.WriteLine("Details about the chosen planet:");
        obj.Draw();
        Console.WriteLine("Position:" + obj.calculatePosition(time));
        Console.WriteLine("Details about the childs:");
        foreach (SpaceObject child in solarSystem)
        {
            if (child.parent != null)
            {
                if (child.parent == obj)
                {
                    child.Draw();
                    Console.WriteLine("Position:" + child.calculatePosition(time));
                }
            }
        }
    }
}
