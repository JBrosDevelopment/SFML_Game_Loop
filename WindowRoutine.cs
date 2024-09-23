using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Game_Loop
{
    public class WindowRoutine
    {
        public RenderWindow Window { get; private set; }
        public Color BackgroundColor = new Color(125, 125, 125);
        public List<Drawable> Elements = new();
        public Vector2f Center => new Vector2f(Window.Size.X / 2, Window.Size.Y / 2);
        public const string DefaultFontPath = ".\\Fonts\\Roboto\\Roboto-Regular.ttf";
        public WindowRoutine() { }
        public WindowRoutine(VideoMode videoMode, string title, Styles style = Styles.Default, Color? color = null)
        {
            var window = new RenderWindow(videoMode, title, style);
            window.Closed += Close;
            Window = window;
            BackgroundColor = color ?? BackgroundColor;
        }
        public void BeginRoutine()
        {
            Start();
            Clock clock = new Clock();
            
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(BackgroundColor);

                float deltaTime = clock.Restart().AsSeconds();
                Update(deltaTime);

                foreach (var element in Elements)
                {
                    if (element is Button b)
                    {
                        b.Draw(Window, RenderStates.Default);
                        if (b.IsMouseDown(Window)) 
                        {
                            b.Click();
                        }
                        else if (b.IsMouseOver(Window)) 
                        {
                            b.Highlight();
                        }
                        else 
                        {
                            b.Reset();
                        }
                    }
                    else
                    {
                        Window.Draw(element);
                    }
                }

                Window.Display();
            }
        }
        public virtual void Close(object? sender, EventArgs e) 
        {
            var window = sender as RenderWindow;
            window?.Close();
        }
        public virtual void Start() { }
        public virtual void Update(double delta) { }
    }
}
