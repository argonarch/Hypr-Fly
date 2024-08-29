using Godot;
using System.IO;

public partial class Game : Node2D
{
  [Signal]
  public delegate void TabladaEventHandler(string tabla, string icono);

  PackedScene PointScene = GD.Load<PackedScene>("res://Scene/point.tscn");
  public string entorno;
  public override void _Ready()
  {
    if (!File.Exists(LiteData.HyprFlyDB))
    {
      LiteData.RestoreBackup();
    }
    GD.Print("Start Game");
    GD.Print("Config Dir: ", Globals.HyprFlyDir);
    var outputBash = new Godot.Collections.Array();
    string bashCommand = "[ -n '$FLATPAK_SANDBOX_DIR' ] && echo '1' || echo '0'";
    string[] argsBash = { "-c", bashCommand };
    OS.Execute("/bin/bash", argsBash, outputBash);
    int isFlatpak = int.Parse(string.Join("", outputBash));

    var output = new Godot.Collections.Array();
    float[] positionMouse = new float[2];
    if (isFlatpak == 1)
    {
      GD.Print("Run in Flatpak");
      entorno = "flatpak";
      OS.Execute("flatpak-spawn", new string[] { "--host", "hyprctl", "cursorpos" }, output);
      string outputString = string.Join("", output);
      positionMouse = System.Array.ConvertAll(outputString.Split(","), float.Parse);
      //GD.Print(positionMouse[0], ", ", positionMouse[1]);
    }
    else
    {
      GD.Print("Run in Desktop");
      entorno = "desktop";
      OS.Execute("hyprctl", new string[] { "cursorpos" }, output);
      string outputString = string.Join("", output);
      positionMouse = System.Array.ConvertAll(outputString.Split(","), float.Parse);
      //GD.Print(positionMouse[0], ", ", positionMouse[1]);
    }

    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = new Vector2(positionMouse[0], positionMouse[1]);
    AddChild(pointInstance);

    Tablada += OnInstance;
  }

  private void OnInstance(string tabla, string icono)
  {
    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = GetViewport().GetMousePosition();
    pointInstance.Tabla = tabla;
    AddChild(pointInstance);
  }

}
