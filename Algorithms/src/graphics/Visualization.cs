using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Algorithms {

    class Visualization : Window {

        public Visualization(Element[] elements) {
            this.elements = elements;
        }

        Element[] elements;

        protected override string GetWindowName() {
            return "Graph";
        }

        protected override void Update() {
            int max = 0;
            foreach (Element e in elements) {
                if (e.value > max)
                    max = e.value;
            }
            float step = elements.Length > windowWidth ? (float)elements.Length / (float)windowWidth : 1.0f;
            for (float k = 0; k < elements.Length; k += step) {
                int i = (int)k;
                float width = (float)windowWidth / (float)elements.Length;
                if (width < 1.0f) width = 1.0f;
                float height = (float)(elements[i].value * windowHeight) / (float)max;
                RectangleShape bar = new RectangleShape(new Vector2f(width, height));
                float x = i * windowWidth / elements.Length;
                float y = windowHeight - height;
                bar.Position = new Vector2f(x, y);
                bar.FillColor = Color.Yellow;
                renderWindow.Draw(bar);
            }
        }
    }
}
