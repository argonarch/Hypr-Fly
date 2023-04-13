extends Node2D

func _ready():
	get_viewport().transparent_bg = true;
	var items = []
	var sectors = []
	var num_items = 4
	$item.process_mode = 4
	$item.visible = false
	$item/Line2D.visible = false
	$Principal.visible = false
	$Principal.position = get_global_mouse_position()
	$Principal.visible = true
	var rad_mid = PI/num_items
	var mid_item : float = num_items/2.0
	var radian_mid = rad_mid*2
	var diametro_sector = 250
	for num in range(0,num_items):
		var radian_start = PI*(num)/mid_item
		var radian_end = PI*(num+1)/mid_item
		var radian_center = radian_end/2+num*(PI/mid_item)/2
		var vectores = PackedVector2Array([Vector2(0,0),Vector2(diametro_sector,0).rotated(radian_start+radian_mid),Vector2(diametro_sector,0).rotated(radian_center+radian_mid),Vector2(diametro_sector,0).rotated(radian_end+radian_mid)])
		
		items.append($item.duplicate(4))
		add_child(items[num])
		items[num].set_name("item-" + str(num+1))
		items[num].position = $Principal.get_global_position() + Vector2(120, 0).rotated(radian_start+rad_mid)
		items[num].process_mode = 0
		items[num].add_to_group("items")
		items[num].visible = true
		
		sectors.append($Principal/sector.duplicate(4))
		$Principal.add_child(sectors[num])
		sectors[num].set_name("sector-" + str(num+1))
		sectors[num].get_node("sector").set_polygon(vectores)
		sectors[num].get_node("sector-collision").set_polygon(vectores)
		"""
		print(num)
		print(mid_item)
		print(radian_end)
		print(radian_center)
		print(rad_to_deg(radian_center))
		"""
