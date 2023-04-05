extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	get_viewport().transparent_bg = true;
	
func _process(delta):
	#$CharacterBody2D/Line2D.set_point_position(1, Vector2(0,0))
	#$CharacterBody2D/Line2D.set_point_position(0, $pricipal.get_position())
	
	pass

