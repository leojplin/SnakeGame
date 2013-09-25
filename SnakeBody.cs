using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework;

namespace SnakeGame
{
    class SnakeBody
    {
        public Vector2 position;
        Texture2D texure;
        
        public SnakeBody(Texture2D texure, Vector2 position)
        {
            this.texure = texure;
            this.position = position;
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texure, new Vector2(position.X * 30,position.Y * 30 +100), Color.White);
        }
    }
}
