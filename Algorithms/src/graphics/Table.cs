using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Algorithms {
    class Table : Window {

        private int width;
        private List<Tab> tabs;
        Font font;
        private int xSpace = 200;
        private int ySpace = 30;
        private int titleHeight = 100;

        private Button leftButton;
        private Button rightButton;
        private int buttonOffset = 50;

        private Tab selectedTab;

        public Table(int width, int height) {
            this.width = width;
            windowWidth = (uint)(width * xSpace);
            windowHeight = (uint)((height + 1) * ySpace + titleHeight);
            tabs = new List<Tab>();
            font = new Font("fonts\\arial.ttf");
            leftButton = new Button(new Texture("assets\\button.png"));
            leftButton.Sprite.Scale *= 0.05f;
            leftButton.Sprite.Position = new Vector2f(buttonOffset, titleHeight / 2);
            leftButton.Sprite.Origin = new Vector2f(leftButton.Sprite.GetLocalBounds().Width / 2, leftButton.Sprite.GetLocalBounds().Height / 2);
            leftButton.Sprite.Rotation = 180;
            rightButton = new Button(new Texture("assets\\button.png"));
            rightButton.Sprite.Scale *= 0.05f;
            rightButton.Sprite.Position = new Vector2f(windowWidth - buttonOffset, titleHeight / 2);
            rightButton.Sprite.Origin = new Vector2f(leftButton.Sprite.GetLocalBounds().Width / 2, leftButton.Sprite.GetLocalBounds().Height / 2);
        }

        public void AddTab(Tab tab) {
            tabs.Add(tab);
            selectedTab = tab;
        }

        protected override string GetWindowName() {
            return "Table";
        }

        protected override void Update() {
            leftButton.Update(TabLeft, renderWindow);
            leftButton.Draw(renderWindow);
            rightButton.Update(TabRight, renderWindow);
            rightButton.Draw(renderWindow);
            Draw();
        }

        private void TabLeft() {
            int i = tabs.IndexOf(selectedTab);
            if(i == 0) {
                i = tabs.Count - 1;
            }
            else {
                i--;
            }
            selectedTab = tabs.ElementAt(i);
        }

        private void TabRight() {
            int i = tabs.IndexOf(selectedTab);
            if (i == tabs.Count - 1) {
                i = 0;
            } else {
                i++;
            }
            selectedTab = tabs.ElementAt(i);
        }

        private void Draw() {
            for (int x = 1; x < width; x++) {
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
                    CharacterSize = (uint)(titleHeight / 2)
                };
                float scale = titleText.GetGlobalBounds().Width > (windowWidth * 0.75) ? windowWidth * 0.75f / titleText.GetGlobalBounds().Width : 1.0f;
                titleText.Scale *= scale;
                titleText.Origin = new Vector2f(titleText.GetLocalBounds().Width / 2, titleText.GetLocalBounds().Height / 2);
                renderWindow.Draw(titleText);

                for (int y = 0; y < selectedTab.values.Count; y++) {
                    string[] line = selectedTab.values.ElementAt(y);
                    for (int x = 0; x < width; x++) {
                        if (line != null) {
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
