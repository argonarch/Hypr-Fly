using Godot;

public partial class CustomSignals : Node
{
  [Signal]
  public delegate void TabladaEventHandler(string tabla, string icono);
  [Signal]
  public delegate void EstadoEventHandler(string nombre);
  [Signal]
  public delegate void ZoneActualEventHandler(string nombre);
  [Signal]
  public delegate void SectorActualEventHandler(string nombre);
  [Signal]
  public delegate void ItemActualEventHandler(string nombre);
}


