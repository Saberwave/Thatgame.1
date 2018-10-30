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

namespace PongBreak
{
    class Paddle : CCNode
    {
        public CCSprite paddleSprite;
        
        public Paddle(): base()
        {
            paddleSprite = new CCSprite("redpaddle");
            paddleSprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(paddleSprite);
            this.ContentSize = paddleSprite.ContentSize; //sets bounding box size to actual sprite size
        }

    }
}