using Godot;
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

    // string[] stringers = ((string)output[0]).Split(",");
    string[] stringers = new string[2];
    stringers[0] = "800";
    stringers[1] = "600";

    string[] stringer_emply = { "" };

    if (stringers == stringer_emply)
    {
      stringers[0] = "800";
      stringers[1] = "600";
    }
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
