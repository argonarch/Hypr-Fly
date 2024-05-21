using Godot;

public partial class Globals : Node
{
  // Called when the node enters the scene tree for the first time.
  public int numItems { get; set; } = 4;
  public int distanceItem { get; set; } = 120;
  public bool itemSelected { get; set; } = false;
  public bool neutralStatus { get; set; } = false;
  public string dbName { get; set; } = "res://DataStore/database.db";

}
