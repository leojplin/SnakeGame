using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SnakeGame
{
    class Snake
    {
        public enum Direction { left, right, up, down };

        Direction currentDirection = Direction.right;
        float waitTime = .15f;
        float waitedTime = 0f;
        public static List<SnakeBody> snakeBodyList;
        Texture2D texture;
        Apple apple;
        Grid grid;
        ContentManager content;


        public Snake(ContentManager content)
        {
            texture = content.Load<Texture2D>("snake");
            snakeBodyList = new List<SnakeBody>(3)
            {
                new SnakeBody(texture,new Vector2(12,10)),
                new SnakeBody(texture,new Vector2(11,10)),
                new SnakeBody(texture,new Vector2(10,10)),
            };
            apple = new Apple(content, "apple");
            grid = new Grid(content);
            this.content = content;
        }

        public void Update(GameTime gameTime, Direction direction)
        {
            #region asd
            waitedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            waitTime = .3f - (float)(snakeBodyList.Count / 80f);
            if (waitedTime >= waitTime)
            {
                waitedTime = 0f;
                Vector2 tempPos = snakeBodyList[0].position;

                bool changingDirection = false;
                if (currentDirection != direction)
                {
                    if ((currentDirection == Direction.left || currentDirection == Direction.right) != (direction == Direction.left || direction == Direction.right))
                    {
                        if ((currentDirection == Direction.up || currentDirection == Direction.down) != (direction == Direction.up || direction == Direction.down))
                        {
                            changingDirection = true;
                            currentDirection = direction;
                        }
                    }
                }
                switch (currentDirection)
                {
                    case Direction.left:
                        if (changingDirection && (snakeBodyList[1].position.Y != snakeBodyList[0].position.Y))
                            snakeBodyList[0].position.X -= 1;
                        else if (!changingDirection) snakeBodyList[0].position.X -= 1;
                        break;
                    case Direction.right:
                        if (changingDirection && (snakeBodyList[1].position.Y != snakeBodyList[0].position.Y))
                            snakeBodyList[0].position.X += 1;
                        else if (!changingDirection) snakeBodyList[0].position.X += 1;
                        break;
                    case Direction.up:
                        if (changingDirection && (snakeBodyList[1].position.X != snakeBodyList[0].position.X))
                            snakeBodyList[0].position.Y -= 1;
                        else if (!changingDirection) snakeBodyList[0].position.Y -= 1;
                        break;
                    case Direction.down:
                        if (changingDirection && (snakeBodyList[1].position.X != snakeBodyList[0].position.X))
                            snakeBodyList[0].position.Y += 1;
                        else if (!changingDirection) snakeBodyList[0].position.Y += 1;
                        break;
                }
            #endregion

                apple.Update();
                if (apple.isEaten)
                {
                    addSnakeBody();
                    apple.isEaten = false;
                    SnakeGame.scorePoints += snakeBodyList.Count;
                }

                for (int i = snakeBodyList.Count - 1; i > 0; i--)
                {
                    if (i == 1) snakeBodyList[i].position = tempPos;
                    else
                        snakeBodyList[i].position = snakeBodyList[i - 1].position;
                }

                changingDirection = false;


            }

            if (isSnakeDead())
            {
                SnakeGame.state = SnakeGame.GameState.GameOver;
                snakeBodyList = new List<SnakeBody>(3)
                {
                    new SnakeBody(texture,new Microsoft.Xna.Framework.Vector2(12,10)),
                    new SnakeBody(texture,new Microsoft.Xna.Framework.Vector2(11,10)),
                    new SnakeBody(texture,new Microsoft.Xna.Framework.Vector2(10,10)),
                };
                currentDirection = Direction.right;
                apple = new Apple(content, "apple");
                SnakeGame.scorePoints = 0;
                

            }


        }

        private bool isSnakeDead()
        {
            return snakeBodyList[0].position.X == -1 || snakeBodyList[0].position.X == 30 ||
                snakeBodyList[0].position.Y == -1 || snakeBodyList[0].position.Y == 20 || snakeCollidedItself();
        }

        private bool snakeCollidedItself()
        {
            for (int i = 1; i < snakeBodyList.Count; i++)
            {

                if (snakeBodyList[0].position == snakeBodyList[i].position) return true;
            }

            return false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            foreach (SnakeBody s in snakeBodyList)
            {
                s.Draw(spriteBatch);
            }
            apple.Draw(spriteBatch);
        }

        public void addSnakeBody()
        {
            snakeBodyList.Add(new SnakeBody(texture,
                new Vector2(snakeBodyList[snakeBodyList.Count - 1].position.X, snakeBodyList[snakeBodyList.Count - 1].position.Y)));

        }

    }
}
