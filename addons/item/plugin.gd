@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('Item2D', 'Area2D', preload("res://addons/item/item.gd"), null)


func _exit_tree():
	remove_custom_type('Item2D')
