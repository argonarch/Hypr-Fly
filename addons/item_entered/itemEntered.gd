@tool
extends Node2D
class_name ItemEntered2D

@export_range(0,400) var distancia: int = 100 : set = distianciador
@export_range(0,20) var division : int = 4 : set = divisionador
@export_range(0,1) var escala: float = 0.5 : set = escalador

var images_path
var icono : String

func _ready():
	if get_child_count() == 0:
		self.rotation_degrees = 90
		creador_item(division)
		

func distianciador(distancia0):
	distancia = distancia0
	var rad_mid = PI/division
	var mid_item : float =division/2.0
	var n = 0
	if Engine.is_editor_hint() and is_inside_tree():
		for node in get_tree().get_nodes_in_group("contenedor_items"):
			var radian_start = PI*(n)/mid_item
			node.position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			n = n + 1
		n = 0

func escalador(escala0):
	escala = escala0
	if Engine.is_editor_hint() and is_inside_tree():
		for node in get_tree().get_nodes_in_group("contenedor_items"):
			node.escala = escala0
	
func divisionador(division0):
	division = division0
	if Engine.is_editor_hint() and is_inside_tree():
		for n in self.get_children():
			self.remove_child(n)
		creador_item(division0)


func creador_item(division0):
	var rad_mid = PI/division0
	var mid_item : float = division0 / 2.0
	for n in division0:
			var radian_start = PI*(n)/mid_item
			var contenedor = Item2D.new()
			contenedor.image_path = images_path[n]["icon"]
			contenedor.set_name("contenedor-" + str(n+1))
			contenedor.position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			contenedor.escala = escala
			#item.rotation = n * (2 * PI) / division
			self.add_child(contenedor)
			contenedor.add_to_group("contenedor_items")
			contenedor.set_owner(get_tree().edited_scene_root)
	var contenedor = Item2D.new()
	contenedor.image_path = icono
	contenedor.set_name("contenedor-centro")
	contenedor.escala = escala
	self.add_child(contenedor)
	contenedor.add_to_group("contenedor_items")
	contenedor.set_owner(get_tree().edited_scene_root)
