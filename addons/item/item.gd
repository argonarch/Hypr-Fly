@tool
extends Area2D
class_name Item2D
signal estado(nombre, estado)
@export_range(0,1) var escala: float = 0.5 : set = escalador

var vector_escala = Vector2(0.5,0.5)
var image_path : String
var itemShape = CollisionShape2D.new()
var item = Sprite2D.new()
var init_position

func _ready():
	if get_child_count() == 0:
		var circleShape = CircleShape2D.new()
		circleShape.set_radius(100.00)
		itemShape.set_shape(circleShape)
		#itemShape.rotation = n * (2 * PI) / division
		itemShape.visible = false
		itemShape.set_name("item-Shape")
		itemShape.scale = vector_escala
		
		item.set_texture(load("res://Assets/icons/" + image_path))
		item.scale = vector_escala
		item.rotation_degrees = -90
		#item.rotation = n * (2 * PI) / division
		item.set_name("item")
		self.add_child(itemShape)
		self.add_child(item)
		init_position = position
		self.mouse_exited.connect(_on_item_mouse_exited)
		self.mouse_entered.connect(_on_item_mouse_entered)
		item.set_owner(get_tree().edited_scene_root)
		itemShape.set_owner(get_tree().edited_scene_root)


func escalador(escala0):
	escala = escala0
	vector_escala = Vector2(escala0,escala0)
	if Engine.is_editor_hint() and is_inside_tree():
		itemShape.scale = vector_escala
		item.scale = vector_escala

func _on_item_mouse_exited():
	emit_signal("estado",self.name)

func _on_item_mouse_entered():
	emit_signal("estado",self.name)

func auto_posicion():
	position = init_position
