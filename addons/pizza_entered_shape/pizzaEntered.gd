@tool
extends Node2D

@export_range(0,400) var radio : int = 100 : set = radiador
@export_range(0,400) var radio_interno : int = 20 : set = radiador_inter
@export_range(0,20) var division : int = 4 : set = divisionador
@export_range(2,20) var borde : int = 2 : set = bordeador
# Called when the node enters the scene tree for the first time.
func _ready():
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
		get_tree().call_group("sectors_shape", "divisioner", borde0)
	
func radiador(radio0):
	radio = radio0
	if Engine.is_editor_hint() and is_inside_tree():
		get_tree().call_group("sectors", "radiuser", radio0)
		get_tree().call_group("sectors_shape", "radiuser", radio0)
		
func radiador_inter(radio_interno0):
	radio_interno = radio_interno0
	if Engine.is_editor_hint() and is_inside_tree():
		get_tree().call_group("sectors", "radiuser_inter", radio_interno0)
		get_tree().call_group("sectors_shape", "radiuser_inter", radio_interno0)

func creador_item(division0):
	for n in division0:
			var contenedor = Area2D.new()
			contenedor.set_name("contenedor-" + str(n+1))
			self.add_child(contenedor)
			var itemShape = PizzaShape2D.new()
			itemShape.rotation = n * (2 * PI) / division
			itemShape.angle = (360) / float(division)
			itemShape.divitions = borde
			itemShape.radius = radio
			itemShape.radius_interno = radio_interno
			itemShape.visible = false
			itemShape.set_name("sector-Shape-" + str(n+1))
			itemShape.add_to_group("sectors_shape")
			contenedor.add_child(itemShape)
			var item = PizzaDraw2D.new()
			item.rotation = n * (2 * PI) / division
			item.angle = (360) / float(division0)
			item.divitions = borde
			item.radius = radio
			item.radius_interno = radio_interno
			item.set_name("sector-" + str(n+1))
			item.add_to_group("sectors")
			contenedor.add_child(item)
			self.add_child(contenedor)
			contenedor.set_owner(get_tree().edited_scene_root)
			item.set_owner(get_tree().edited_scene_root)
			itemShape.set_owner(get_tree().edited_scene_root)
