@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('PizzaPart2D', 'Area2D', preload("res://addons/pizza_part/pizzaPart.gd"), null)
	

func _exit_tree():
	remove_custom_type('PizzaPart2D')
