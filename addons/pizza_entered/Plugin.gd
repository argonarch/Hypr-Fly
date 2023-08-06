@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('PizzaEntered2D', 'Node2D', preload("res://addons/pizza_entered/pizzaEntered.gd"), null)


func _exit_tree():
	remove_custom_type('PizzaEntered2D')
