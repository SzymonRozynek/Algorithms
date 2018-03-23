using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Algorithms {
    class Graph : Window {

        private const int axisWidth = 3;
        private const int axisOffset = 50;
        private const int lineWidth = 1;
        private const int lineCount = 25;
        Font font;

        public Graph() {
            windowWidth = 600;
            windowHeight = 600;
            points = new List<Point>();
            font = new Font("fonts\\arial.ttf");
        }

        List<Point> points;

        protected override string GetWindowName() {
            return "Graph";
        }

        public void AddPoint(int argument, float value) {
            points.Add(new Point(argument, value));
        }

        protected override void Update() {

            int graphWidth = (int)windowWidth - 2 * axisOffset;
            int graphHeight = (int)windowHeight - 2 * axisOffset;

            //Axis
            RectangleShape axisX = new RectangleShape() {
                Position = new Vector2f(axisOffset, windowHeight - axisOffset),
                Size = new Vector2f(graphWidth, axisWidth)
            };
            renderWindow.Draw(axisX);
            RectangleShape axisY = new RectangleShape() {
                Position = new Vector2f(axisOffset, axisOffset),
                Size = new Vector2f(axisWidth, graphHeight)
            };
            renderWindow.Draw(axisY);

            //Calculate max
            int maxArg = 0;
            float maxValue = 0;
            foreach (Point p in points) {
                if (p.argument > maxArg) {
                    maxArg = p.argument;
                }
                if (p.value > maxValue) {
                    maxValue = p.value;
                }
            }

            //Lines and labels
            int stepX = graphWidth / lineCount;
            int stepY = graphHeight / lineCount;
            for(int i = 1; i <= lineCount; i++) {
                int y = (int)windowHeight - axisOffset - i * stepY;
                RectangleShape lineX = new RectangleShape() {
                    Position = new Vector2f(axisOffset, y),
                    Size = new Vector2f(graphWidth, lineWidth),
                    FillColor = Color.Red
                };
                renderWindow.Draw(lineX);
                float value = ((float)i / lineCount) * maxValue;
                Text label = new Text(value.ToString(), font) {
                    Position = new Vector2f(axisOffset / 2, y),
                    CharacterSize = 10
                };
                label.Position -= new Vector2f(label.GetLocalBounds().Width / 2, label.GetLocalBounds().Height / 2);
                renderWindow.Draw(label);
            }

            //Points
            foreach (Point p in points) {
                CircleShape c = new CircleShape() {
                    Position = new Vector2f(axisOffset + graphWidth * ((float)p.argument / maxArg), windowHeight - axisOffset - graphWidth * (p.value / maxValue)),
                    Radius = 5,
                    FillColor = Color.Yellow
                };
                c.Position -= c.Scale * c.Radius;
                renderWindow.Draw(c);
            }
        }

        class Point {
            public int argument;
            public float value;

            public Point(int argument, float value) {
                this.argument = argument;
                this.value = value;
            }
        }
    }
}
