using Godot;
using System;

public partial class GroupGlove : Node2D
{
  public int distancia { get; set; } = 100;
  public int division { get; set; } = 4;
  public float escala { get; set; } = 0.5f;
  public string[] imagesPath { get; set; }
  public string icono { get; set; }
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	if (GetChildCount() == 0)
	{
	  this.RotationDegrees = 90;
	  createGloves(division);
	}
  }
  void createGloves(float divisionNow)
  {
	double rad_mid = Math.PI / divisionNow;
	float mid_item = divisionNow / 2.0f;
	for (int i = 0; i < divisionNow; i++)
	{

	  double radianStart = Math.PI * (i) / mid_item;
	  Glove gloveExtern = new Glove();
	  gloveExtern.imagePath = imagesPath[i];
	  gloveExtern.Name = "contenedor-" + (i + 1).ToString();
	  gloveExtern.Position = new Vector2(distancia, 0).Rotated((float)(radianStart + rad_mid));
	  gloveExtern.scale = escala;
	  this.AddChild(gloveExtern);
	  gloveExtern.AddToGroup("contenedor_items");
	  gloveExtern.Owner = GetTree().EditedSceneRoot;
	}
	Glove globeCenter = new Glove();
	globeCenter.imagePath = icono;
	globeCenter.Name = "contenedor-centro";
	globeCenter.scale = escala;
	this.AddChild(globeCenter);
	globeCenter.AddToGroup("contenedor_items");
	globeCenter.Owner = GetTree().EditedSceneRoot;
  }

}
