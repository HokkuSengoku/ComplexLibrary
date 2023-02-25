namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class Scene
    {
        // possible implementation of figures storage
        // feel free to use your own!
        private readonly Dictionary<string, IFigure> _figures = new Dictionary<string, IFigure>();

        private readonly Dictionary<string, ICompositeFigure> _compositeFigures = new Dictionary<string, ICompositeFigure>();
        private CompositeFigure _compositeFiguress;

        public void AddFigure(string name, IFigure figure)
        {
            /* todo: check if the name is unique and throw if it's not */

            _figures[name] = figure;
        }

        public SceneRectangle CalculateSceneCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps the entire scene. */
            /* Already implemented but feel free to change according to figures storage strategy. */

            var allFigures = ListDrawableFigures()
                .Select(f => f.CalculateCircumscribingRectangle())
                .SelectMany(a => new[] { a.Vertex1, a.Vertex2 })
                .ToList();

            if (allFigures.Count == 0)
            {
                return default;
            }

            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(allFigures.Min(p => p.X), allFigures.Min(p => p.Y)),
                Vertex2 = new ScenePoint(allFigures.Max(p => p.X), allFigures.Max(p => p.Y)),
            };
        }

        public void CreateCompositeFigure(string name, IEnumerable<string> childFigures)
        {
            List<IFigure> figures = new List<IFigure>();
            foreach (var nameFigure in childFigures)
            {
                figures.Add(_figures[nameFigure]);
            }

            _compositeFiguress = new CompositeFigure(figures);
            if (_compositeFigures.ContainsKey(name))
            {
                throw new BadFormatException("ek");
            }
            else
            {
                _compositeFigures[name] = _compositeFiguress;
            }
        }

        public SceneRectangle CalculateCircumscribingRectangle(string name)
        {
            /* Should calculate the rectangle that wraps figure or group 'name' */

            throw new NotImplementedException();
        }

        public void PrintCircumscribingRectangleScene()
        {
            SceneRectangle scene = CalculateSceneCircumscribingRectangle();
            Console.WriteLine("Печатаем координаты описанного прямоугольника:");
            Console.WriteLine($"Верхняя левая вершина: ({scene.Vertex1.X}, {scene.Vertex1.Y})");
            Console.WriteLine($"Нижняя правая вершина: ({scene.Vertex2.X}, {scene.Vertex2.Y})");
        }

        public void PrintCircumscribingRectangle(string name)
        {
            if (_compositeFigures.ContainsKey(name))
            {
                SceneRectangle groupRectangle = _compositeFigures[name].CalculateCircumscribingRectangle();
                Console.WriteLine("Печатаем координаты описанного прямоугольника:");
                Console.WriteLine($"Верхняя левая вершина: ({groupRectangle.Vertex1.X}, {groupRectangle.Vertex1.Y})");
                Console.WriteLine($"Нижняя правая вершина: ({groupRectangle.Vertex2.X}, {groupRectangle.Vertex2.Y})");
            }
            else
            {
                SceneRectangle figureRectangle = _figures[name].CalculateCircumscribingRectangle();
                Console.WriteLine("Печатаем координаты описанного прямоугольника:");
                Console.WriteLine($"Верхняя левая вершина: ({figureRectangle.Vertex1.X}, {figureRectangle.Vertex1.Y})");
                Console.WriteLine($"Нижняя правая вершина: ({figureRectangle.Vertex2.X}, {figureRectangle.Vertex2.Y})");
            }
        }

        public void MoveScene(ScenePoint vector)
        {
            foreach (var figure in _figures)
            {
                figure.Value.Move(vector);
            }
        }

        public void Move(string name, ScenePoint vector)
        {
            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Move(vector);
            }
            else
            {
                _figures[name].Move(vector);
            }
        }

        public void RotateScene(double angle)
        {
            foreach (var figure in _figures)
            {
                figure.Value.Rotate(angle);
            }
        }

        public void Rotate(string name, double angle)
        {
            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Rotate(angle);
            }
            else
            {
                _figures[name].Rotate(angle);
            }
        }

        public IEnumerable<IFigure> ListDrawableFigures()
        {
            /* Already implemented */

            return _figures
                .Values
                .Concat(_compositeFigures.SelectMany(x => x.Value.ChildFigures))
                .Distinct();
        }

        public void CopyScene(string copyName)
        {
            /* Should copy the entire scene to a group named 'copyName' */

            List<IFigure> figures = new List<IFigure>();
            foreach (var figure in _figures)
            {
                figures.Add(figure.Value);
            }

            if (_compositeFigures.ContainsKey(copyName))
            {
                throw new NameDoesAlreadyExistException("Scene.cs Error in line 133: name does already exist");
            }
            else
            {
                _compositeFigures[copyName] = new CompositeFigure(figures);
            }
        }

        public void Copy(string originalName, string copyName)
        {
            /* Should copy figure or group 'originalName' to 'copyName' */

            if (_compositeFigures.ContainsKey(originalName) && !_compositeFigures.ContainsKey(copyName))
            {
                _compositeFigures[copyName] = (CompositeFigure)_compositeFigures[originalName].Clone();
            }
            else if (_figures.ContainsKey(originalName) && !_figures.ContainsKey(copyName))
            {
                _figures[copyName] = (IFigure)_figures[originalName].Clone();
            }
            else
            {
                throw new NameDoesAlreadyExistException("Error in line 155: name does already exist");
            }
        }

        public void DeleteScene()
        {
            _figures.Clear();
            _compositeFigures.Clear();
        }

        public void Delete(string name)
        {
            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures.Clear();
            }
            else if (_figures.ContainsKey(name))
            {
                _figures.Remove(name);
            }
            else
            {
                throw new BadFormatException("bad name input");
            }
        }

        public void ReflectScene(ReflectOrientation reflectOrientation)
        {
            foreach (var figure in _figures)
            {
                figure.Value.Reflect(reflectOrientation);
            }
        }

        public void Reflect(string name, ReflectOrientation reflectOrientation)
        {
            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Reflect(reflectOrientation);
            }
            else if (_figures.ContainsKey(name))
            {
                _figures[name].Reflect(reflectOrientation);
            }
            else
            {
                throw new BadNameException("Error in line 202: the name value does not exist");
            }
        }
    }
}
