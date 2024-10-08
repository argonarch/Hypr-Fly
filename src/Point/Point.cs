using Godot;
public partial class Point : Node2D
{
  [Signal]
  public delegate void PointExecEventHandler();

  public string IconCenter;
  public string Tabla = "inicio";
  Customer[] customers;
  GroupPizza sectores;
  public static GroupGlove items;
  GroupZone zonas;

  public override void _Ready()
  {
    GD.Print("Get Database dates of table: " + Tabla);
    
    customers = LiteData.GetItems(Tabla);
    IconCenter = LiteData.GetIconCenter(Tabla);
    int cantidad_items = customers.Length;
    int bordeado = 24 - cantidad_items;
    Vector2 distancia = new Vector2(110, 200);

    items = new GroupGlove(cantidad_items, (int)distancia[0], 0.5f, customers, IconCenter);
    sectores = new GroupPizza(cantidad_items, new Vector2(50, distancia[1]));
    zonas = new GroupZone(new Vector2(distancia[0], distancia[1] + 60));

    AddChild(zonas);
    AddChild(sectores);
    AddChild(items);

    PointExec += _exec_command;
  }

  public override void _Process(double delta)
  {
    if (Globals.ZonaSalida == "Zone-0")
    {
      items.GetNode<Glove>("Glove-" + Globals.IdPizza).Position = items.GetLocalMousePosition();
    }
  }

  public void _exec_command()
  {
    GD.Print("Glove Executed: " + Globals.IdPizza);
    int type = Globals.CustomerSelected.Type;
    if (type == 2)
    {
      string newTable = Globals.CustomerSelected.Command;
      string newIconCenter = LiteData.GetIconCenter(newTable); ;
      Visible = false;
      GD.Print("Table is: " + newTable);

      Globals.IdPizza = "ninguno";
      GetParent().EmitSignal("CreaterPoints", newTable, newIconCenter);
    }
    else
    {
      GD.Print("Command is: " + Globals.CustomerSelected.Command);
      if (Globals.Environment == "Flatpak")
      {
        OS.CreateProcess("flatpak-spawn", new string[] { "--host", Globals.CustomerSelected.Command});
      } else {
        OS.CreateProcess(Globals.CustomerSelected.Command, new string[] { });
      }
      GetTree().Quit();
    }
  }
}
