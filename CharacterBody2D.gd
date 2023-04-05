extends CharacterBody2D


var can_grab = false
var grabbed_offset = Vector2()

func _input_event(viewport, event, shape_idx):
	if event is InputEventMouseButton:
		can_grab = event.pressed
		grabbed_offset = position - get_global_mouse_position()

func _process(delta):
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT) and can_grab:
		position = get_global_mouse_position() + grabbed_offset
	$Line2D.set_point_position(0, Vector2(0,0))
	$Line2D.set_point_position(1, to_local(get_node("../pricipal").get_global_position()))
	pass

func _on_input_event(viewport, event, shape_idx):
	pass # Replace with function body.
