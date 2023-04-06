extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	get_viewport().transparent_bg = true;
	var items = []
	var num_items = 4
	for num in range(0,num_items):
		print(num)
		items.append($"item-area".duplicate(4))
		add_child(items[num])
		items[num].position = $pricipal.get_global_position() + Vector2(200, 0).rotated(deg_to_rad((360.0/num_items)*num))
