using Godot;
using System;

public partial class GroupGlove : Node2D
{
  public int Distancia { get; set; }
  public int Division { get; set; }
  public float Escala { get; set; }
  public Customer[] Customers { get; set; }
  public string Icono { get; set; }

  public GroupGlove(int division, int distancia, float escala, Customer[] customers, string icono)
  {
    Name = "Items";
    Division = division;
    Distancia = distancia;
    Escala = escala;
    Icono = icono;
    Customers = customers;
  }

  public override void _Ready()
  {
    if (GetChildCount() == 0)
    {
      RotationDegrees = 90;
      createGloves(Division);
    }
  }
  void createGloves(float divisionNow)
  {
    double rad_mid = Math.PI / divisionNow;
    float mid_item = divisionNow / 2.0f;
    for (int i = 0; i < divisionNow; i++)
    {
      double radianStart = Math.PI * (i) / mid_item;
      Glove gloveExtern = new Glove("Glove-" + (i + 1).ToString(), Customers[i], Escala, Customers[i].Icon);
      gloveExtern.Position = new Vector2(Distancia, 0).Rotated((float)(radianStart + rad_mid));
      AddChild(gloveExtern);
      gloveExtern.AddToGroup("contenedor_items");
    }
    Glove globeCenter = new Glove(Escala, Icono);
    AddChild(globeCenter);
    globeCenter.AddToGroup("contenedor_items");
  }

}
