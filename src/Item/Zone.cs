using Godot;

public partial class Zone : Area2D
{
  public float Diametro { get; set; }

  CollisionShape2D zona_collion = new CollisionShape2D();
  CircleShape2D circleShape = new CircleShape2D();

  public override void _Ready()
  {
    circleShape.Radius = Diametro;
    zona_collion.Shape = circleShape;
    AddChild(zona_collion);

    MouseExited += () => _on_zona_estado(Name);

    zona_collion.Owner = GetTree().EditedSceneRoot;
    Owner = GetTree().EditedSceneRoot;
  }

  void _on_zona_estado(string nombre)
  {
    Globals.ZonaSalida = nombre;
    GD.Print("Zone Exited: ", nombre);
    if (Globals.ZonaSalida == "Zone-1")
    {
      Point.linea.Visible = true;
    }
    else if (Globals.ZonaSalida == "Zone-2" && false)
    {
      GetParent().GetParent().EmitSignal("PointExec");
    }
  }
}
