using Godot;
using System.Collections.Generic;
public partial class Game : Node2D
{
  private CustomSignals _customSignals;
  PackedScene PointScene = GD.Load<PackedScene>("res://Scene/point.tscn");
  public override void _Ready()
  {
    GD.Print("Runnig");
    var output = new Godot.Collections.Array();
    string[] argExec = { "cursorpos" };
    OS.Execute("hyprctl", argExec, output);
    GD.Print(output);
    List<string> stringersList = new List<string>();
    if ((string)output[0] == "")
    {
      stringersList.Add("800");
      stringersList.Add("600");
    }
    else
    {
      string[] stringerFilt = ((string)output[0]).Split(",");
      stringersList.Add((string)stringerFilt[0]);
      stringersList.Add((string)stringerFilt[1]);
    }

    string[] stringers = stringersList.ToArray();

    GD.Print("Setted Stringers");
    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = new Vector2(float.Parse(stringers[0]), float.Parse(stringers[1]));
    pointInstance.tabla = "items";
    AddChild(pointInstance);

    GD.Print("Instance Point Node");
    _customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    _customSignals.Tablada += OnInstance;
    GD.Print("Connected to Signal Tablada");
  }
  private void OnInstance(string tabla, string icono)
  {

    Point pointInstance = PointScene.Instantiate<Point>();
    pointInstance.Position = GetViewport().GetMousePosition();
    pointInstance.tabla = tabla;
    pointInstance.icono_center = icono;
    AddChild(pointInstance);
  }
}
