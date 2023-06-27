@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('Items2D', 'Node2D', preload("res://addons/items/Items_Script.gd"), null)


func _exit_tree():
	remove_custom_type('Items2D')
