using Godot;

public partial class CustomSignals : Node
{
  [Signal]
  public delegate void TabladaEventHandler(string tabla, string icono);

}


