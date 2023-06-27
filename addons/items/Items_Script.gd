@tool
extends Node2D

@export_range(0,400) var distancia: int = 200 : set = distianciador
@export_range(0,20) var division : int = 4 : set = divisionador
@export_range(0,1) var escala: float = 0.5 : set = escalador
var vector_escala = Vector2(0.5,0.5)
# Called when the node enters the scene tree for the first time.
var image_path = preload("res://Assets/coin.png")

func _ready():
	self.rotation_degrees = 90
	creador_item(division)
	pass # Replace with function body.

func distianciador(distancia0):
	distancia = distancia0
	var rad_mid = PI/division
	var mid_item : float =division/2.0
	var n = 0
	if Engine.is_editor_hint() and is_inside_tree():
		for node in get_tree().get_nodes_in_group("contenedor_items"):
			var radian_start = PI*(n)/mid_item
			node.get_node("item").position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			node.get_node("item-Shape").position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			n = n + 1
		n = 0
	pass

func escalador(escala0):
	escala = escala0
	vector_escala = Vector2(escala0,escala0)
	if Engine.is_editor_hint() and is_inside_tree():
		for node in get_tree().get_nodes_in_group("contenedor_items"):
			node.get_node("item").scale = vector_escala
			node.get_node("item-Shape").scale = vector_escala
	
func divisionador(division0):
	division = division0
	if Engine.is_editor_hint() and is_inside_tree():
		for n in self.get_children():
			self.remove_child(n)
		creador_item(division0)
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func creador_item(division0):
	var rad_mid = PI/division0
	var mid_item : float =division0/2.0
	for n in division0:
			var radian_start = PI*(n)/mid_item
			var contenedor = Area2D.new()
			contenedor.set_name("contenedor-" + str(n+1))
			self.add_child(contenedor)
			var itemShape = CollisionShape2D.new()
			var circleShape = CircleShape2D.new()
			circleShape.set_radius(150.00)
			itemShape.set_shape(circleShape)
			#itemShape.rotation = n * (2 * PI) / division
			itemShape.visible = false
			itemShape.position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			itemShape.set_name("item-Shape")
			itemShape.add_to_group("items_shape")
			contenedor.add_child(itemShape)
			var item = Sprite2D.new()
			item.set_texture(image_path)
			item.position = Vector2(distancia, 0).rotated(radian_start+rad_mid)
			item.scale = vector_escala
			#item.rotation = n * (2 * PI) / division
			item.set_name("item")
			item.add_to_group("items")
			contenedor.add_child(item)
			self.add_child(contenedor)
			contenedor.add_to_group("contenedor_items")
			contenedor.set_owner(get_tree().edited_scene_root)
			item.set_owner(get_tree().edited_scene_root)
			itemShape.set_owner(get_tree().edited_scene_root)
