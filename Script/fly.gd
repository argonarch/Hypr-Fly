extends Node2D


func _ready():
	position = get_global_mouse_position()
	$sub_item.process_mode = PROCESS_MODE_DISABLED
	$sub_item.visible = false
	$sub_item/Line2D.visible = false
	$item.visible = false
	$item.position = Vector2(0, 0)
	$item.visible = true
	var sectors = []
	var items = []
	var num_items = Global.num_items
	var max_diametro_sector_draw = 250
	var max_diametro_sector = 400
	var min_diametro_sector = 40
	var rad_mid = PI / num_items
	var mid_item: float = num_items / 2.0
	for num in range(0, num_items):
		var radian_start = PI * (num) / mid_item
		var radian_end = PI * (num + 1) / mid_item
		var radian_center = radian_end / 2 + num * (PI / mid_item) / 2

		items.append($sub_item.duplicate(4))
		items[num].set_name("sub_item-" + str(num + 1))
		add_child(items[num])
		items[num].position = (
			$item.to_local(get_global_position())
			+ Vector2(Global.distance_item, 0).rotated(radian_start + rad_mid)
		)
		items[num].process_mode = 0
		items[num].add_to_group("sub_items")
		items[num].visible = true

		var vectores_collision = PackedVector2Array(
			[
				Vector2(min_diametro_sector, 0).rotated(radian_start),
				Vector2(max_diametro_sector, 0).rotated(radian_start),
				Vector2(max_diametro_sector, 0).rotated(radian_center),
				Vector2(max_diametro_sector, 0).rotated(radian_end),
				Vector2(min_diametro_sector, 0).rotated(radian_end),
				Vector2(min_diametro_sector, 0).rotated(radian_center)
			]
		)

		var vectores = PackedVector2Array(
			[
				Vector2.ZERO,
				Vector2(max_diametro_sector_draw, 0).rotated(radian_start),
				Vector2(max_diametro_sector_draw, 0).rotated(radian_center),
				Vector2(max_diametro_sector_draw, 0).rotated(radian_end),
			]
		)

		sectors.append($sector.duplicate(4))
		sectors[num].set_name("sector-" + str(num + 1))
		add_child(sectors[num])
		sectors[num].get_node("sector").set_polygon(vectores)
		sectors[num].get_node("sector-collision").set_polygon(vectores_collision)
		sectors[num].add_to_group("sectors")
	remove_child($sector)
	remove_child($sub_item)
	get_tree().call_group("sub_items", "initial_variable")
