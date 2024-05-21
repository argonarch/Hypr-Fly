using Godot;

public partial class Zone : Area2D
{
  [Signal]
  public delegate void ZoneActualEventHandler(string nombre);

  public float diametro { get; set; }

  CollisionShape2D zona_collion = new CollisionShape2D();
  CircleShape2D circleShape = new CircleShape2D();

  public override void _Ready()
  {
    circleShape.Radius = diametro;
    zona_collion.Shape = circleShape;
    AddChild(zona_collion);
    MouseExited += () => _on_zona_mouse_exited();
    if (Name == "Zone-1")
    {
      MouseEntered += () => _on_zona_mouse_entered();
    }
    zona_collion.Owner = GetTree().EditedSceneRoot;
  }
  void _on_zona_mouse_exited()
  {
    EmitSignal("ZoneActual", Name);
  }
  void _on_zona_mouse_entered()
  {
    EmitSignal("ZoneActual", Name);
  }
}
