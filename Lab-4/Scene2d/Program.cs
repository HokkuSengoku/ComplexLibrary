namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            /*
             * Main Application Loop
             */

            Console.WriteLine("Starting scene application...");

            var commandProducer = new CommandProducer();
            var scene = new Scene();

            bool readCommandsFromFile = args.Length > 0;

            IEnumerable<string> commands = readCommandsFromFile ?
                ReadCommandsFromFile(args[0]) :
                ReadCommandsFromUserInput();

            bool drawSceneOnEveryCommand = !readCommandsFromFile;

            foreach (string commandLine in commands)
            {
                try
                {
                    commandProducer.AppendLine(commandLine);

                    if (commandProducer.IsCommandReady)
                    {
                        var command = commandProducer.GetCommand();
                        command.Apply(scene);

                        Console.WriteLine(command.FriendlyResultMessage);

                        if (drawSceneOnEveryCommand)
                        {
                            DrawScene(scene);
                        }
                    }
                }
                catch (BadFormatException)
                {
                    Console.WriteLine("bad format");
                }

                /* todo: more exceptions handling here */
            }

            if (!drawSceneOnEveryCommand)
            {
                DrawScene(scene);
            }

            Console.WriteLine("Commands processing complete.");
        }

        private static IEnumerable<string> ReadCommandsFromFile(string input)
        {
            Console.WriteLine("Reading commands from input file " + input);

            return File.ReadAllLines(input);
        }

        private static IEnumerable<string> ReadCommandsFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Enter a command or press Enter to exit");
                Console.Write("> ");

                string line = Console.ReadLine();
                if (line == null || line.Trim().Length == 0)
                {
                    break;
                }

                yield return line;
            }
        }

        private static void DrawScene(Scene scene)
        {
            const string outputFileName = "scene.png";

            if (File.Exists(outputFileName))
            {
                File.Delete(outputFileName);
            }

            var area = scene.CalculateSceneCircumscribingRectangle();

            var origin = new ScenePoint
            {
                X = Math.Min(area.Vertex1.X, area.Vertex2.X),
                Y = Math.Min(area.Vertex1.Y, area.Vertex2.Y),
            };

            var width = (int)Math.Abs(area.Vertex1.X - area.Vertex2.X) + 1;
            var height = (int)Math.Abs(area.Vertex1.Y - area.Vertex2.Y) + 1;

            using (Stream output = File.Create(outputFileName))
            using (Image image = new Bitmap(width, height))
            using (Graphics drawing = Graphics.FromImage(image))
            {
                using (var bg = new SolidBrush(Color.DarkGray))
                {
                    drawing.FillRectangle(bg, 0, 0, width, height);
                }

                drawing.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;

                foreach (var figure in scene.ListDrawableFigures())
                {
                    figure.Draw(origin, drawing);
                }

                image.Save(output, ImageFormat.Png);
            }

            Console.WriteLine("The scene has been saved to " + outputFileName);
        }
    }
}
