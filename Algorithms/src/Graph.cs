using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Algorithms {

    class Graph {

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        static void OnClose(object sender, EventArgs e) {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        public void Draw(Element[] elements) {

            RenderWindow app = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Graph");
            app.Closed += new EventHandler(OnClose);
            Color windowColor = new Color(0, 0, 0);

            while (app.IsOpen) {
                app.DispatchEvents();
                app.Clear(windowColor);
                Update(elements, app);
                app.Display();
            }
        }

        private void Update(Element[] elements, RenderWindow app) {
            int max = 0;
            foreach(Element e in elements) {
                if (e.value > max)
                    max = e.value;
            }

            for (int i = 0; i < elements.Length; i++) {
                float width = (float)WindowWidth / (float)elements.Length;
                float height = elements[i].value * WindowHeight / max;
                RectangleShape bar = new RectangleShape(new Vector2f(width, height));
                float x = i * WindowWidth / elements.Length;
                float y = WindowHeight - height;
                bar.Position = new Vector2f(x, y);
                bar.FillColor = Color.Yellow;
                app.Draw(bar);
            }
        }
    }
}
