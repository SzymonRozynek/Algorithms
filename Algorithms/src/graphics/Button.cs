using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Algorithms {
     class Button {

        public Button(Texture tex) {
            texture = tex;
            Sprite = new Sprite(texture);
        }

        private Sprite sprite;
        private Texture texture;
        private bool clicked = false;

        public Sprite Sprite { get => sprite; set => sprite = value; }

        public void Update(OnClick onClick, RenderWindow window) {
            if(Mouse.IsButtonPressed(Mouse.Button.Left) && !clicked && sprite.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y)) {
                onClick();
                clicked = true;
            }
            else if(!Mouse.IsButtonPressed(Mouse.Button.Left)) {
                clicked = false;
            }
        }

        public void Draw(RenderWindow w) {
            w.Draw(sprite);
        }

        public delegate void OnClick();
    }
}
