@tool
extends Node2D
class_name PizzaEntered2D
@export_range(0,400) var radio : int = 200 : set = radiador
@export_range(0,400) var radio_interno : int = 20 : set = radiador_inter
@export_range(0,20) var division : int = 4 : set = divisionador
@export_range(2,20) var borde : int = 2 : set = bordeador

func _ready():
	if get_child_count() == 0:
		creador_item(division)
		self.rotation_degrees = 90

func divisionador(division0):
	division = division0
	if Engine.is_editor_hint() and is_inside_tree():
		for n in self.get_children():
			self.remove_child(n)
		creador_item(division0)

func bordeador(borde0):
	borde = borde0
	if Engine.is_editor_hint() and is_inside_tree():
		get_tree().call_group("sectors", "divisioner", borde0)
	
func radiador(radio0):
	radio = radio0
	if Engine.is_editor_hint() and is_inside_tree():
		get_tree().call_group("sectors", "radiuser", radio0)
		
func radiador_inter(radio_interno0):
	radio_interno = radio_interno0
	if Engine.is_editor_hint() and is_inside_tree():
		get_tree().call_group("sectors", "radiuser_inter", radio_interno0)

func creador_item(division0):
	for n in division0:
		var contenedor = PizzaPart2D.new()
		contenedor.set_name("contenedor-" + str(n+1))
		contenedor.rotation = n * (2 * PI) / division
		contenedor.angle = (360) / float(division)
		contenedor.divitions = borde
		contenedor.radius = radio
		contenedor.radius_interno = radio_interno
		contenedor.add_to_group("sectors")
		self.add_child(contenedor)
		contenedor.set_owner(get_tree().edited_scene_root)
