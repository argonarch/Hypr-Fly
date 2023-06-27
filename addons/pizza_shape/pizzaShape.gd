@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('PizzaShape2D', 'Polygon2D', preload("res://addons/pizza_shape/pizza.gd"), null)
	# Initialization of the plugin goes here.
	pass


func _exit_tree():
	remove_custom_type('PizzaShape2D')
	# Clean-up of the plugin goes here.
	pass

