using Godot;
public partial class Glove : Area2D
{
  public Vector2 VectorScale { get; set; }
  public string ImagePath { get; set; }
  public Customer Customer;

  CollisionShape2D itemShape;
  CircleShape2D circleShape;
  Sprite2D item;
  Vector2 initPosition;

  public Glove(string name, Customer customer, float scalePizza, string imagePath)
  {
    Name = name;
    VectorScale = new Vector2(scalePizza, scalePizza);
    ImagePath = imagePath;
    Customer = customer;
  }
  public Glove(float scalePizza, string imagePath)
  {
    Name = "Center";
    VectorScale = new Vector2(scalePizza, scalePizza);
    ImagePath = imagePath;
  }

  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      circleShape = new CircleShape2D { Radius = 100.00f };
      itemShape = new CollisionShape2D { Shape = circleShape, Visible = false, Name = "item-Shape", Scale = VectorScale };
      var image = new Image();
      image.Load(Globals.HyprFlyDir + "/Icons/" + ImagePath);
      item = new Sprite2D
      {
        Name = "item",
        Scale = VectorScale,
        RotationDegrees = -90,
        Texture = ImageTexture.CreateFromImage(image),
      };
      MouseEntered += () => _on_sector_mouse_entered();
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

  public void _on_sector_mouse_entered()
  {
    Globals.CustomerSelected = Customer;
  }
}

