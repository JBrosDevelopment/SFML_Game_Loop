# SFML Game Loop

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
    }

    public override void Update(double delta)
    {

    }
}
```