using Godot;

public partial class Pizza : Area2D
{
  [Signal]
  public delegate void PizzaActualEventHandler(string nombre);

  public Vector2[] Points { get; set; }
  public string Id { get; set; }
  CollisionPolygon2D pizzaShape;
  Polygon2D pizzaDraw;

  public Pizza(string id, Vector2[] points)
  {
    Name = "Pizza-" + id;
    Id = id;
    Points = points;
  }

  public override void _Ready()
  {
    pizzaShape = new CollisionPolygon2D { Name = "Pizza_Shape", Polygon = Points, Visible = false };
    pizzaDraw = new Polygon2D { Name = "Pizza_Draw", Polygon = Points, Color = new Color(0.0f, 0.0f, 0.0f, 0.26f) };

    AddChild(pizzaShape);
    AddChild(pizzaDraw);

    MouseExited += () => _on_sector_mouse_exited();
    MouseEntered += () => _on_sector_mouse_entered();
    PizzaActual += _on_sector_estado;
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

    GD.Print("Pizza Actual: ", Globals.IdPizza);
  }
}
