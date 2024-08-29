using Godot;
using System;

public partial class GroupPizza : Node2D
{
  public Vector2 Radio { get; set; }
  public int Division { get; set; }
  public int Borde { get; set; }

  public GroupPizza(int division, Vector2 radio, int borde)
  {
    Name = "Sectors";
    Division = division;
    Radio = radio;
    Borde = borde;
  }

  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      createItems(Division);
      RotationDegrees = 90;
    }
  }
  void createItems(int divisionNow)
  {
    for (int i = 0; i < divisionNow; i++)
    {
      Pizza contenedor = new Pizza("Pizza-" + (i + 1).ToString(), (i + 1).ToString(), Radio, 360 / (float)(divisionNow), Borde);
      contenedor.Rotation = (float)(i * (2 * Math.PI) / Division);
      contenedor.AddToGroup("sectors");
      AddChild(contenedor);
    }
  }
}
