using Raylib_cs;
using RayLibBase.GameplayLoop;

namespace RayLibBase;

enum GameScreen { LOGO, TITLE, GAMEPLAY, ENDING };

internal class Program
{
    public static GameScreen currentScreen { get; set; } = GameScreen.LOGO;
    public static Settings settings = new();

    private static IGamePhase? logoPhase;
    private static IGamePhase? gameplayPhase;
    private static IGamePhase? titlePhase;
    private static IGamePhase? endingPhase;

    private static IGamePhase GetGamePhase(GameScreen currentScreen)
    {
        return currentScreen switch
        {
            GameScreen.TITLE => titlePhase == null ? titlePhase = new TitlePhase(settings) : titlePhase,
            GameScreen.GAMEPLAY => gameplayPhase == null ? gameplayPhase = new GameplayPhase(settings) : gameplayPhase,
            GameScreen.ENDING => endingPhase == null ? endingPhase = new EndingPhase(settings) : endingPhase,
            _ => logoPhase == null ? logoPhase = new LogoPhase(settings) : logoPhase,
        };
    }

    static void Main(string[] args)
    {
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);

        Raylib.InitWindow(settings.Width, settings.Height, "Hello World");

        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(settings.TargetFPS);

        bool exitWindowRequested = false;
        bool exitWindow = false;

        IGamePhase currentGamePhase;

        // Main game loop
        while (!exitWindow)
        {
            if (Raylib.WindowShouldClose() || Raylib.IsKeyPressed(KeyboardKey.Escape))
                exitWindowRequested = true;

            if (exitWindowRequested)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Y)) exitWindow = true;
                else if (Raylib.IsKeyPressed(KeyboardKey.N)) exitWindowRequested = false;
            }

            currentGamePhase = GetGamePhase(currentScreen);

            currentGamePhase.Update();

            Raylib.BeginDrawing();

            if (exitWindowRequested)
            {
                Raylib.DrawRectangle(0, 100, settings.Width, 200, Color.Black);
                Raylib.DrawText("Do you really want to exit?", 40, 180, 30, Color.White);
                Raylib.DrawText("Press Y to confirm or N to cancel", 120, 200, 20, Color.LightGray);
            }
            else
            {
                currentGamePhase.Draw();
            }

            Raylib.EndDrawing();
        }

        logoPhase?.Unload();
        titlePhase?.Unload();
        gameplayPhase?.Unload();
        endingPhase?.Unload();
        Raylib.CloseWindow();
    }
}
