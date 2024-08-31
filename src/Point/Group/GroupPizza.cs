using Godot;
using System;
using System.Collections.Generic;

public partial class GroupPizza : Node2D
{
  public Vector2 Radio { get; set; }
  public int Division { get; set; }
  public int Smooth { get; set; } = 60;

  public GroupPizza(int division, Vector2 radio)
  {
    Name = "Sectors";
    Division = division;
    Radio = radio;
  }

  public override void _Ready()
  {
    Rotation = Globals.Pi_2;
    List<Vector2> RadioExtern = new List<Vector2>();
    List<Vector2> RadioIntern = new List<Vector2>();
    int partSize = Smooth / Division;
    float fractions = Mathf.Tau / Smooth;
    for (int i = 0; i <= Smooth; i++)
    {
      RadioExtern.Add(new Vector2(Radio[1], 0).Rotated(fractions * i));
      RadioIntern.Add(new Vector2(Radio[0], 0).Rotated(fractions * i));
    }
    for (int i = 0; i < Division; i++)
    {
      int inicio = i * partSize;
      int fin = Math.Min(inicio + partSize + 1, RadioExtern.Count);
      List<Vector2> Extern = RadioExtern.GetRange(inicio, fin - inicio);
      List<Vector2> Intern = RadioIntern.GetRange(inicio, fin - inicio);
      Intern.Reverse();
      Extern.AddRange(Intern);
      Pizza contenedor = new Pizza(i.ToString(), Extern.ToArray());
      contenedor.AddToGroup("sectors");
      AddChild(contenedor);
    }
  }
}
