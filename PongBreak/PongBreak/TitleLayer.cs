using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using CocosSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
/* TitleLayer is a class that is immediately displayed tot he user when they open the app
 * It features simple touch handling so that the user can tap the screen to initiate the 
 * level select menu, and from there choose a level to play
 * 
 * 
 */
namespace PongBreak
{
    public class TitleLayer : CCLayer
    {
        CCLabel tapToStart, creators;
        CCSprite logo;
        public TitleLayer() : base()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene(LevelSelect.GetLevelSelectScene(Window));
            AddEventListener(touchListener, this);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            CreateText();
            logo = new CCSprite("Breakthrough logo.png");
            logo.PositionX = 720.0f;
            logo.PositionY = 2000.0f;
            AddChild(logo);

        }

        private void CreateText()
        {
            tapToStart = new CCLabel("Tap Screen to Start", "Arial", 50, CCLabelFormat.SystemFont);
            tapToStart.PositionX = 720.0f;
            tapToStart.PositionY = 1000.0f;
            tapToStart.Color = CCColor3B.White;
            AddChild(tapToStart);

            creators = new CCLabel("Created By: Jordan Duffield, Thomas Underwood, Dylan Aubert", "Arial", 40, CCLabelFormat.SystemFont);
            creators.PositionX = 720.0f;
            creators.PositionY = 100.0f;
            creators.Color = CCColor3B.White;
            AddChild(creators);
        }
       
    }
}