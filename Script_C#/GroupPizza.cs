using Godot;
using System;

public partial class GroupPizza : Node2D
{
  public int radio { get; set; } = 200;
  public int radio_interno { get; set; } = 20;
  public int division { get; set; } = 4;
  public int borde { get; set; } = 2;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      createItems(division);
      this.RotationDegrees = 90;
    }
  }
  void createItems(int divisionNow)
  {
    for (int i = 0; i < divisionNow; i++)
    {
      Pizza contenedor = new Pizza();
      contenedor.Name = "contenedor-" + (i + 1).ToString();
      contenedor.Rotation = (float)(i * (2 * Math.PI) / division);
      contenedor.angle = (360) / (float)(division);
      contenedor.divitions = borde;
      contenedor.radius = radio;
      contenedor.radius_interno = radio_interno;
      contenedor.AddToGroup("sectors");
      this.AddChild(contenedor);
      contenedor.Owner = GetTree().EditedSceneRoot;
    }
  }
}
