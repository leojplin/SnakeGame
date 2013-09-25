using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;

namespace SnakeGame
{
    class MenuButton
    {
        public Vector2 position;
        public int height;
        public int width;
        public Texture2D texure;
        public Color btnColor;
        public bool btnIntersect;
        public SnakeGame.Buttons btnKind;
        public static List<MenuButton> btnList = new List<MenuButton>();
        MouseState oldMouseState = Mouse.GetState();




        public MenuButton(Texture2D texure, GraphicsDevice device, int index)
        {
            btnKind = (SnakeGame.Buttons)index;
            btnIntersect = false;
            btnColor = Color.White;
            height = texure.Height;
            width = texure.Width;
            position.X = device.Viewport.Width / 2 - width / 2;
            position.Y = device.Viewport.Height / 2 - height - 100 + index * height * 2;
            this.texure = texure;
            

            btnList.Add(this);
        }
        public MenuButton(Texture2D texure, GraphicsDevice device, int index, Vector2 pos)
        {
            btnKind = (SnakeGame.Buttons)index;
            btnIntersect = false;
            btnColor = Color.White;
            height = texure.Height;
            width = texure.Width;
            position = pos;
            this.texure = texure;
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuButton button in btnList)
                spriteBatch.Draw(button.texure, button.position, button.btnColor);
        }

        public void drawSingleButton(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.Draw(texure, pos, btnColor);
        }

        public void updateSingleButton(MouseState mouse)
        {
            Rectangle buttonRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            Rectangle cursorRect = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (buttonRect.Intersects(cursorRect))
            {
                btnColor = Color.WhiteSmoke;
                btnIntersect = true;
            }
            else
            {
                btnColor = Color.White;
                btnIntersect = false;
            }

            if (btnIntersect && mouse.LeftButton == ButtonState.Released && oldMouseState.LeftButton
                                                                 == ButtonState.Pressed)
            {
                switch ((int)btnKind)
                {
                    case 4:
                        SnakeGame.state = SnakeGame.GameState.Paused;
                        break;
                    case 5:
                        SnakeGame.state = SnakeGame.GameState.Resumed;
                        break;
                }
            }

            oldMouseState = mouse;
        }


        public static void Update(MouseState mouse)
        {
            foreach (MenuButton button in btnList)
            {
                Rectangle buttonRect = new Rectangle((int)button.position.X, (int)button.position.Y, button.width, button.height);
                Rectangle cursorRect = new Rectangle(mouse.X, mouse.Y, 1, 1);

                if (buttonRect.Intersects(cursorRect))
                {
                    button.btnColor = Color.WhiteSmoke;
                    button.btnIntersect = true;
                }
                else
                {
                    button.btnColor = Color.White;
                    button.btnIntersect = false;
                }

                if (button.btnIntersect && mouse.LeftButton == ButtonState.Pressed )
                {
                    switch ((int)button.btnKind)
                    {
                        case 1:
                            SnakeGame.state = SnakeGame.GameState.InGame;
                            break;
                        case 2:
                            SnakeGame.state = SnakeGame.GameState.Help;
                            break;
                        case 3:
                            SnakeGame.state = SnakeGame.GameState.Quit;
                            break;

                    }
                }
            }
        }

    }
}
