using Godot;

public partial class GroupZone : Node2D
{
  public int[] zones { get; set; }
  public override void _Ready()
  {
    for (int i = 0; i < zones.Length; i++)
    {

      Zone zone = new Zone();
      zone.diametro = (float)(zones[i]);
      zone.Name = "zona-" + (i + 1).ToString();
      this.AddChild(zone);
      zone.Owner = GetTree().EditedSceneRoot;
    }
  }
}
