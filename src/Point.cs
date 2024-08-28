using Godot;
public partial class Point : Node2D
{

  [Signal]
  public delegate void PointExecEventHandler();

  public string icono_center = "emblem-added.png";
  public string tabla = "items";

  GroupPizza sectores;
  public static GroupGlove items;
  GroupZone zonas;
  public static Line2D linea;

  public override void _Ready()
  {
    GD.Print("Get Database dates of table: " + tabla);

    string[] imagesPaths = DataBase.GetDates("select icon from " + tabla + ";", "icon");
    int cantidad_items = DataBase.GetDate("select count(*) from " + tabla + ";").ToInt();

    GD.Print("Cantidad: " + cantidad_items);
    int bordeado = 24 - cantidad_items;
    Vector2 distancia = new Vector2(110, 200);
    Vector2[] points = { Vector2.Zero, Vector2.Zero };

    linea = new Line2D { Name = "Linea", Points = points, Visible = false };
    items = new GroupGlove(cantidad_items, (int)distancia[0], 0.5f, imagesPaths, icono_center);
    sectores = new GroupPizza(cantidad_items, new Vector2(distancia[1], 50), bordeado);
    zonas = new GroupZone(distancia);

    AddChild(zonas);
    AddChild(sectores);
    AddChild(linea);
    AddChild(items);

    PointExec += _exec_command;
  }

  public override void _Process(double delta)
  {
    if (Globals.ZonaSalida == "Zone-1")
    {
      linea.SetPointPosition(1, linea.GetLocalMousePosition());
      items.GetNode<Glove>(Globals.NamePizza).Position = items.GetLocalMousePosition();
    }
  }

  public void _exec_command()
  {
    linea.Visible = false;
    GD.Print("Pizza Executed: " + Globals.NamePizza);
    string command = DataBase.GetDate("select command from " + tabla + " where item = '" + Globals.NamePizza + "' ;");
    if (command == "comandado")
    {
      string tabla_nombre = DataBase.GetDate("select tabla from " + tabla + " where item = '" + Globals.NamePizza + "' ;");
      string icono_nombre = DataBase.GetDate("select icon from " + tabla + " where item = '" + Globals.NamePizza + "' ;");
      this.Visible = false;
      GD.Print("Table is: " + tabla_nombre);

      Globals.NamePizza = "ninguno";
      GetParent().EmitSignal("Tablada", tabla_nombre, icono_nombre);
    }
    else
    {
      GD.Print("Command is: " + command);
      OS.CreateProcess(command, new string[0]);
      GetTree().Quit();
    }
  }
}
