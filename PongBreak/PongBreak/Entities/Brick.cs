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
    class Brick : CCNode
    {
        public int Hits { get; set; } //how many times the brick has been hit by the ball
        
        public Brick(): base()
        {
            BrickSprite();
            AddChild(BrickSprite());
            this.ContentSize = BrickSprite().ContentSize;
            
            
            Hits = 0;
            HitsToBreak();
        }
        public virtual CCSprite BrickSprite()
        {
            return new CCSprite("greenBrick");
        }
        public virtual int HitsToBreak()
        {
            return 1;
        }
        public virtual bool IsHit() //bool to determine if a brick has been hit by the ball
        {
            Hits += 1; //increment hits counter
            if(Hits >= HitsToBreak())
            {
                return true; //return true, brick breaks
            }
            else
            {
                return false;
            }
        }
    }
}