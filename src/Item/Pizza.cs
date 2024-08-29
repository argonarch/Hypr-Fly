using Godot;
using System.Collections.Generic;
using System;
public partial class Pizza : Area2D
{
  [Signal]
  public delegate void PizzaActualEventHandler(string nombre);

  public Vector2 Radius { get; set; }
  public float Angle { get; set; }
  public int Divitions { get; set; }
  public string Id { get; set; }
  CollisionPolygon2D pizzaShape;
  Polygon2D pizzaDraw;

  Vector2[] vectors = {
        new Vector2(0,0),
        new Vector2(100,0),
        new Vector2(0,100)
    };
  public Pizza(string name, string id, Vector2 radius, float angle, int divitions)
  {
    Name = name;
    Id = id;
    Radius = radius;
    Angle = angle;
    Divitions = divitions;
  }

  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      List<Vector2> vectorsList = new List<Vector2>();
      vectorsList.Add(new Vector2(Radius[1], 0));
      for (int i = 0; i < Divitions; i++)
      {
        float radianRotated = (float)(((Angle * i) / (Divitions - 1)) * (Math.PI / 180));
        vectorsList.Add(new Vector2(Radius[0], 0).Rotated(radianRotated));
      }
      for (int i = Divitions - 1; i > 0; i--)
      {
        float radianRotated = (float)(((Angle * i) / (Divitions - 1)) * (Math.PI / 180));
        vectorsList.Add(new Vector2(Radius[1], 0).Rotated(radianRotated));
      }
      vectors = vectorsList.ToArray();
      pizzaShape = new CollisionPolygon2D { Name = "Pizza_Shape", Polygon = vectors, Visible = false };
      pizzaDraw = new Polygon2D { Name = "Pizza_Draw", Polygon = vectors, Color = new Color(0.0f, 0.0f, 0.0f, 0.26f) };

      AddChild(pizzaShape);
      AddChild(pizzaDraw);

      MouseExited += () => _on_sector_mouse_exited();
      MouseEntered += () => _on_sector_mouse_entered();
      PizzaActual += _on_sector_estado;


      pizzaDraw.Owner = GetTree().EditedSceneRoot;
      pizzaShape.Owner = GetTree().EditedSceneRoot;
      Owner = GetTree().EditedSceneRoot;
    }
  }
  void _on_sector_mouse_exited()
  {
    pizzaDraw.Color = new Color(0.0f, 0.0f, 0.0f, 0.26f);
  }
  void _on_sector_mouse_entered()
  {
    pizzaDraw.Color = new Color(0.36f, 0.42f, 0.75f, 0.30f);
    EmitSignal("PizzaActual", Id);
  }

  void _on_sector_estado(string nombre)
  {
    if (Globals.IdPizza != "ninguno")
    {
      Point.items.GetNode<Glove>("Glove-" + Globals.IdPizza).auto_posicion();
    }
    Globals.IdPizza = nombre;
    // GD.Print("Pizza Actual: ", Globals.IdPizza);
  }
}
