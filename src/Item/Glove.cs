using Godot;
public partial class Glove : Area2D
{
  public Vector2 VectorScale { get; set; } = new Vector2(0.5f, 0.5f);
  public string ImagePath { get; set; }
  CollisionShape2D itemShape;
  CircleShape2D circleShape;
  Sprite2D item;
  Vector2 initPosition;

  public Glove(string name, float scalePizza, string imagePath)
  {
    Name = name;
    VectorScale = new Vector2(scalePizza, scalePizza);
    ImagePath = imagePath;
  }

  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      circleShape = new CircleShape2D { Radius = 100.00f };
      itemShape = new CollisionShape2D { Shape = circleShape, Visible = false, Name = "item-Shape", Scale = VectorScale };
      item = new Sprite2D
      {
        Name = "item",
        Scale = VectorScale,
        RotationDegrees = -90,
        Texture = ResourceLoader.Load<Texture2D>("res://Assets/icons/" + ImagePath)
      };

      AddChild(itemShape);
      AddChild(item);
      initPosition = Position;
      item.Owner = GetTree().EditedSceneRoot;
      itemShape.Owner = GetTree().EditedSceneRoot;
      Owner = GetTree().EditedSceneRoot;
    }
  }
  public void auto_posicion()
  {
    Position = initPosition;
  }
}

