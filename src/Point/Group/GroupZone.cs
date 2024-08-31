using Godot;

public partial class GroupZone : Node2D
{
  public Vector2 Zones { get; set; }

  public GroupZone(Vector2 zones)
  {
    Name = "Zones";
    Zones = new Vector2(zones[0], zones[1]);
  }

  public override void _Ready()
  {
    for (int i = 0; i < 2; i++)
    {
      Zone zone = new Zone { Diametro = Zones[i], Name = "Zone-" + i.ToString() };
      AddChild(zone);
    }
  }

}
