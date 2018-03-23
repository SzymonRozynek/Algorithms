using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Algorithms {
    class Table : Window {

        private int width;
        private List<string[]> values;
        Font font;
        private int xSpace = 200;
        private int ySpace = 30;

        public Table(int width, int height) {
            this.width = width;
            windowWidth = (uint)(width * xSpace);
            windowHeight = (uint)((height + 1) * ySpace);
            values = new List<string[]>();
            font = new Font("fonts\\arial.ttf");
        }

        public void AddLine(string[] line) {
            if (line.Length != width) {
                throw new Exception("Incorrect line width");
            }
            values.Add(line);
        }

        protected override string GetWindowName() {
            return "Table";
        }

        protected override void Update() {
            for(int x = 1; x < width; x++) {
                RectangleShape r = new RectangleShape() {
                    Position = new Vector2f(((float)x / width) * windowWidth, 0.0f),
                    FillColor = Color.Yellow,
                    Size = new Vector2f(3.0f, windowHeight)
                };
                renderWindow.Draw(r);
            }
            for (int y = 0; y < values.Count; y++) {
                string[] line = values.ElementAt(y);
                for (int x = 0; x < width; x++) {
                    Text t = new Text(line[x], font) {
                        Position = new Vector2f(xSpace / 2 + x * xSpace, ySpace/2 + y * ySpace),
                        Color = Color.Yellow,
                    };
                    float field = 0.8f * xSpace;
                    if(t.GetLocalBounds().Width > field) {
                        t.Scale = new Vector2f(field / t.GetLocalBounds().Width, field / t.GetLocalBounds().Width);
                    }
                    t.Position -= new Vector2f(t.GetLocalBounds().Width * t.Scale.X / 2, t.GetLocalBounds().Height * t.Scale.Y / 2);
                    renderWindow.Draw(t);
                }
            }
        }
    }
}
