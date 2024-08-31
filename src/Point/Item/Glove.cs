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

  public Glove(string id, Customer customer, float scalePizza)
  {
    Name = "Glove-" + id;
    VectorScale = new Vector2(scalePizza, scalePizza);
    ImagePath = customer.Icon;
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
    circleShape = new CircleShape2D { Radius = 100.00f };
    itemShape = new CollisionShape2D { Shape = circleShape, Visible = false, Name = "item-Shape", Scale = VectorScale };
    var image = new Image();
    image.Load(Globals.HyprFlyDir + "/Icons/" + ImagePath);
    item = new Sprite2D
    {
      Name = "item",
      Scale = VectorScale,
      Rotation = - Globals.Pi_2,
      Texture = ImageTexture.CreateFromImage(image),
    };
    MouseEntered += () => _on_sector_mouse_entered();
    AddChild(itemShape);
    AddChild(item);
    initPosition = Position;
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

