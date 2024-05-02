using Godot;
using System.Collections.Generic;
using System;
public partial class Pizza : Area2D
{
  public int radius { get; set; } = 100;
  public int radius_interno { get; set; }
  public float angle { get; set; } = 45.00f;
  public int divitions { get; set; } = 2;
  CollisionPolygon2D pizzaShape = new CollisionPolygon2D();
  Polygon2D pizzaDraw = new Polygon2D();

  private CustomSignals _customSignals;
  Vector2[] vectors = {
        new Vector2(0,0),
        new Vector2(100,0),
        new Vector2(0,100)
    };
  public override void _Ready()
  {
    _customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    if (GetChildCount() == 0)
    {
      List<Vector2> vectorsList = new List<Vector2>();
      vectorsList.Add(new Vector2(radius_interno, 0));
      for (int i = 0; i < divitions; i++)
      {
        float radianRotated = (float)(((angle * i) / (divitions - 1)) * (Math.PI / 180));
        vectorsList.Add(new Vector2(radius, 0).Rotated(radianRotated));
      }
      for (int i = divitions; i < 0; i--)
      {
        float radianRotated = (float)(((angle * i) / (divitions - 1)) * (Math.PI / 180));
        vectorsList.Add(new Vector2(radius_interno, 0).Rotated(radianRotated));
      }
      vectors = vectorsList.ToArray();
      pizzaShape.Polygon = vectors;
      pizzaDraw.Polygon = vectors;
      pizzaDraw.Color = new Color(0.0f, 0.0f, 0.0f, 0.26f);

      pizzaShape.Visible = false;

      pizzaShape.Name = "Pizza_Shape";

      pizzaDraw.Name = "Pizza_Draw";

      this.AddChild(pizzaShape);

      this.AddChild(pizzaDraw);

      this.MouseExited += () => _on_sector_mouse_exited();

      this.MouseEntered += () => _on_sector_mouse_entered();

      pizzaDraw.Owner = GetTree().EditedSceneRoot;

      pizzaShape.Owner = GetTree().EditedSceneRoot;

    }
  }
  void _on_sector_mouse_exited()
  {
    pizzaDraw.Color = new Color(0.0f, 0.0f, 0.0f, 0.26f);
  }
  void _on_sector_mouse_entered()
  {
    pizzaDraw.Color = new Color(0.36f, 0.42f, 0.75f, 0.30f);

    _customSignals.EmitSignal("Estado", this.Name);
  }
}
