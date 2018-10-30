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
    public class LevelTwo : CCLayer
    {
        Ball ball; //ball entity
        Paddle p1Paddle;//paddle entities for each player
        //each row of bricks is separate, this allows for creating multiple rows of bricks
        //using different brick sprites for each row if neceessary
        List<RedBrick> bricksRow1;//array of brick entities. a row of bricks
        List<RedBrick> bricksRow2;
        List<RedBrick> bricksRow3;
        List<SilverBrick> bricksRow4;
        List<RedBrick> bricksRow5;
        List<SilverBrick> bricksRow6;
        List<CCRect> bricksBoundingBoxRow1;
        List<CCRect> bricksBoundingBoxRow2;
        List<CCRect> bricksBoundingBoxRow3;
        List<CCRect> bricksBoundingBoxRow4;
        List<CCRect> bricksBoundingBoxRow5;
        List<CCRect> bricksBoundingBoxRow6;
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
        public LevelTwo()
        {
            //display the score as a label

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
        void CreateBricks(List<CCRect> rects, List<RedBrick> bricksList, int amount, float x, float y)
        {

            for (int i = 0; i < amount; i++)
            {
                bricksList.Add(new RedBrick());
                bricksList[i].PositionX = 100 + i * x;
                bricksList[i].PositionY = y;
                AddChild(bricksList[i]);
                rects.Add(new CCRect());
                rects[i] = bricksList[i].BoundingBoxTransformedToParent;
            }
        }
        void CreateSilverBricks(List<CCRect> rects, List<SilverBrick> bricksList, int amount, float x, float y)
        {

            for (int i = 0; i < amount; i++)
            {
                bricksList.Add(new SilverBrick());
                bricksList[i].PositionX = 100 + i * x;
                bricksList[i].PositionY = y;
                AddChild(bricksList[i]);
                rects.Add(new CCRect());
                rects[i] = bricksList[i].BoundingBoxTransformedToParent;
            }
        }
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
            var texture = new CCTexture2D("redSoilTile.png");
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
            gameOver.Position = bounds.Center;
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
            bricksRow1 = new List<RedBrick>();
            bricksRow2 = new List<RedBrick>();
            bricksRow3 = new List<RedBrick>();
            bricksRow4 = new List<SilverBrick>();
            bricksRow5 = new List<RedBrick>();
            bricksRow6 = new List<SilverBrick>();
            CreateBricks(bricksBoundingBoxRow1, bricksRow1, 9, 155, 2000);
            CreateBricks(bricksBoundingBoxRow2, bricksRow2, 9, 155, 2080);
            CreateBricks(bricksBoundingBoxRow3, bricksRow3, 9, 155, 1840);
            CreateSilverBricks(bricksBoundingBoxRow4, bricksRow4, 9, 155, 1920);
            CreateBricks(bricksBoundingBoxRow5, bricksRow5, 9, 155, 1760);
            CreateSilverBricks(bricksBoundingBoxRow6, bricksRow6, 9, 155, 2160);

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
        void HandleBrickCollisions(List<CCRect> rects, List<RedBrick> bricks)
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
        void HandleSilverBrickCollisions(List<CCRect> rects, List<SilverBrick> silverBricks)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                bool doesBallOverlapBrick = ballBoundingBox.IntersectsRect(rects[i]);
                if (doesBallOverlapBrick)
                {
                    hitCount++;
                    ball.VelocityY *= -1;

                    if (silverBricks[i].IsHit())
                    {
                        RemoveChild(silverBricks[i]);
                        rects[i] = new CCRect();
                        p1Score += 2; //increment score by two when player breaks a silver brick
                    }
                    if (hitCount >= 2)
                    {
                        ball.VelocityY *= -1;
                        hitCount = 0;
                    }
                }
            }
        }
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
        void GameOver(bool isGameOver)
        {
            if (isGameOver)
            {
                CCLabel label = new CCLabel("Tap to return to Level Select", "Arial", 50, CCLabelFormat.SystemFont);
                label.PositionX = 400;
                label.PositionY = 1000;
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
                GameOver(isGameOver);
                return;
            }
            else if (p1Score == 72) //this is the maximum score possible, thus is the victory condition
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
            HandleSilverBrickCollisions(bricksBoundingBoxRow4, bricksRow4);
            HandleBrickCollisions(bricksBoundingBoxRow5, bricksRow5);
            HandleSilverBrickCollisions(bricksBoundingBoxRow6, bricksRow6);
            //ball hitting paddle logic
            HandlePaddleCollisions(p1BoundingBox, ballBoundingBox, ball);
            //ball hitting wall logic
            HandleWallCollisions(ballBoundingBox, ball);
            debugLabel.Text = string.Format("Lives: {0} Score: {1}", lives, p1Score);
        }
        public static CCScene LvlTwoScene(CCWindow mainWindow)
        {
            var scene = new CCScene(mainWindow);
            var layer = new LevelTwo();
            scene.AddChild(layer);
            return scene;
        }

    }
}