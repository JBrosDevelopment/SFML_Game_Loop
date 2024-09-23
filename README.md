# SFML Game Loop

Install with Nuget:
> `dotnet add package SFML_Game_Loop`

A simple game loop class system that adds Start and Update functions to your SFML.Net project

```cs
using SFML.Graphics;
using SFML.Window;
using SFML_Game_Loop;

public static class Program
{
    public static void Main(string[] args)
    {
        MainMenu mainMenu = new();
        mainMenu.BeginRoutine();
    }
}

class MainMenu : WindowRoutine
{
    public MainMenu() :
        base(new VideoMode(600, 400), "Main Menu", Styles.Default, new Color(255, 255, 255))
    {
    }

    public override void Start()
    {
        button = new Button(
            position: new Vector2f(Center.X - 250, Center.Y - 75),
            size: new Vector2f(500, 180),
            color: new Color(125, 125, 125, 125),
            fontColor: new Color(225, 225, 225),
            text: "PLAY!",
            charSize: 48);
        button.Clicked += ButtonClicked;
        Elements.Add(button);
    }

    public override void Update(double delta)
    {

    }
}
```
