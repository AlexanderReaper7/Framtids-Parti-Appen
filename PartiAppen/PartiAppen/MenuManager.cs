using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PartiAppen
{
    class MenuManager
    {
        public enum Menues
        {
            Menu,
            Program,
            OmOss,
            Press
        }

        public static Menues state = Menues.Menu;

        // Create menu pages
        Menu menu = new Menu(Enum.GetNames(typeof(Menues)).Length);

        public SpriteFont menuFont;

        public static MouseState MouseState => Mouse.GetState();
        public static Point MousePosition => Mouse.GetState().Position;
        public static bool mouseNotPressed = true;

        public static bool ContainsMouse(Rectangle rectangle)
        {
            return rectangle.Contains(MousePosition);
        }

        public void LoadMenu(ContentManager Content)
        {
            // Fonts
            menuFont = Content.Load<SpriteFont>(@"Fonts/Main");

            // Colortheme
            Color b = Color.LightGray, h = Color.LightBlue;
            int headerPadding = 300;
            Rectangle mainRec = new Rectangle(0, headerPadding, 720, 80);
            Vector2 mainPad = new Vector2(-20);

            #region Menu
            menu.Pages[(int)Menues.Menu].AddButtonList_Single(menuFont, mainRec, 80f, new[] { "Vårt program", "Om oss", "Press" }, mainPad, b, h);
            #endregion

            #region Program
            menu.Pages[(int)Menues.Program].AddButton_Single(new Button(menuFont, new Rectangle(100, 100, 100, 100), "Meny", mainPad, b, h));
            menu.Pages[(int)Menues.Program].AddButton_Single(new Button(menuFont, new Rectangle(100, 200, 100, 100), "Lol", mainPad, b, h));

            #endregion

            #region OmOss
            #endregion

            #region Press
            #endregion
        }

        public void Update()
        {
            

            menu.Update();

            switch (state)
            {
                case Menues.Menu:
                    // switch menu when left mouse button is pressed
                    if (mouseNotPressed && MouseState.LeftButton == ButtonState.Pressed)
                    {
                        for (int i = 0; i < Enum.GetNames(typeof(Menues)).Length; i++)
                        {
                            // Check if player select first button in main menu (play)
                            if (menu.State(0, i))
                            {
                                // Change page to selected screen
                                menu.PageSelection = i + 1;
                            }

                        }


                        // Make single activation press reset
                        mouseNotPressed = false;
                    }
                    break;
                case Menues.Program:
                    // switch menu when left mouse button is pressed
                    if (mouseNotPressed && MouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (menu.State(1, 0))
                        {
                            // Change page to Menu screen
                            menu.PageSelection = 0;
                        }



                        // Make single activation press reset
                        mouseNotPressed = false;
                    }
                    break;
                case Menues.OmOss:
                    break;
                case Menues.Press:
                    break;
            }
            

            
        

            


            mouseNotPressed = MouseState.LeftButton == ButtonState.Released;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            menu.Draw(spriteBatch);
        }
    }
}
