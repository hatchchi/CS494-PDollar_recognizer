using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDollarGestureRecognizer;


namespace pdollar 
{
    
    class pdollarApp 
    {
        //if just pdollar is entered, provide help
        public static void PrintHelp() 
        {
            Console.WriteLine("\n  __________________________HELP________________________________  \n");
            Console.WriteLine("     Here are all the commands to use with pdollar:\n");
            Console.WriteLine(" 1. pdollar -t <gesturefile>      adds a gesture file to the list of gesture templates");
            Console.WriteLine("     \n");
            Console.WriteLine(" 2. pdollar -r                    clears all the stored templates");
            Console.WriteLine("     \n");
            Console.WriteLine(" 3. pdollar <eventstream>         prints the name of gestures as they are recognized from the event stream");
            Console.WriteLine("     \n");
        }

        // add a gesture file to the list of gesture templates
        public static Gesture AddGestureFile(string gestureFile, bool templateStore) 
        {
            try 
            {
                StreamReader streamReader = File.OpenText(gestureFile);
                List<Point> pointList = new List<Point>();

                //get gesture inputs from and look for end, and assign stroke ID
                int strokeID = 0;
                string gestureName, line;
                gestureName = streamReader.ReadLine();

                while ((line = streamReader.ReadLine()) != null) 
                {
                    if (line == "BEGIN") continue;
                    else if (line == "END") strokeID++;
                    else 
                    {
                        int[] coords = line.Split(',').Select(int.Parse).ToArray();
                        pointList.Add(new Point(coords[0], coords[1], strokeID));
                    }
                }
                streamReader.Close();

                if (templateStore) 
                {
                    streamReader = File.OpenText(gestureFile);
                    StreamWriter writer = new StreamWriter("./templates/" + gestureName + ".txt");
                    while ((line = streamReader.ReadLine()) != null) writer.WriteLine(line);
                    writer.Close();
                }

                return new Gesture(pointList.ToArray(), gestureName); ;
            }
            //catchall, just in case
            catch (FileNotFoundException) 
            {
                Console.WriteLine("Sorry, file called " + gestureFile + " was not found.");
            }
            return null;
        }


        // recognizes and prints the name of gestures as they are recognized from the event stream
        public static void RecognizeEvents(string eventStream) 
        {
            List<Gesture> templates = new List<Gesture>();
            string[] files = Directory.GetFiles("./templates");
            foreach (string file in files) templates.Add(AddGestureFile(file, false));
            try 
            { 
                StreamReader streamReader = File.OpenText(eventStream);
                List<Point> pointList = new List<Point>();
                int strokeID = 0;
                string line;

                while ((line = streamReader.ReadLine()) != null) 
                {
                    //add series of strokes to each strokeID
                    if (line == "MOUSEDOWN") continue;
                    else if (line == "MOUSEUP") strokeID++;
                    else if (line == "RECOGNIZE") 
                    {
                        Gesture gesture = new Gesture(pointList.ToArray());
                        string recognized = PointCloudRecognizer.Classify(gesture, templates.ToArray());
                        if (recognized != "") Console.WriteLine(recognized);
                        strokeID = 0;
                        pointList.Clear();
                    }
                    else 
                    {
                        int[] coords = line.Split(',').Select(int.Parse).ToArray();
                        pointList.Add(new Point(coords[0], coords[1], strokeID));
                    }
                }
            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine("Sorry, file called " + eventStream + " was not found.");
            }
        }
        // clears all the stored templates
        public static void ClearTemplates() 
        {
            string[] files = Directory.GetFiles("./templates");
            foreach (string file in files) File.Delete(file);
            if (Directory.Exists("./templates")) Directory.Delete("./templates");
        }

        //listen for main commands, send to sub routines
        public static void Main(string[] args) 
        {
            //if no template directory, create one
            if (!Directory.Exists("./templates"))
                Directory.CreateDirectory("./templates");

            // if nothing entered after pdollar, print help
            if (args.Length <= 1) PrintHelp();

            //if pdollar AND -t, -r, or eventfile run applicable subroutine (maybe ommit pdollar here?)
            else if (args[0] == "pdollar") 
            {
                if (args[1] == "-t" && args.Length == 3 && args[2].EndsWith(".txt"))
                    AddGestureFile(args[2], true);
                else if (args.Length == 2 && args[1].EndsWith("eventfile.txt"))
                    RecognizeEvents(args[1]);
                else if (args.Length == 2 && args[1] == "-r")
                    ClearTemplates();
                else PrintHelp();
            }
            // catchall, just in case
            else 
            {
                Console.WriteLine(args[0]);
                PrintHelp();
            }
        }
    }
}