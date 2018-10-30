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
/* LevelSelect class serves as a level selector for the player, where they can choose
 * from multiple (3) levels. Touch listener is set up to listen for touches of the 
 * mutliple buttons which will link to a specific level depending on which is tapped
 */
namespace PongBreak
{
    public class LevelSelect : CCLayer
    {

        CCSprite button1, button2, button3, currentSpriteTouched;
        CCLabel levelSelectLabel;
      
        
        public LevelSelect(CCSize size) : base(size)
        {
            
        }
        public void createTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
           // touchListener.OnTouchesBegan = TouchBegan;
            AddEventListener(touchListener);
        }
        public void CreateButtons()
        {
            button1 = new CCSprite("blueButton.png");
            button2 = new CCSprite("redButton.png");
            button3 = new CCSprite("silverButton.png");
            
            button1.PositionX = 720;
            button1.PositionY = 1500;
            button2.PositionX = 720;
            button2.PositionY = 1200;
            button3.PositionX = 720;
            button3.PositionY = 900;
            AddChild(button1);
            AddChild(button2);
            AddChild(button3);
        }
       
        public void CreateText()
        {
            levelSelectLabel = new CCLabel("Choose a Level:", "Arial", 50, CCLabelFormat.SystemFont);
            levelSelectLabel.PositionX = 720.0f;
            levelSelectLabel.PositionY = 2000.0f;
            levelSelectLabel.Color = CCColor3B.White;
            AddChild(levelSelectLabel);
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();
            CCRect bounds = VisibleBoundsWorldspace;
            CreateButtons();
            CreateText();

            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = this.OnTouchesBegan;
            AddEventListener(touchListener, button1);
        }

        /* =============================================================
         * Reference OnTouchesBegan: externally sourced code
         * Purpose: to create touch logic on the sprite buttons in the level select screen
         * Date: 28/10/2018
         * Source: stackoverflow
         * Author: jaybers
         * url: https://stackoverflow.com/questions/33168953/android-game-drag-one-image-at-a-time-into-screen-from-a-group-of-images
         * Adaption required: provide links to each level as per the corresponding button
         * ==============================================================
         */
        bool OnTouchesBegan(CCTouch touch, CCEvent touchEvent)
        {
            CCSprite caller = touchEvent.CurrentTarget as CCSprite;
            currentSpriteTouched = null;
            if(caller == button1)
            {
                if(button1.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    //System.Diagnostics.Debug.WriteLine("Button pressed");
                    Window.DefaultDirector.ReplaceScene(LevelOne.LvlOneScene(Window));
                    return true;
                }
                else if(button2.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    Window.DefaultDirector.ReplaceScene(LevelTwo.LvlTwoScene(Window));
                    return true;
                }
                else if(button3.BoundingBoxTransformedToParent.ContainsPoint(touch.Location))
                {
                    Window.DefaultDirector.ReplaceScene(LevelThree.LvlThreeScene(Window));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //end reference OnTouchesBegan
       
        void GoToLevelOne()
        {
            
        }
        //method returns the level select screen. used in TitleLayer
        public static CCScene GetLevelSelectScene(CCWindow window)
        {
            CCScene scene = new CCScene(window);
            CCLayer layer = new LevelSelect(window.WindowSizeInPixels);
            scene.AddChild(layer);
            return scene;

        }

    }
}