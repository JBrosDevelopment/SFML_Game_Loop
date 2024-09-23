using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Game_Loop
{
    public class Button : Drawable
    {
        public RectangleShape Shape { get; set; }
        public Text Label { get; set; }
        public Color HighlightColor { get; set; }
        public Color ClickColor { get; set; }
        public Color ButtonColor { get; set; }
        public event EventHandler Clicked;

        public Vector2f Position
        {
            get => Shape.Position;
            set
            {
                Shape.Position = value;
                Label.Position = GetTextPosition(Shape, Label);
            }
        }
        public Vector2f Scale
        {
            get => Shape.Scale;
            set => Shape.Scale = value;
        }
        public Button() { }
        public Button(Vector2f position, Vector2f size, Color color, Color fontColor, string text, uint charSize, Font? font = null)
        {
            Shape = new RectangleShape()
            {
                Position = position,
                Size = size,
                FillColor = color
            };
            Label = new Text()
            {
                DisplayedString = text,
                CharacterSize = charSize,
                Font = font ?? new Font(WindowRoutine.DefaultFontPath),
                FillColor = fontColor,
            };
            Label.Position = GetTextPosition(Shape, Label);

            ButtonColor = Shape.FillColor;
            HighlightColor = new Color(ButtonColor.R, ButtonColor.G, ButtonColor.B, (byte)Math.Clamp(ButtonColor.A + 25, 0, 255) );
            ClickColor     = new Color(ButtonColor.R, ButtonColor.G, ButtonColor.B, (byte)Math.Clamp(ButtonColor.A + 50, 0, 225) );
        }
        public Button(RectangleShape shape, Text label, Color? highlightColor = null, Color? clickColor = null)
        {
            Label = label;
            Shape = shape;
            ButtonColor = shape.FillColor;
            HighlightColor =
                highlightColor ??
                new Color(ButtonColor.R, ButtonColor.G, ButtonColor.B, (byte)Math.Clamp(ButtonColor.A + 25, 0, 255) );
            ClickColor = 
                clickColor ?? 
                new Color(ButtonColor.R, ButtonColor.G, ButtonColor.B, (byte)Math.Clamp(ButtonColor.A + 50, 0, 225) );
        }
        public List<Drawable> AddButtonToElements(IEnumerable<Drawable> elements)
        {
            return [.. elements, Shape, Label];
        }
        public void Highlight() => Shape.FillColor = HighlightColor;
        public void Click() => Shape.FillColor = ClickColor;
        public void Reset() => Shape.FillColor = ButtonColor;

        public static Vector2f GetTextPosition(RectangleShape rect, Text text)
        {
            float rectWidth = rect.Size.X;
            float rectHeight = rect.Size.Y;

            FloatRect textRect = text.GetLocalBounds();

            float xPosition = rect.Position.X + (rectWidth / 2.0f) - (textRect.Width / 2.0f) - textRect.Left;
            float yPosition = rect.Position.Y + (rectHeight / 2.0f) - (textRect.Height / 2.0f) - textRect.Top;

            return new Vector2f(xPosition, yPosition);
        }
        public bool IsMouseOver(RenderWindow window)
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            FloatRect buttonBounds = Shape.GetGlobalBounds();
            return buttonBounds.Contains(mousePosition.X, mousePosition.Y);
        }
        public bool IsMouseDown(RenderWindow window, Mouse.Button button = Mouse.Button.Left)
        {
            bool clicked = IsMouseOver(window) && Mouse.IsButtonPressed(button);
            if (clicked) Clicked.Invoke(this, new EventArgs());
            return clicked;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Shape);
            target.Draw(Label);
        }
    }
}
