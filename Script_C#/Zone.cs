using Godot;
using System;

public partial class Zone : Area2D
{
  public float diametro { get; set; }

  CollisionShape2D zona_collion = new CollisionShape2D();
  CircleShape2D circleShape = new CircleShape2D();

  private CustomSignals _customSignals;
  public override void _Ready()
  {
    _customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    circleShape.Radius = diametro;
    zona_collion.Shape = circleShape;
    this.AddChild(zona_collion);
    this.MouseExited += () => _on_zona_mouse_exited();
    zona_collion.Owner = GetTree().EditedSceneRoot;
  }
  void _on_zona_mouse_exited()
  {
    Console.Write("saliste de zona: " + this.Name);
    _customSignals.EmitSignal("Estado", this.Name);
  }
}
