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
  }

  void _on_zona_estado(string nombre)
  {
    Globals.ZonaSalida = nombre;
    if (Globals.ZonaSalida == "Zone-1")
    {
      GetParent().GetParent().EmitSignal("PointExec");
    }
    // GD.Print("Zone Exited: ", nombre);
  }
}
