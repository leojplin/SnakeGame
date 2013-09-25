using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework;
using ANX.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SnakeGame
{
    class Apple
    {
        static Random random = new Random();
        public Vector2 position;
        Texture2D texture;
        public bool isEaten;
        

        public Apple(ContentManager content, string s)
        {
            texture = content.Load<Texture2D>(s);
            position = new Vector2(Apple.random.Next(30), Apple.random.Next(20));
            isEaten = false;
            
        }      

        public void Update()
        {
            if (Snake.snakeBodyList[0].position == position) isEaten = true;
            if (isEaten)
            {

                position = new Vector2(random.Next(30), random.Next(20));
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X*30 ,position.Y*30 +100), Color.White);
        }
    }
}
