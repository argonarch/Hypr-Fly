@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('ItemsEntered2D', 'Node2D', preload("res://addons/item_entered/itemEntered.gd"), null)



func _exit_tree():
	remove_custom_type('ItemsEntered2D')
