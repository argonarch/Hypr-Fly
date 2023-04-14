extends Node
#for member in get_tree().get_nodes_in_group("items"):
		#	member.hide()
		#	member.queue_free()
	#print(str('se entro al area: ', area.get_name()))
"""
var area_neutral = false
var sector : String

get_node("../Principal/neutral").mouse_exited.connect(_on_neutral_mouse_exited)
get_node("../Principal/sector").mouse_exited.connect(_on_sector_mouse_exited)

_process()
	if area_neutral and sector == "sector-"+num_of_name():
		position = get_global_mouse_position()

func _on_neutral_mouse_exited():
	area_neutral = true
	print("salio mouse")
	#can_grab = true
	pass
	
func _on_sector_mouse_entered():
	
	print("mouse toco un sector")
	#can_grab = true
	pass
	
func num_of_name():
	for num in get_name().split():
		if num.is_valid_int():
			return  str(num)
			
func _on_sector_mouse_exited():
	print("saliste del sector")
	
func _on_area_exited(area):
	print(str('saliste del area: ', area.get_name()))
	sector = area.get_name()
"""

"""
for member in get_tree().get_nodes_in_group("sectors"):
	member.mouse_entered.connect(_on_sector_mouse_entered)
pass
"""
