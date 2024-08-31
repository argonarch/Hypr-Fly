using Godot;

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
    Rotation = Globals.Pi_2;
    float rad_mid = Mathf.Pi / Division;
    float mid_item = Division / 2.0f;
    for (int i = 0; i < Division; i++)
    {
      float radianStart = Mathf.Pi * (i) / mid_item;
      Glove gloveExtern = new Glove(i.ToString(), Customers[i], Escala);
      gloveExtern.Position = new Vector2(Distancia, 0).Rotated(radianStart + rad_mid);
      AddChild(gloveExtern);
      gloveExtern.AddToGroup("contenedor_items");
    }
    Glove globeCenter = new Glove(Escala, Icono);
    AddChild(globeCenter);
    globeCenter.AddToGroup("contenedor_items");
  }
}
