extends Area2D
class_name  Zona2D
signal estado(nombre, estado)
@export var diametro : float 

var zona_collion = CollisionShape2D.new()
var circleShape = CircleShape2D.new()

func _ready():
	circleShape.set_radius(diametro)
	zona_collion.set_shape(circleShape)
	self.add_child(zona_collion)
	self.mouse_exited.connect(_on_zona_mouse_exited)
	zona_collion.set_owner(get_tree().edited_scene_root)


func _on_zona_mouse_exited():
	#print("saliste de zona: " + self.name)
	emit_signal("estado", self.name)
