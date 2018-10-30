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
using Microsoft.Xna.Framework.Graphics;
//game scene is where the game scene is set up using CCScene. This is where backgrounds and such are drawn
//utilises tiled backgrounds for the sake of simplicity. The plan is to make several tileable background images
//such that different levels have a unique tiled background.
namespace PongBreak
{
    public class GameScene : CCScene
    {

        public GameScene(CCWindow window) : base(window)
        {
            var backgroundLayer = new CCLayer();
           // CreateBackground(window, backgroundLayer);
            //AddChild(backgroundLayer);
        }

        private void CreateBackground(CCWindow window, CCLayer backgroundLayer)
        {
            var texture = new CCTexture2D("redSoilTile.png");
            texture.SamplerState = SamplerState.LinearWrap;
            var background = new CCSprite(texture);
            background.ContentSize = new CCSize(window.WindowSizeInPixels.Width, window.WindowSizeInPixels.Height);
            background.TextureRectInPixels = new CCRect(0, 0, window.WindowSizeInPixels.Width, window.WindowSizeInPixels.Height);
            background.PositionX = window.WindowSizeInPixels.Width / 2;
            background.PositionY = window.WindowSizeInPixels.Height / 2;
            backgroundLayer.AddChild(background);
        }
    }
}