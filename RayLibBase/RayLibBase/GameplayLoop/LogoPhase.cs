using Raylib_cs;

namespace RayLibBase.GameplayLoop;

internal class LogoPhase : IGamePhase
{
    private int frameCounter = 0;
    private Settings settings;

    public LogoPhase(Settings settings)
    {
        this.settings = settings;
    }

    public void Update()
    {
        frameCounter++;
        if (frameCounter > 120)
        {
            Program.currentScreen = GameScreen.TITLE;
        }
    }

    public void Draw()
    {
        Raylib.ClearBackground(Color.RayWhite);

        Raylib.DrawRectangle(settings.Width / 2 - 128, settings.Height / 2 - 128, 256, 256, Color.Black);
        Raylib.DrawRectangle(settings.Width / 2 - 112, settings.Height / 2 - 112, 224, 224, Color.RayWhite);
        Raylib.DrawText("raylib", settings.Width / 2 - 44, settings.Height / 2 + 48, 50, Color.Black);
    }

    public void Unload()
    {
    }
}
