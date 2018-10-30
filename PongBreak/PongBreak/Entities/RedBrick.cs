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

namespace PongBreak
{
    class RedBrick : Brick
    {
        CCSprite sprite;
        public RedBrick()
        {
            BrickSprite();
            AddChild(BrickSprite());
            this.ContentSize = BrickSprite().ContentSize;
        }

        public override CCSprite BrickSprite()
        {
            sprite = new CCSprite("redBrick.png");
            return sprite;
        }
    }
}