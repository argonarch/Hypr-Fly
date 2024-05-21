using Godot;
public partial class Glove : Area2D
{
  [Signal]
  public delegate void GloveActualEventHandler(string nombre);

  public float scale { get; set; } = 0.5f;
  public Vector2 vectorScale { get; set; } = new Vector2(0.5f, 0.5f);
  public string imagePath { get; set; }
  CollisionShape2D itemShape = new CollisionShape2D();
  CircleShape2D circleShape = new CircleShape2D();
  Sprite2D item = new Sprite2D();
  Vector2 initPosition;
  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      circleShape.Radius = 100.00f;
      itemShape.Shape = circleShape;
      itemShape.Visible = false;
      itemShape.Name = "item-Shape";
      itemShape.Scale = vectorScale;
      item.Texture = ResourceLoader.Load<Texture2D>("res://Assets/icons/" + imagePath);
      item.Scale = vectorScale;
      item.RotationDegrees = -90;
      item.Name = "item";
      AddChild(itemShape);
      AddChild(item);
      initPosition = Position;
      MouseExited += () => _on_item_mouse_exited();
      MouseEntered += () => _on_item_mouse_entered();
      item.Owner = GetTree().EditedSceneRoot;
      itemShape.Owner = GetTree().EditedSceneRoot;
    }
  }
  void _on_item_mouse_exited()
  {
    EmitSignal("GloveActual", Name);
  }
  void _on_item_mouse_entered()
  {
    EmitSignal("GloveActual", Name);
  }
  public void auto_posicion()
  {
    Position = initPosition;
  }
}

