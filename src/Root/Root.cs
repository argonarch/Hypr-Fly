using Godot;
using System.IO;

public partial class Root : Node2D
{
  [Signal]
  public delegate void CreaterPointsEventHandler(string tabla, string icono);

  PackedScene PointScene = GD.Load<PackedScene>("res://src/Point/Point.tscn");
  public string entorno;
  public override void _Ready()
  {
    if (!File.Exists(LiteData.HyprFlyDB))
    {
      LiteData.RestoreBackup();
    }
    GD.Print("Start Game");
    GD.Print("Config Dir: ", Globals.HyprFlyDir);
    string isFlatpak = OS.GetEnvironment("FLATPAK_ID");

    var output = new Godot.Collections.Array();
    float[] positionMouse = new float[2];
    if (isFlatpak != null)
    {
      GD.Print("Run in Flatpak");
      Globals.Environment = "Flatpak";
      OS.Execute("flatpak-spawn", new string[] { "--host", "hyprctl", "cursorpos" }, output);
    }
    else
    {
      GD.Print("Run in Desktop");
      Globals.Environment = "Linux";
      OS.Execute("hyprctl", new string[] { "cursorpos" }, output);
    }
    string outputString = string.Join("", output);
    positionMouse = System.Array.ConvertAll(outputString.Split(","), float.Parse);
    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = new Vector2(positionMouse[0], positionMouse[1]);
    AddChild(pointInstance);

    CreaterPoints += OnInstance;
  }

  private void OnInstance(string tabla, string icono)
  {
    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = GetViewport().GetMousePosition();
    pointInstance.Tabla = tabla;
    AddChild(pointInstance);
  }

}
