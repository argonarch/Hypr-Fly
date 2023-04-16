extends Area2D

func _ready():
	$neutral.shape.radius = Global.distance_item
	mouse_exited.connect(_on_mouse_exited)
	mouse_entered.connect(_on_mouse_entered)
	pass

func _on_mouse_exited():
	Global.neutral_exit = true
	pass
	
func _on_mouse_entered():
	Global.neutral_exit = false
	pass
