extends Node2D

var sceneLauncher = preload("res://Scene/launcher.tscn")
var sceneParedes = preload("res://Scene/paredes.tscn")

func _ready():
	var output = []
	var exit_code = OS.execute("hyprctl", ["cursorpos"], output)
	var stringer = output[0].rsplit(",", true, 0)
	print(stringer)
	var instance = sceneLauncher.instantiate()
	var instance_2 = sceneParedes.instantiate()
	instance.position = Vector2(float(stringer[0]),float(stringer[1]))
	instance.tabla = "items"
	add_child(instance)
	add_child(instance_2)
	instance.tablada.connect(_on_instance)

func _on_instance(tabla,icono):
	var instance = sceneLauncher.instantiate()
	instance.tabla = tabla
	instance.position = get_global_mouse_position()
	instance.icono_center = icono
	add_child(instance)
