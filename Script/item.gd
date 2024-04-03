extends Area2D


func _ready():
	var sectors = []
	var num_items = Global.num_items
	var mid_item: float = num_items / 2.0
	var max_diametro_sector_draw = 250
	var max_diametro_sector = 400
	var min_diametro_sector = 40
	for num in range(0, num_items):
		var radian_start = PI * (num) / mid_item
		var radian_end = PI * (num + 1) / mid_item
		var radian_center = radian_end / 2 + num * (PI / mid_item) / 2

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
	pass
