﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tools_XNA_dotNET_Framework;

namespace PartiAppen
{
    // A class that manages pages and buttons inside those pages
    public class Menu
    {
        // List for all the pages in your desired menu
        public List<Page> Pages = new List<Page>();
        // A selection variable for pages
        public int PageSelection;

        // Constructor
        public Menu()
        { }
        // Constructor with desired amount of pages
        public Menu(int amountOfPages)
        {
            for (int i = 0; i < amountOfPages; i++)
            {
                Pages.Add(new Page());
            }
        }

        // State bool, when used, it checks if your current selection is on the same page and button, it is to make it easier to write what each button shall do in another class
        public bool State(int page, int button)
        {
            if (PageSelection == page)
            {
                if (Pages[page].ButtonSelection == button)
                {
                    return true;
                }
                
            }

            return false;

        }

        // Easy navigation of buttons and button states, takes in an input of up, down, left and right
        /*
        public void Navigation(bool up, bool down, bool left, bool right)
        {
            if (up)
            {
                // Make the selection go up in current page
                Pages[PageSelection].SelectUp(false);
            }
            else if (down)
            {
                // Make the selection go down in current page
                Pages[PageSelection].SelectDown(false);
            }
            else if (left)
            {
                // Make the selection change to left in current page's current button
                Pages[PageSelection].Buttons[Pages[PageSelection].ButtonSelection].SelectLeft(false);
            }
            else if (right)
            {
                // Make the selection change to right in current page's current button
                Pages[PageSelection].Buttons[Pages[PageSelection].ButtonSelection].SelectRight(false);
            }
        }
        */

        public void Update()
        {
            UpdateMouse(MenuManager.MousePosition);
        }

        // Update mouse collision with all buttons
        private void UpdateMouse(Point mousePosition)
        {
            Pages[PageSelection].UpdateMouse(mousePosition);
        }

        // Draw the current selected page
        public void Draw(SpriteBatch spriteBatch)
        {
            Pages[PageSelection].Draw(spriteBatch);
        }

    }

    // A class that handles texts, background and buttons
    public class Page
    {
        // Variable for custom background
        public Texture2D Background;
        // Variable for the backgrounds transparency
        public float BackgroundTransparency;
        // Images
        public List<Image> Images = new List<Image>();
        // Variable that handles Texts
        public List<Line> Text = new List<Line>();
        // Variable that handles buttons
        public List<Button> Buttons = new List<Button>();
        // A selection variable for buttons
        public int ButtonSelection;

        // Constructor
        public Page()
        {
        }
        // Constructor with a list of buttons
        public Page(List<Button> buttons)
        {
            Buttons = buttons;
        }

        // Method for adding a background to a page, insert texture and it's transparency value (in %)
        public void AddBackground(Texture2D texture, float transparency)
        {
            Background = texture;
            BackgroundTransparency = transparency;
        }

        public void AddImage(params Image[] image)
        {
            Images.AddRange(image);
        }

        // Add a text, which font it shall have, what position, if the position is centralized (origin based), what the text is and what color
        public void AddText(SpriteFont font, Vector2 position, bool center, string newText, Color color)
        {
            Text.Add(new Line(font, position, center, newText, color));
        }

        // Add a button with no switching state, takes in font, position and text
        public void AddButton_Single(Button button)
        {
            Buttons.Add(button);
        }

        // Add multiple buttons with no switching state, takes in font, position, the spacing between texts, and an array of text (each string in the array represent a state)
        public void AddButtonList_Single(SpriteFont font, Rectangle rectangle, float spacing, string[] texts, Vector2 textPadding, Color color, Color highlightedColor)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                Buttons.Add(new Button(font, new Rectangle(rectangle.X, (int)(rectangle.Y + i*spacing), rectangle.Width, rectangle.Height), texts[i], textPadding, color, highlightedColor));                
            }
        }


        // Update mouse collisions with buttons
        public void UpdateMouse(Point mousePosition)
        {
            // For every button
            for (int i = 0; i < Buttons.Count; i++)
            {
                // Check collisions
                if (Buttons[i].Rectangle.Contains(mousePosition))
                {
                    // The one that collides, make that one the selection
                    ButtonSelection = i;
                }
            }
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // If background have a texture, draw it across the screen.
            if (Background != null)
            {
                spriteBatch.Draw(Background, new Rectangle(0, 0, 1080, 720), new Color(new Vector4(BackgroundTransparency)));
            }

            // Draw every image
            foreach (Image image in Images)
            {
                image.Draw(spriteBatch);
            }

            // For every button, if i is same as selection, make that button have a highlight 
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].HighLight = i == ButtonSelection;
                // Draw buttons
                Buttons[i].Draw(spriteBatch);
            }

            // Draw every text in page
            for (int i = 0; i < Text.Count; i++)
            {
                Text[i].Draw(spriteBatch);
            }
        }

    }

    public class Image
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Color tint;

        /// <summary>
        /// Creates an image object with a tint
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="destinationRectangle">Position and size</param>
        /// <param name="tint"></param>
        public Image(Texture2D texture, Rectangle destinationRectangle, Color tint)
        {
            this.texture = texture;
            this.destinationRectangle = destinationRectangle;
            this.tint = tint;
        }

        /// <summary>
        /// Creates an image object
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="destinationRectangle">Position and size</param>
        public Image(Texture2D texture, Rectangle destinationRectangle)
        {
            this.texture = texture;
            this.destinationRectangle = destinationRectangle;
            tint = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, tint);
        }
    }

    // A class that simplifies a string with a font, position, if it is centered, what text and color
    public class Line
    {
        public SpriteFont Font;
        public Vector2 Position;
        public bool Center;
        public string Text;
        public Color Color;

        // Constructor
        public Line(SpriteFont font, Vector2 position, bool center, string text, Color color)
        {
            Font = font;
            Position = position;
            Center = center;
            Text = text;
            Color = color;
        }

        // Update a specific text
        public void UpdateLine(string text)
        {
            Text = text;
        }

        // Draw, if center then make adjustments so that position gets into origin, otherwise position is upper left corner of the string
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Center)
            {
                spriteBatch.DrawString(Font, Text, Position - new Vector2(Font.MeasureString(Text).X/2, Font.MeasureString(Text).Y/2), Color);
            }
            else
            {
                spriteBatch.DrawString(Font, Text, Position, Color);
            }
            
        }

    }

    // A class that simplifies a button with a font, position and what text
    public class Button
    {
        private SpriteFont font;
        // Upper left corner of the rectangle
        private Point Position => Rectangle.Location;
        private Vector2 VectorPosition => Rectangle.Location.ToVector2();
        // A rectangle for checking if it intersects with mouse
        public Rectangle Rectangle;
        // Origin for text
        private Vector2 textPadding;
        private string text;
        // A bool that declares if the button shall be highlighted (different color than basic)
        public bool HighLight;
        private Color highLightedColor;
        private Color color;

        // Constructor for button
        public Button(SpriteFont font, Rectangle rectangle, string text, Vector2 textPadding, Color color, Color highLightedColor)
        {
            this.font = font;
            Rectangle = rectangle;
            this.text = text;
            this.textPadding = textPadding;
            this.color = color;
            this.highLightedColor = highLightedColor;
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // Rectangle rectangle is text position & font measurement of text to length and height
            spriteBatch.DrawFilledRectangle(Rectangle, HighLight ? highLightedColor : color);
            
            // Draw text
            spriteBatch.DrawString(font, text, VectorPosition, Color.Black, 0f, textPadding, Vector2.One, SpriteEffects.None, 1);
        }
    }

}