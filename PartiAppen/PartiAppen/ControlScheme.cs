using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PartiAppen
{
    // A class that update all the different bools from inputs, easily make a control scheme, also handles the mouse position/intersection on screen
    public class ControlScheme
    {
        public bool SelectMouse { get; set; }

        public bool IsKeyUp { get; set; }

        public Rectangle MousePointer { get; set; }
        public Rectangle PreviousMousePosition { get; set; }
        

        public void Update()
        {
            PreviousMousePosition = MousePointer;
            MousePointer = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            
            
            // MouseSelection
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                SelectMouse = true;
            else SelectMouse = false;
            

            // Looks if any of the navigation keys are up, this can be used as a single activation key
            if (!SelectMouse)
            {
                IsKeyUp = true;
            }
            else
            {
                IsKeyUp = false;
            }

        }

        // alpha is used to make the texture fade away, it's a cool but unnecessary effect
        public void DrawMouse(SpriteBatch spriteBatch, Texture2D mouseTexture, float alpha)
        {
            spriteBatch.Draw(mouseTexture, new Vector2(MousePointer.X, MousePointer.Y), new Color(new Vector4(alpha)));
        }


    }
}
