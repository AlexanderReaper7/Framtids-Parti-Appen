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
            SpriteFont textFont = content.Load<SpriteFont>(@"Fonts/Text");

            // Color theme
            Color primary = new Color(3,169,244), primaryLight = new Color(103,218,255), primaryDark = new Color(0,122,193);
            Color backColor = primaryLight, highLightColor = primary;
            Color logoBlue = new Color(4, 0, 91), logoYellow = new Color(252, 214, 3);
            Rectangle mainRec = new Rectangle(0, 1080 - 80 * (MenuesAmount - 1), 720, 80);
            Rectangle logoRect = new Rectangle(720 / 2 - (480 / 2), 20, 480, 480);
            Vector2 padding = new Vector2(-20);
            Texture2D logo = content.Load<Texture2D>(@"Images/logo");

            // BackButton is in all pages but menu
            #region BackButton

            Texture2D backArrow = content.Load<Texture2D>(@"Images/1x/baseline_arrow_back_black_48dp");

            for (int i = 1; i < MenuesAmount; i++) // skip first page ( menu )
            {
                // Icon TODO: change rectangle
                menu.Pages[i].AddImageButton(new ImageButton(backArrow, new Rectangle(new Point(720/2 - 80/2 - 5, 1080-80-40), new Point(80)), () => SetMenuState(Menues.Menu)));
            }

            #endregion

            #region Menu
            // Logo
            menu.Pages[(int)Menues.Menu].AddImage(new Image(logo, logoRect));
            // Buttons
            menu.Pages[(int)Menues.Menu].AddButtonList(menuFont, mainRec, 80f, new[] { "Vårt program", "Om oss", "Press" }, padding, backColor, highLightColor, new Action[] {() => SetMenuState(Menues.Program), () => SetMenuState(Menues.OmOss), () => SetMenuState(Menues.Press)});
            
            menu.Pages[(int)Menues.Menu].AddText(textFont, new Vector2(20, 540 + 20), false, "Våran sikt är att människan är en gruppvarelse därmed ska samhället " + System.Environment.NewLine
                + "samarbeta för att upplyfta alla människor, sverige ska vara världsledare " + System.Environment.NewLine
                + "som andra länder kan se upp till och ta efter. För att bli världsledare krävs " + System.Environment.NewLine
                + "det att sverige investerar i sin framtid, genom forskning och utbildning. " + System.Environment.NewLine + System.Environment.NewLine 
                + "Världen är mer sammankopplad än någonsin och internationellt " + System.Environment.NewLine
                + "samarbete är vägen till framtiden. För att sverige ska kunna samarbeta " + System.Environment.NewLine
                + "med alla länder så är det viktigt att vi ska fortsätta vara neutrala. " + System.Environment.NewLine
                + "Sveriges universitet ska samarbeta att utveckla teknologier som " + System.Environment.NewLine
                + "andra länder kan nyttja.", Color.Black);
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

        private int mouseScroll = 0, previousMouseScroll = 0;
        private const float scrollMultiplier = 0.6f;

        public void Update()
        {
            menu.Update();

            // Menu specific logic
            switch (State)
            {
                case Menues.Menu:
                    // reset camera
                    Game1.camera.Position = Vector2.Zero;
                    break;
                case Menues.Program:
                    // mouse scrolling
                    previousMouseScroll = mouseScroll;
                    mouseScroll = Mouse.GetState().ScrollWheelValue;

                    int deltaScroll = previousMouseScroll - mouseScroll;

                    // move camera
                    Game1.camera.Position = new Vector2(Game1.camera.Position.X, Game1.camera.Position.Y + (deltaScroll * scrollMultiplier));
                    break;
                case Menues.OmOss:
                    // reset camera
                    Game1.camera.Position = Vector2.Zero;
                    break;
                case Menues.Press:
                    // reset camera
                    Game1.camera.Position = Vector2.Zero;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
