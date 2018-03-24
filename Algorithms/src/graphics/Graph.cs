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
        private const int axisOffset = 70;
        private const int lineWidth = 1;
        private const int lineCount = 20;
        private const int legendWidth = 200;
        private Color[] graphColors = { Color.Yellow, Color.Blue, Color.Cyan, Color.Magenta };

        private Font font;
        private string xLabel;
        private string yLabel;

        public Graph(string xLabel, string yLabel) {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            windowWidth = 600 + legendWidth;
            windowHeight = 600;
            datas = new List<Data>();
            font = new Font("fonts\\arial.ttf");
        }

        List<Data> datas;

        protected override string GetWindowName() {
            return "Graph";
        }

        public void AddData(Data data) {
            datas.Add(data);
        }

        protected override void Update() {

            int graphWidth = (int)windowWidth - 2 * axisOffset - legendWidth;
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

            //Axis labels
            float maxWidth = 100;
            Text xt = new Text(xLabel, font) {
                Position = new Vector2f(axisOffset + graphWidth, windowHeight - axisOffset - 20),
                CharacterSize = 20
            };
            xt.Origin = new Vector2f(xt.GetLocalBounds().Width / 2, xt.GetLocalBounds().Height / 2);
            xt.Scale = xt.GetLocalBounds().Width > maxWidth ? (new Vector2f(1.0f, 1.0f)) * (maxWidth / xt.GetLocalBounds().Width) : new Vector2f(1.0f, 1.0f);
            renderWindow.Draw(xt);
            Text yt = new Text(yLabel, font) {
                Position = new Vector2f(axisOffset, axisOffset - 20),
                CharacterSize = 20
                
            };
            yt.Origin = new Vector2f(yt.GetLocalBounds().Width / 2, yt.GetLocalBounds().Height / 2);
            yt.Scale = yt.GetLocalBounds().Width > maxWidth ? (new Vector2f(1.0f, 1.0f)) * (maxWidth / yt.GetLocalBounds().Width) : new Vector2f(1.0f, 1.0f);
            renderWindow.Draw(yt);

            //Calculate max
            int maxArg = 0;
            float maxValue = 0;
            foreach (Data data in datas) {
                foreach (Point p in data.points) {
                    if (p.argument > maxArg) {
                        maxArg = p.argument;
                    }
                    if (p.value > maxValue) {
                        maxValue = p.value;
                    }
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
                Text label = new Text(value.ToString("0.00"), font) {
                    Position = new Vector2f(axisOffset / 2, y),
                    CharacterSize = 14
                };
                label.Position -= new Vector2f(label.GetLocalBounds().Width / 2, label.GetLocalBounds().Height);
                renderWindow.Draw(label);
            }
            for (int i = 1; i <= lineCount; i++) {
                int x = axisOffset + i * stepX;
                RectangleShape lineY = new RectangleShape() {
                    Position = new Vector2f(x, axisOffset),
                    Size = new Vector2f(lineWidth, graphHeight),
                    FillColor = Color.Red,                                   
                };
                renderWindow.Draw(lineY);
                float value = ((float)i / lineCount) * maxArg;
                Text label = new Text(value.ToString(), font) {
                    Position = new Vector2f(x, graphHeight + axisOffset + 10),
                    CharacterSize = 14,
                    Rotation = 90.0f
                };
                label.Position += new Vector2f(label.GetLocalBounds().Height / 2, 0.0f);
                label.Origin = new Vector2f(0.0f, label.GetLocalBounds().Height/2);
                renderWindow.Draw(label);
            }

            //Points
            int colorCount = 0;
            float labelY = axisOffset;
            foreach (Data data in datas) {

                //Legend
                int legendsOffset = 20;
                CircleShape col = new CircleShape() {
                    FillColor = graphColors[colorCount],
                    Position = new Vector2f(axisOffset + graphWidth + legendsOffset, labelY),
                    Radius = 8
                };
                Text label = new Text(data.title, font) {
                    Position = new Vector2f(axisOffset + graphWidth + legendsOffset + 2*col.Radius + 10, labelY),
                    CharacterSize = 15
                };
                labelY += col.Radius*2.5f;
                renderWindow.Draw(col);
                renderWindow.Draw(label);

                foreach (Point p in data.points) {
                    CircleShape c = new CircleShape() {
                        Position = new Vector2f(axisOffset + graphWidth * ((float)p.argument / maxArg), windowHeight - axisOffset - graphHeight * (p.value / maxValue)),
                        Radius = 4,
                        FillColor = graphColors[colorCount]
                    };
                    c.Position -= c.Scale * c.Radius;
                    renderWindow.Draw(c);
                }
                colorCount++;
            }
        }

        public class Data {
            public List<Point> points;
            public string title;

            public Data(string title) {
                this.title = title;
                points = new List<Point>();
            }

            public void AddPoint(int argument, float value) {
                points.Add(new Point(argument, value));
            }
        }

        public class Point {
            public int argument;
            public float value;

            public Point(int argument, float value) {
                this.argument = argument;
                this.value = value;
            }
        }
    }
}
