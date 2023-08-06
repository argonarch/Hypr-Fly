@tool
extends EditorPlugin


func _enter_tree():
	add_custom_type('Zonador2D', 'Node2D', preload("res://addons/zonador/zonador_script.gd"), null)


func _exit_tree():
	remove_custom_type('Zonador2D')
