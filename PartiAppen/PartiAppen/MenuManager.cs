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

        /*
         * Meny      = menu.pages[0]
         * Program   = 1
         * Politiker = 2
         * Historia  = 3
         * Om Oss    = 4
         * Press     = 5
         * Kontakt   = 6
         */
        Menu menu = new Menu(6);

        public SpriteFont menuFont;

        public void LoadMenu(ContentManager Content)
        {
            // Fonts
            menuFont = Content.Load<SpriteFont>(@"Fonts/Main");

            menu.Pages[0].AddButtonList_Single(menuFont, new Vector2(60), 100f, new[] { "Play", "Highscore", "Options", "How To Play", "Credits", "Exit" });
        }
    }
}
