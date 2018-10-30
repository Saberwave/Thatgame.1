using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;

/* Ball class relates to the code and logic involved with creating a ball entity as well as
 * sets up the controls for the ball's movements. Inherits from CCNode.
 */
namespace PongBreak
{
    class Ball : CCNode
    {
        public CCSprite ballSprite;
       
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        
        //constructor for the ball entity
        public Ball(): base()
        {
            ballSprite = new CCSprite("ball"); //set the ball's sprite to the ball image in Content folder
            ballSprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(ballSprite);
            VelocityY = -200;
            this.ContentSize = ballSprite.ContentSize; //sets size of bounding box to the sprite's actual size
            this.Schedule(AddVelocity);
        }
        //AddVelocity moves the ball
        void AddVelocity(float time)
        {
            PositionX += VelocityX * time;
            PositionY += VelocityY * time;
        }

    }
}