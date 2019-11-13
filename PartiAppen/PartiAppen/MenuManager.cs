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
            SpriteFont subFont = content.Load<SpriteFont>(@"Fonts/Sub");
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

            Texture2D backArrowBlack = content.Load<Texture2D>(@"Images/2x/baseline_arrow_back_black_48dp");
            Texture2D backArrowWhite = content.Load<Texture2D>(@"Images/2x/baseline_arrow_back_white_48dp");


            for (int i = 1; i < MenuesAmount; i++) // skip first page ( menu )
            {
                // Icon TODO: change rectangle
                menu.Pages[i].AddImageButton(new ImageButton(backArrowWhite, new Rectangle(new Point(20, 20), new Point(80)),logoBlue,logoYellow , () => SetMenuState(Menues.Menu)));
            }

            #endregion

            #region Menu
            // Logo
            menu.Pages[(int)Menues.Menu].AddImage(new Image(logo, logoRect));
            // Buttons
            menu.Pages[(int)Menues.Menu].AddButtonList(menuFont, mainRec, 80f, new[] { "Vårt program", "Om oss", "Press" }, padding, logoBlue, logoYellow, new Action[] {() => SetMenuState(Menues.Program), () => SetMenuState(Menues.OmOss), () => SetMenuState(Menues.Press)});
            
            menu.Pages[(int)Menues.Menu].AddText(textFont, new Vector2(20, 540 + 20), false, 
                "Våran sikt är att människan är en gruppvarelse därmed ska samhället " + System.Environment.NewLine
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

            // Variable to change where the text is.
            int yOffSet = 360;

            //menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false, "", Color.Black);
            
            // Buttons for shortcut
            menu.Pages[(int)Menues.Program].AddButton(new Button(menuFont, new Rectangle(100, 100, 100, 100), "Vad tycker vi om X?", padding, backColor, highLightColor, () => SetMenuState(Menues.Menu)));


            // Klimat
            menu.Pages[(int)Menues.Program].AddText(menuFont, new Vector2(20, yOffSet + 0), false, "Klimat", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 50), false,
                "Vi tror på en säker och energieffektiv framtid. Med nya teknologier " + System.Environment.NewLine
                + "kan vi utvinna energi från jord. Vi vill forska och utveckla detta " + System.Environment.NewLine
                + "område för säkrare och billigare elproduktion. ", Color.Black);
            // sub Kärnkraft
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 160), false, "Kärnkraft", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 200), false,
                "Kärnkraft extremt energieffektivt och har liten klimatpåverkan med en " + System.Environment.NewLine
                + "extremt liten chans för katastrof. Sverige får idag 40% av sin " + System.Environment.NewLine
                + "energi från kärnkraft vi vill expandera det till 50% under de närmaste " + System.Environment.NewLine
                + "20 åren när vindkraftverken som utgör 10% av vår energi går sönder och " + System.Environment.NewLine
                + "behöves ersättas. Dem sista 50% procenten består av 41% vatten som " + System.Environment.NewLine
                + "är effektivt men för dyrt för att vara effektivt att expandera och " + System.Environment.NewLine
                + "9% värmekraft som kommer från energin vi får av att återanvända sopor.", Color.Black);
            // sub Forskning
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 400), false, "Forskning", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 440), false,
                "Med dagens teknologi så klarar jorden inte mer än 8 miljarder människor " + System.Environment.NewLine
                + "och vi är på en bana mot 10 miljarder där det kommer stanna in. " + System.Environment.NewLine
                + "För att lösa detta så krävs det att vi inoverar och kommer på " + System.Environment.NewLine
                + "nya mer miljövänliga lösningar. Vilket kräver bättre utbildning och " + System.Environment.NewLine
                + "mer pengar investerat på forskning.", Color.Black);


            // Skolan
            menu.Pages[(int)Menues.Program].AddText(menuFont, new Vector2(20, yOffSet + 0), false, "Skolan", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false,
                "Skolan är verktyget som formar samhället, detta ska våra stadgar reflektera genom att fortsätta erbjuda gratis skolgång och uppmuntra till en högre utbildning på högskole- eller universitetsnivå. Detta är en viktig pelare i demokratin, en välutbildad befolkning fattar bättre beslut.", Color.Black);
            // sub Friskolan
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 0), false, "Friskolan", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false,
                "Vi som parti är för friskolor för det skapar konkurrens vilket leder till bättre tjänster. Staten ska dock styra kunskapskraven samt leda inspektioner av skolorna för att säkerställa att friskolorna uppnår kraven som sätts på dem.", Color.Black);
            // sub Lärarbrist
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 0), false, "Lärarbrist", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false,
                "Läraryrket har under lång tid förlorat mer och mer status på grund av lägre löner och lägre krav för att människor utan lärarutbildning ska kunna få yrket. Detta har lett till att fler och fler utbildar sig till andra yrken vilket har lett till en brist på 60,000 lärare. Till att börja med vill vi öka lärarlöner så att fler ska vilja få jobbet för att låg lön är en motivation mot att inte bli lärare. Ett annat problem för lärare är alla jobb vid sidan av undervisningen dem behöver göra. Vi vill fokusera på att anställa människor som kan göra dem jobben så lärarna inte behöver det.", Color.Black);

            // Ekonomi
            menu.Pages[(int)Menues.Program].AddText(menuFont, new Vector2(20, yOffSet + 0), false, "Ekonomi", Color.Black);
            // sub Skatt
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 0), false, "Skatt", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false,
                "Det nuvarande systemet har visat att fungera. Vi tycker att skatten skall vara balanserad som vi anser att den är just nu. Därmed vill vi behålla skattesystemet och fokusera på att finjustera våra resurstillgångar med att investera dem på ett korrekt sätt.", Color.Black);
            // sub Bidrag
            menu.Pages[(int)Menues.Program].AddText(subFont, new Vector2(20, yOffSet + 0), false, "Bidrag", Color.Black);

            menu.Pages[(int)Menues.Program].AddText(textFont, new Vector2(20, yOffSet + 0), false,
                "Vi tror på forskning och därför vill vi ge större summa pengar till alla Sveriges fantastiska universitet. Vi skall lägga ytterligare 20 miljarder kronor på forskningsmedel och stöd för universitet. Bidragen kommer vara mer inriktad mot energiforskning. Genom dessa bidrag så skall vi uppfylla vår profilfråga!", Color.Black);

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

            // Update mouse
            previousMouseScroll = mouseScroll;
            mouseScroll = Mouse.GetState().ScrollWheelValue;

            int deltaScroll = previousMouseScroll - mouseScroll;

            // Menu specific logic
            switch (State)
            {
                case Menues.Menu:
                    // reset camera
                    Game1.camera.Position = Vector2.Zero;
                    break;
                case Menues.Program:
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
