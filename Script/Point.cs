using Godot;
public partial class Point : Node2D
{
  public string namePizza = "ninguno";
  public string zonaSalida;
  public string icono_center = "emblem-added.png";
  public string tabla = "items";
  private CustomSignals _customSignals;
  private DataBase _dataBase;
  private Pizza _pizza;
  GroupPizza sectores = new GroupPizza();
  GroupGlove items = new GroupGlove();
  GroupZone zonas = new GroupZone();
  Line2D linea = new Line2D();

  public override void _Ready()
  {
    _customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    _dataBase = GetNode<DataBase>("/root/DataBase");
    GD.Print("Connected to CustomSignals");

    GD.Print("Get Database dates of table: " + tabla);
    string[] imagesPaths = _dataBase.GetDates("select icon from " + tabla + ";", "icon");
    int cantidad_items = _dataBase.GetDate("select count(*) from " + tabla + ";").ToInt();
    GD.Print("Cantidad: " + cantidad_items);
    int bordeado = 12 - cantidad_items;
    int distancia_item = 110;
    int distancia_final = 200;
    Vector2[] points = { new Vector2(0, 0),
                         new Vector2(0,0)};
    linea.Points = points;
    linea.Visible = false;

    items.Name = "Items";
    items.division = cantidad_items;
    items.distancia = distancia_item;
    items.imagesPath = imagesPaths;
    items.icono = icono_center;

    sectores.Name = "Sectors";
    sectores.radio = distancia_final;
    sectores.radio_interno = 50;
    sectores.division = cantidad_items;
    sectores.borde = bordeado;

    zonas.Name = "zonas";
    int[] distZones = { distancia_item, distancia_final + 60 };
    zonas.zones = distZones;

    AddChild(zonas);
    AddChild(sectores);
    AddChild(linea);
    AddChild(items);
    signal_consegidor(sectores, zonas);
  }

  public override void _Process(double delta)
  {
    if (zonaSalida == "Zone-1")
    {
      linea.SetPointPosition(1, linea.GetLocalMousePosition());

      items.GetNode<Glove>(namePizza).Position = items.GetLocalMousePosition();
    }
  }

  void signal_consegidor(GroupPizza sectores, GroupZone zones)
  {
    Godot.Collections.Array<Node> childSectors = sectores.GetChildren();
    Godot.Collections.Array<Node> childZones = zones.GetChildren();
    foreach (Pizza child in childSectors)
    {
      child.PizzaActual += _on_sector_estado;
    }
    foreach (Zone child in childZones)
    {
      child.ZoneActual += _on_zona_estado;
    }
  }

  void _on_sector_estado(string nombre)
  {
    if (namePizza != "ninguno")
    {
      items.GetNode<Glove>(namePizza).auto_posicion();
    }
    namePizza = nombre;
    GD.Print("Pizza Actual: ", namePizza);
  }

  void _on_zona_estado(string nombre)
  {
    zonaSalida = nombre;
    GD.Print("Zone Exited: ", nombre);
    if (zonaSalida == "Zone-1")
    {
      linea.Visible = true;
    }
    else if (zonaSalida == "Zone-2")
    {
      linea.Visible = false;
      GD.Print("Pizza Executed: " + namePizza);
      string command = _dataBase.GetDate(
          "select command from " + tabla + " where item = '" + namePizza + "' ;");
      if (command == "comandado")
      {
        string tabla_nombre = _dataBase.GetDate(
                  "select tabla from " + tabla + " where item = '" + namePizza + "' ;");

        string icono_nombre = _dataBase.GetDate(
            "select icon from " + tabla + " where item = '" + namePizza + "' ;");
        this.Visible = false;
        GD.Print("Table is: " + tabla_nombre);
        _customSignals.EmitSignal("Tablada", tabla_nombre, icono_nombre);
      }
      else
      {
        GD.Print("Command is: " + command);
        OS.CreateProcess(command, new string[0]);
        GetTree().Quit();
      }
    }
  }
}
