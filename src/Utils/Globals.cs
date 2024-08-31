using Godot;

public partial class Globals : Node
{
  // Called when the node enters the scene tree for the first time.
  public static string IdPizza { get; set; } = "ninguno";
  public static Customer CustomerSelected { get; set; }
  public static string ZonaSalida { get; set; }
  public static string HyprFlyDir = OS.GetConfigDir() + "/HyprFly";
  public static string Environment { get; set; }
  public const float Pi_2 = Mathf.Pi / 2;
}
