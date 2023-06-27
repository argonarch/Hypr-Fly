@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('PizzaDraw2D', 'Polygon2D', preload("res://addons/pizza_draw/drawPizza.gd"), null)


func _exit_tree():
	remove_custom_type('PizzaDraw2D')
