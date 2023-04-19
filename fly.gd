extends Node2D

func _ready():
	get_viewport().transparent_bg = true;
	var items = []
	var num_items = Global.num_items
	$sub_item.process_mode = PROCESS_MODE_DISABLED
	$sub_item.visible = false
	$sub_item/Line2D.visible = false
	$item.visible = false
	$item.position = get_global_mouse_position()
	$item.visible = true
	var rad_mid = PI/num_items
	var mid_item : float = num_items/2.0
	for num in range(0,num_items):
		var radian_start = PI*(num)/mid_item
		items.append($sub_item.duplicate(4))
		items[num].set_name("sub_item-" + str(num+1))
		add_child(items[num])
		items[num].position = $item.get_global_position() + Vector2(Global.distance_item, 0).rotated(radian_start+rad_mid)
		items[num].process_mode = 0
		items[num].add_to_group("sub_items")
		items[num].visible = true
	remove_child($sub_item)
	get_tree().call_group("sub_items","initial_variable")
