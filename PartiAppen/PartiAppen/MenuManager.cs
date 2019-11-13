using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace PartiAppen
{
    class MenuManager
    {
        public enum Menues
        {
            Menu,
            Program,
            Politicians,
            History,
            About,
            Press,
            Contact
        }

        Menu menu = new Menu(6);

        public SpriteFont menuFont;

        public void LoadMenu(ContentManager Content)
        {
            // Fonts
            menuFont = Content.Load<SpriteFont>(@"Fonts/Main");


            // Menu
            menu.Pages[0].AddButtonList_Single(menuFont, new Vector2(60, 240), 100f, new[] { "Vårt program", "Vår historia", "Våra politiker", "Om oss", "Press", "Kontakt" });

            // Program

            // Historia

            // Politiker

            // Om Oss

            // Press

            // Kontakt
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            menu.Draw(spriteBatch);
        }
    }
}
