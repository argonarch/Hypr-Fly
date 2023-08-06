@tool
extends Node2D
class_name  Zonador2D

@export var zona_1 : int = 100
@export var zona_2 : int = 150
#@export var zona_3 : int = 200

func _ready():
	var distancias = [zona_1,zona_2]
	var cantidad = 2
	for n in cantidad:
		var zona = Zona2D.new()
		zona.diametro = float(distancias[n])
		zona.set_name("zona-" + str(n+1))
		self.add_child(zona)
		zona.set_owner(get_tree().edited_scene_root)
