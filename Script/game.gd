extends Node2D

var scene = preload("res://Scene/point.tscn")


# Called when the node enters the scene tree for the first time.
func _ready():
	var output = []
	var exit_code = OS.execute("hyprctl", ["cursorpos"], output)
	var stringer = output[0].rsplit(",", true, 0)
	print(stringer)
	var stringer_emply: PackedStringArray = [""]
	if stringer == stringer_emply:
		stringer = ["800", "500"]
	else:
		stringer = exit_code
	var instance = scene.instantiate()
	instance.position = Vector2(float(stringer[0]), float(stringer[1]))
	instance.tabla = "items"
	add_child(instance)
	instance.tablada.connect(_on_instance)


func _on_instance(tabla, icono):
	var instance = scene.instantiate()
	instance.tabla = tabla
	instance.position = get_global_mouse_position()
	instance.icono_center = icono
	add_child(instance)
