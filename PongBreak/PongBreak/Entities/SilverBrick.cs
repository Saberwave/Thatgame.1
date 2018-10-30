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
    class SilverBrick : Brick
    {
        CCSprite sprite;
        public SilverBrick()
        {
            BrickSprite();
            AddChild(BrickSprite());
            this.ContentSize = BrickSprite().ContentSize;
            HitsToBreak();
            Hits = 0;
            changeSprite();
        }
        //override hitsToBreak from Brick to 2, this means that silver bricks takes 2 hits to break instead of 1
        public override int HitsToBreak()
        {
            return 2;
        }
       
        
        public override CCSprite BrickSprite()
        {
            sprite = new CCSprite("silverBrick.png");
            if(Hits == 1)
            {
                sprite = changeSprite();
                return sprite;
            }
            else
                return sprite;
        }
        //for some reason the sprite doesn't change after the first hit
        public CCSprite changeSprite()
        {
            return new CCSprite("silverBrickCracked.png");
        }
    }
}