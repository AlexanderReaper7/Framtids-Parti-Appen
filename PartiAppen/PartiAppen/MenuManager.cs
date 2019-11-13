using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Menues state = Menues.Menu;

        public Menues State
        {
            get => state;
            set
            {
                menu.CurrentPage = (int)value;
                state = value;
            }
        }

        public int MenuesAmount => Enum.GetNames(typeof(Menues)).Length;

        // Create menu pages
        Menu menu = new Menu(Enum.GetNames(typeof(Menues)).Length);

        public static Point MousePosition => Mouse.GetState().Position;

        public void LoadMenu(ContentManager content)
        {
            // Fonts
            SpriteFont menuFont = content.Load<SpriteFont>(@"Fonts/Main");

            // Color theme
            Color primary = new Color(3,169,244), primaryLight = new Color(103,218,255), primaryDark = new Color(0,122,193);
            Color backColor = Color.LightGray, highLightColor = primary;
            int headerPadding = 300;
            //Rectangle mainRec = new Rectangle(0, 1080 - 80 * (MenuesAmount - 1), 720, 80);
            Rectangle mainRec = new Rectangle(0, 626, 720, 80);
            Rectangle logoRect = new Rectangle(720 / 2 - (586 / 2), 20, 586, 586);
            Vector2 padding = new Vector2(-20);
            Texture2D logo = content.Load<Texture2D>(@"Images/logo");

            // BackButton is in all pages but menu
            #region BackButton

            Texture2D backArrow = content.Load<Texture2D>(@"Images/1x/baseline_arrow_back_black_48dp");

            for (int i = 1; i < MenuesAmount; i++) // skip first page ( menu )
            {
                // Icon TODO: change rectangle
                menu.Pages[i].AddImageButton(new ImageButton(backArrow, new Rectangle(new Point(20), new Point(80)), () => SetMenuState(Menues.Menu)));
            }

            #endregion

            #region Menu
            // Logo
            menu.Pages[(int)Menues.Menu].AddImage(new Image(logo, new Rectangle(720/2 - (586 / 2), 20, 586,586)));
            // Buttons
            menu.Pages[(int)Menues.Menu].AddButtonList(menuFont, mainRec, 80f, new[] { "Vårt program", "Om oss", "Press" }, padding, backColor, highLightColor, new Action[] {() => SetMenuState(Menues.Program), () => SetMenuState(Menues.OmOss), () => SetMenuState(Menues.Press)});
            #endregion

            #region Program
            menu.Pages[(int)Menues.Program].AddButton(new Button(menuFont, new Rectangle(100, 100, 100, 100), "Vad tycker vi om X?", padding, backColor, highLightColor, () => SetMenuState(Menues.Menu)));
            #endregion

            #region OmOss
            #endregion

            #region Press
            #endregion
        }

        private void SetMenuState(Menues newState) { State = newState; }

        public void Update()
        {
            menu.Update();

            // Menu specific logic
            switch (State)
            {
                case Menues.Menu:
                    break;
                case Menues.Program:
                    break;
                case Menues.OmOss:
                    break;
                case Menues.Press:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
