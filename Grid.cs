using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ANX.Framework;

namespace SnakeGame
{
    class Grid
    {
        Texture2D vline;
        Texture2D hline;

        public Grid(ContentManager content)
        {
            vline = content.Load<Texture2D>("vline");
            hline = content.Load<Texture2D>("hline");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 900; i+=30)
            {
                spriteBatch.Draw(vline, new Vector2(i, 100), Color.White);
            }
            for (int i = 0; i < 600; i += 30)
            {
                spriteBatch.Draw(hline, new Vector2(0, i + 100), Color.White);
            }
        }
    }
}
