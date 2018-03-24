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
        private List<Tab> tabs;
        Font font;
        private int xSpace = 200;
        private int ySpace = 30;
        private int titleHeight = 100;

        private Tab selectedTab;

        public Table(int width, int height) {
            this.width = width;
            windowWidth = (uint)(width * xSpace);
            windowHeight = (uint)((height + 1) * ySpace + titleHeight);
            tabs = new List<Tab>();
            font = new Font("fonts\\arial.ttf");
        }

        public void AddTab(Tab tab) {
            tabs.Add(tab);
            selectedTab = tab; //temp
        }

        protected override string GetWindowName() {
            return "Table";
        }

        protected override void Update() {

            for(int x = 1; x < width; x++) {
                RectangleShape r = new RectangleShape() {
                    Position = new Vector2f(((float)x / width) * windowWidth, titleHeight),
                    FillColor = Color.Yellow,
                    Size = new Vector2f(3.0f, windowHeight)
                };
                renderWindow.Draw(r);
            }

            if (selectedTab != null) {

                Text titleText = new Text(selectedTab.title, font) {
                    Position = new Vector2f(windowWidth / 2, titleHeight / 2),
                    CharacterSize = (uint)(titleHeight/2)
                };
                titleText.Origin = new Vector2f(titleText.GetLocalBounds().Width / 2, titleText.GetLocalBounds().Height / 2);
                renderWindow.Draw(titleText);

                for (int y = 0; y < selectedTab.values.Count; y++) {
                    string[] line = selectedTab.values.ElementAt(y);
                    for (int x = 0; x < width; x++) {
                        Text t = new Text(line[x], font) {
                            Position = new Vector2f(xSpace / 2 + x * xSpace, ySpace / 2 + y * ySpace + titleHeight),
                            Color = Color.Yellow,
                        };
                        float field = 0.8f * xSpace;
                        if (t.GetLocalBounds().Width > field) {
                            t.Scale = new Vector2f(field / t.GetLocalBounds().Width, field / t.GetLocalBounds().Width);
                        }
                        t.Position -= new Vector2f(t.GetLocalBounds().Width * t.Scale.X / 2, t.GetLocalBounds().Height * t.Scale.Y / 2);
                        renderWindow.Draw(t);
                    }
                }
            }
        }

        public class Tab {
            public List<string[]> values;
            public string title;

            public Tab(string title) {
                this.title = title;
                values = new List<string[]>();
            }

            public void AddLine(string[] line) {
                values.Add(line);
            }
        }
    }
}
