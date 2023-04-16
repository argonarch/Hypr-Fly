extends Area2D

func _ready():
	mouse_entered.connect(_on_mouse_entered)

func _on_mouse_entered():
	Global.sector = get_name()

