using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using PongGameFront;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello there!");

var nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(800, 600),
    Title = "LearnOpenTK - Creating a Window",
    // This is needed to run on macos
    Flags = ContextFlags.ForwardCompatible,
};

// To create a new window, create a class that extends GameWindow, then call Run() on it.
using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
{
    window.Run();
}
