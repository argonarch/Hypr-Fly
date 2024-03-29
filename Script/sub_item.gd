extends Area2D

var can_grab = false

var init_position
var member_sector
var sector_status = false
var instance_fly = load("res://Scene/fly.tscn")

func _ready():
	area_exited.connect(_on_area_exited)
	get_node("../neutral").mouse_exited.connect(_on_neutral_mouse_exited)
	get_node("../neutral").mouse_entered.connect(_on_neutral_mouse_entered)

func _process(delta):
	if can_grab:
		position = get_node("..").get_local_mouse_position()
	$Line2D.set_point_position(0, Vector2(0,0))
	$Line2D.set_point_position(1, to_local(get_node("../item").get_global_position()))

func _on_area_exited(area):
	if area.get_name() == "accion" and can_grab:
		can_grab = false
		for sector in get_tree().get_nodes_in_group("sectors"):
			sector.queue_free()
		for sub_item in get_tree().get_nodes_in_group("sub_items"):
			sub_item.queue_free()
		get_node("../neutral").queue_free()
		get_node("../accion").queue_free()
		var ins_fly = instance_fly.instantiate()
		get_node("/root").add_child(ins_fly)

func initial_variable():
	for member in get_tree().get_nodes_in_group("sectors"):
		if member.get_name() == "sector-"+num_of_name():
			member_sector = member
			member.mouse_entered.connect(_on_sector_mouse_entered)
			member.mouse_exited.connect(_on_sector_mouse_exited)
	init_position = position

func _on_sector_mouse_entered():
	if Global.neutral_status:
		starter()
	else:
		sector_status = true

func _on_sector_mouse_exited():
	if Global.neutral_status:
		ender()
		sector_status = false
	else:
		sector_status = false

func _on_neutral_mouse_exited():
	if sector_status:
		starter()
		Global.neutral_status = true

func _on_neutral_mouse_entered():
	if sector_status:
		ender()
		Global.neutral_status = false

func starter():
	$Line2D.visible = true
	can_grab = true
	member_sector.get_node("sector").set_color(Color( 0.36, 0.42, 0.75, 0.30))
	
func ender():
	position = init_position
	can_grab = false
	member_sector.get_node("sector").set_color(Color( 0.0, 0.0, 0.0, 0.26))
	$Line2D.visible = false

func num_of_name():
	for num in get_name().split():
		if num.is_valid_int():
			return  str(num)
