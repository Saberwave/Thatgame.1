using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocosSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//this is where hit detection logic and brick layout for the first level is defined
//inherits from CCLayer
// each row of bricks and their CCrects are defined in a separate list
namespace PongBreak
{
    public class LevelThree : CCLayer
    {
        Ball ball; //ball entity
        Paddle p1Paddle;//paddle entities for each player
        //each row of bricks is separate, this allows for creating multiple rows of bricks
        //using different brick sprites for each row if neceessary
        List<Brick> bricksRow1;//array of brick entities. a row of bricks
        List<Brick> bricksRow2;
        List<Brick> bricksRow3;
        List<Brick> bricksRow4;
        List<Brick> bricksRow5;
        List<Brick> bricksRow6;
        List<Brick> bricksRow7;
        List<Brick> bricksRow8;
        List<Brick> bricksRow9;
        List<Brick> bricksRow10;
        List<Brick> bricksRow11;
        List<Brick> bricksRow12;
        List<Brick> bricksRow13;
        List<Brick> bricksRow14;
        List<Brick> bricksRow15;
        List<Brick> bricksRow16;
        List<Brick> bricksRow17;
        List<Brick> bricksRow18;
        List<Brick> bricksRow19;
        List<Brick> bricksRow20;
        List<Brick> bricksRow21;
        List<Brick> bricksRow22;
        List<CCRect> bricksBoundingBoxRow1;
        List<CCRect> bricksBoundingBoxRow2;
        List<CCRect> bricksBoundingBoxRow3;
        List<CCRect> bricksBoundingBoxRow4;
        List<CCRect> bricksBoundingBoxRow5;
        List<CCRect> bricksBoundingBoxRow6;
        List<CCRect> bricksBoundingBoxRow7;
        List<CCRect> bricksBoundingBoxRow8;
        List<CCRect> bricksBoundingBoxRow9;
        List<CCRect> bricksBoundingBoxRow10;
        List<CCRect> bricksBoundingBoxRow11;
        List<CCRect> bricksBoundingBoxRow12;
        List<CCRect> bricksBoundingBoxRow13;
        List<CCRect> bricksBoundingBoxRow14;
        List<CCRect> bricksBoundingBoxRow15;
        List<CCRect> bricksBoundingBoxRow16;
        List<CCRect> bricksBoundingBoxRow17;
        List<CCRect> bricksBoundingBoxRow18;
        List<CCRect> bricksBoundingBoxRow19;
        List<CCRect> bricksBoundingBoxRow20;
        List<CCRect> bricksBoundingBoxRow21;
        List<CCRect> bricksBoundingBoxRow22;
        CCLabel debugLabel;
        CCLabel gameOver;
        CCRect bounds;
        CCSprite backGround;
        int hitCount = 0; //hitCount is set up to prevent the ball from maintaining the same velocity when it destroys 2 bricks at once
        int lives = 3; //life counter of the player. Game over occurs if lives <= 0
        int p1Score = 0, highScore = 0; //placeholder ints for the score system
        // IntroLayer shows the introductory layer of which is displayed to users
        CCRect ballBoundingBox, p1BoundingBox;
        bool isGameOver = false;
        public LevelThree()
        {
            

            Schedule(RunGameLogic);
        }
        void CreateLabels()
        {
            debugLabel = new CCLabel("Score = ", "Arial", 40, CCLabelFormat.SystemFont);
            debugLabel.PositionX = 50;
            debugLabel.PositionY = 2500;
            debugLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            AddChild(debugLabel);

            gameOver = new CCLabel("Game Over", "Arial", 60, CCLabelFormat.SystemFont);
            gameOver.Visible = false;
            AddChild(gameOver);

        }
        //method to create list of bricks and list of CCRects tied to the bricks
        //takes list of CCRects, list of bricks, amount of bricks, and the desires x/y values as position points
        void CreateBricks(List<CCRect> rects, List<Brick> bricksList, int amount, float x, float y, float offset)
        {

            for (int i = 0; i < amount; i++)
            {
                bricksList.Add(new Brick());
                bricksList[i].PositionX = offset + i * x;
                bricksList[i].PositionY = y;
                AddChild(bricksList[i]);
                rects.Add(new CCRect());
                rects[i] = bricksList[i].BoundingBoxTransformedToParent;
            }
        }
        //as there are no silverbricks in level 3, removed the silver brick creation code

        /* Reference CreateBackGround
         * Purpose: creates the background for the level
         * Date: 29/10/2018
         * Source: Youtube
         * Author: Luke Briers
         * url: https://www.youtube.com/watch?v=dcLAj4MqeYs&t=1877s
         * Adaption required: changed variable names, changed method arguments
         * Code is shown at approx. 31 minute mark
         * 
         */
        public void CreateBackGround(CCRect boundary)
        {
            var texture = new CCTexture2D("spaceTile.png");
            texture.SamplerState = SamplerState.LinearWrap;
            backGround = new CCSprite(texture);
            backGround.ContentSize = new CCSize(boundary.MaxX, boundary.MaxY);
            backGround.TextureRectInPixels = new CCRect(0, 0, boundary.MaxX, boundary.MaxY);
            backGround.PositionX = boundary.MaxX / 2;
            backGround.PositionY = boundary.MaxY / 2;
            AddChild(backGround);
        }
        //end reference CreateBackGround
        protected override void AddedToScene()
        {
            base.AddedToScene();
            // Use the bounds to layout the positioning of our drawable assets

            Schedule(RunGameLogic);
            bounds = VisibleBoundsWorldspace;
            CreateBackGround(bounds);

            debugLabel = new CCLabel("Score = ", "Arial", 40, CCLabelFormat.SystemFont);
            debugLabel.PositionX = 50;
            debugLabel.PositionY = 2500;
            debugLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            AddChild(debugLabel);

            gameOver = new CCLabel("Game Over", "Arial", 80, CCLabelFormat.SystemFont);
            gameOver.Visible = false;
            AddChild(gameOver);
            gameOver.PositionY = 900;
            gameOver.PositionX = bounds.MaxX / 2;
            ball = new Ball();
            //ball should be approx middle of screen
            ball.PositionX = bounds.MaxX / 2;
            ball.PositionY = 1500;
            AddChild(ball);

            //declare where exactly on the screen the paddle will be spawned
            p1Paddle = new Paddle();
            p1Paddle.PositionX = bounds.MaxX / 2;
            p1Paddle.PositionY = 100;
            AddChild(p1Paddle);
            //initalise array of bricks and array of CCRects for each brick
            //each row of bricks contains 9 bricks per row
            bricksBoundingBoxRow1 = new List<CCRect>();
            bricksBoundingBoxRow2 = new List<CCRect>();
            bricksBoundingBoxRow3 = new List<CCRect>();
            bricksBoundingBoxRow4 = new List<CCRect>();
            bricksBoundingBoxRow5 = new List<CCRect>();
            bricksBoundingBoxRow6 = new List<CCRect>();
            bricksBoundingBoxRow7 = new List<CCRect>();
            bricksBoundingBoxRow8 = new List<CCRect>();
            bricksBoundingBoxRow9 = new List<CCRect>();
            bricksBoundingBoxRow10 = new List<CCRect>();
            bricksBoundingBoxRow11 = new List<CCRect>();
            bricksBoundingBoxRow12 = new List<CCRect>();
            bricksBoundingBoxRow13 = new List<CCRect>();
            bricksBoundingBoxRow14 = new List<CCRect>();
            bricksBoundingBoxRow15 = new List<CCRect>();
            bricksBoundingBoxRow16 = new List<CCRect>();
            bricksBoundingBoxRow17 = new List<CCRect>();
            bricksBoundingBoxRow18 = new List<CCRect>();
            bricksBoundingBoxRow19 = new List<CCRect>();
            bricksBoundingBoxRow20 = new List<CCRect>();
            bricksBoundingBoxRow21 = new List<CCRect>();
            bricksBoundingBoxRow22 = new List<CCRect>();
            bricksRow1 = new List<Brick>();
            bricksRow2 = new List<Brick>();
            bricksRow3 = new List<Brick>();
            bricksRow4 = new List<Brick>();
            bricksRow5 = new List<Brick>();
            bricksRow6 = new List<Brick>();
            bricksRow7 = new List<Brick>();
            bricksRow8 = new List<Brick>();
            bricksRow9 = new List<Brick>();
            bricksRow10 = new List<Brick>();
            bricksRow11 = new List<Brick>();
            bricksRow12 = new List<Brick>();
            bricksRow13 = new List<Brick>();
            bricksRow14 = new List<Brick>();
            bricksRow15 = new List<Brick>();
            bricksRow16 = new List<Brick>();
            bricksRow17 = new List<Brick>();
            bricksRow18 = new List<Brick>();
            bricksRow19 = new List<Brick>();
            bricksRow20 = new List<Brick>();
            bricksRow21 = new List<Brick>();
            bricksRow22 = new List<Brick>();
            CreateBricks(bricksBoundingBoxRow1, bricksRow1, 2, 700, 2300, 350);
            CreateBricks(bricksBoundingBoxRow2, bricksRow2, 2, 700, 2230, 350);
            CreateBricks(bricksBoundingBoxRow3, bricksRow3, 2, 400, 2160, 500);
            CreateBricks(bricksBoundingBoxRow4, bricksRow4, 2, 400, 2090, 500);
            CreateBricks(bricksBoundingBoxRow5, bricksRow5, 6, 155, 2020, 310);
            CreateBricks(bricksBoundingBoxRow6, bricksRow6, 6, 155, 1950, 310);
            CreateBricks(bricksBoundingBoxRow7, bricksRow7, 2, 155, 1880, 155);
            CreateBricks(bricksBoundingBoxRow8, bricksRow8, 2, 155, 1880, 620);
            CreateBricks(bricksBoundingBoxRow9, bricksRow9, 2, 155, 1880, 1085);
            CreateBricks(bricksBoundingBoxRow10, bricksRow10, 2, 155, 1810, 155);
            CreateBricks(bricksBoundingBoxRow11, bricksRow11, 2, 155, 1810, 620);
            CreateBricks(bricksBoundingBoxRow12, bricksRow12, 2, 155, 1810, 1085);
            CreateBricks(bricksBoundingBoxRow13, bricksRow13, 9, 155, 1740, 105);
            CreateBricks(bricksBoundingBoxRow14, bricksRow14, 9, 155, 1670, 105);
            CreateBricks(bricksBoundingBoxRow15, bricksRow15, 5, 155, 1600, 415);
            CreateBricks(bricksBoundingBoxRow16, bricksRow16, 2, 620, 1530, 415);
            CreateBricks(bricksBoundingBoxRow17, bricksRow17, 2, 620, 1460, 415);
            CreateBricks(bricksBoundingBoxRow18, bricksRow18, 2, 400, 1390, 520);
            CreateBricks(bricksBoundingBoxRow19, bricksRow19, 2, 1240, 1600, 105);
            CreateBricks(bricksBoundingBoxRow20, bricksRow20, 2, 1240, 1530, 105);
            CreateBricks(bricksBoundingBoxRow21, bricksRow21, 2, 1240, 1460, 105);
            CreateBricks(bricksBoundingBoxRow22, bricksRow22, 2, 400, 1320, 520);
            
            var accel = new CCEventListenerAccelerometer();
            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            AddEventListener(touchListener, this);
            //set up a schedule to run the game logic multiple times per second
            Schedule(RunGameLogic);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }
        //method handles moving the paddle. We *NEED* to change to Accelerometer controls as soon as possible
        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            //this code moves the paddle. it will need to be changed for accelerometer controls
            var locationOnScreen = touches[0].Location;
            p1Paddle.PositionX = locationOnScreen.X;
        }
        //method to handle what occurs when the ball collides with a brick
        //the brick will be destroyed on collision and the ball's y velocity gets inverted
        void HandleBrickCollisions(List<CCRect> rects, List<Brick> bricks)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                bool doesBallOverlapBrick = ballBoundingBox.IntersectsRect(rects[i]);
                if (doesBallOverlapBrick)
                {
                    hitCount++;
                    ball.VelocityY *= -1;
                    p1Score += 1;
                    if (bricks[i].IsHit())
                    {
                        RemoveChild(bricks[i]);
                        rects[i] = new CCRect(); //this essentially destroys the CCRect
                    }

                    if (hitCount >= 2)
                    {
                        ball.VelocityY *= -1;
                        hitCount = 0;
                    }
                }
            }
        }
        //since there is no silverbricks in level 3, removed the silverBrick collision code
        void HandlePaddleCollisions(CCRect paddleBox, CCRect ballBox, Ball ballRep)
        {
            bool doesBallOverlapPaddle = ballBox.IntersectsRect(paddleBox);
            bool isMovingDown = ballRep.VelocityY < 0;

            if (doesBallOverlapPaddle && isMovingDown)
            {
                //invert velocity
                ballRep.VelocityY *= -1;
                //assign a value to the x velocity. Keeping constant speed
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                ballRep.VelocityX = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity); // randomizes the xVelocity
                //reset hit counter whenever the ball hits anything that isn't a brick
                hitCount = 0;
            }
        }
        //method to handle what occurs when the ball intersects the left, right, top and bottom of the screen
        void HandleWallCollisions(CCRect ballBox, Ball ballRep)
        {
            //the following pertains to keeping the ball within the screen boundaries
            //get ball current pos
            float ballRight = ballBox.MaxX;
            float ballLeft = ballBox.MinX;
            //calculate edges of the screen for edge screen hit detection
            float screenRight = VisibleBoundsWorldspace.MaxX;
            float screenLeft = VisibleBoundsWorldspace.MinX;
            //check if ball is hitting the right or left side of screen
            bool shouldReflectXVelocity =
                (ballRight > screenRight && ballRep.VelocityX > 0) ||
                (ballLeft < screenLeft && ballRep.VelocityX < 0);
            if (shouldReflectXVelocity)
            {
                ballRep.VelocityX *= -1;
                hitCount = 0;
            }
            if (ballRep.PositionY < 0)
            {
                lives--; //decrease lives by 1
                hitCount = 0;
                if (lives == 0)
                {
                    gameOver.Visible = true;
                    isGameOver = true;
                    return;
                }
                else
                {
                    ballRep.PositionX = 720;
                    ballRep.PositionY = 1500;
                    ballRep.VelocityX = 0;
                }
            }
            if (ballRep.PositionY > VisibleBoundsWorldspace.MaxY)
            {
                ballRep.VelocityY *= -1;
                hitCount = 0;
            }
        }
        void GameOver(bool isGameOver, CCRect bounds)
        {
            if (isGameOver)
            {
                CCLabel label = new CCLabel("Tap to return to Level Select", "Arial", 50, CCLabelFormat.SystemFont);
                label.PositionX = 400;
                label.PositionY = 800;
                label.AnchorPoint = CCPoint.AnchorUpperLeft;
                AddChild(label);

                var touchListener = new CCEventListenerTouchAllAtOnce();
                touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene(LevelSelect.GetLevelSelectScene(Window));
                AddEventListener(touchListener, this);
                highScore = p1Score;
            }
        }
        //LevelComplete displays the highscore and provides means to return to Level Select once th elevel is complete
        void LevelComplete(CCRect boundary)
        {
            highScore = p1Score;
            CCLabel label = new CCLabel("Level Complete!", "Arial", 60, CCLabelFormat.SystemFont);
            label.Position = boundary.Center;
            label.AnchorPoint = CCPoint.AnchorMiddle;
            label.Text = string.Format("Level Complete! Highscore: {0}", highScore);
            AddChild(label);
            CCLabel tapToReturn = new CCLabel("Tap to return to Level Select", "Arial", 50, CCLabelFormat.SystemFont);
            tapToReturn.PositionX = boundary.MaxX / 2;
            tapToReturn.PositionY = 1000;
            AddChild(tapToReturn);
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene(LevelSelect.GetLevelSelectScene(Window));
            AddEventListener(touchListener, this);

        }
        //this method runs the game logic, such as ball physics, collision methods, etc
        void RunGameLogic(float frameTimeInSeconds)
        {

            if (lives == 0)
            {
                isGameOver = true;
                GameOver(isGameOver, bounds);
                return;
            }
            else if (p1Score == 69) //this is the maximum score possible, thus is the victory condition
            {
                LevelComplete(bounds);
                return;
            }
            ball.PositionY += ball.VelocityY * frameTimeInSeconds;
            ballBoundingBox = ball.BoundingBoxTransformedToParent; // bb for ball
            p1BoundingBox = p1Paddle.BoundingBoxTransformedToParent; //bb for p1Paddle

            bool doesBallOverlapPaddle = ballBoundingBox.IntersectsRect(p1BoundingBox);
            //display score and lives

            //call handling brick collisions here
            HandleBrickCollisions(bricksBoundingBoxRow1, bricksRow1);
            HandleBrickCollisions(bricksBoundingBoxRow2, bricksRow2);
            HandleBrickCollisions(bricksBoundingBoxRow3, bricksRow3);
            HandleBrickCollisions(bricksBoundingBoxRow4, bricksRow4);
            HandleBrickCollisions(bricksBoundingBoxRow5, bricksRow5);
            HandleBrickCollisions(bricksBoundingBoxRow6, bricksRow6);
            HandleBrickCollisions(bricksBoundingBoxRow7, bricksRow7);
            HandleBrickCollisions(bricksBoundingBoxRow8, bricksRow8);
            HandleBrickCollisions(bricksBoundingBoxRow9, bricksRow9);
            HandleBrickCollisions(bricksBoundingBoxRow10, bricksRow10);
            HandleBrickCollisions(bricksBoundingBoxRow11, bricksRow11);
            HandleBrickCollisions(bricksBoundingBoxRow12, bricksRow12);
            HandleBrickCollisions(bricksBoundingBoxRow13, bricksRow13);
            HandleBrickCollisions(bricksBoundingBoxRow14, bricksRow14);
            HandleBrickCollisions(bricksBoundingBoxRow15, bricksRow15);
            HandleBrickCollisions(bricksBoundingBoxRow16, bricksRow16);
            HandleBrickCollisions(bricksBoundingBoxRow17, bricksRow17);
            HandleBrickCollisions(bricksBoundingBoxRow18, bricksRow18);
            HandleBrickCollisions(bricksBoundingBoxRow19, bricksRow19);
            HandleBrickCollisions(bricksBoundingBoxRow20, bricksRow20);
            HandleBrickCollisions(bricksBoundingBoxRow21, bricksRow21);
            HandleBrickCollisions(bricksBoundingBoxRow22, bricksRow22);
            
            //ball hitting paddle logic
            HandlePaddleCollisions(p1BoundingBox, ballBoundingBox, ball);
            //ball hitting wall logic
            HandleWallCollisions(ballBoundingBox, ball);
            debugLabel.Text = string.Format("Lives: {0} Score: {1}", lives, p1Score);
        }
        public static CCScene LvlThreeScene(CCWindow mainWindow)
        {
            var scene = new CCScene(mainWindow);
            var layer = new LevelThree();
            scene.AddChild(layer);
            return scene;
        }

    }
}