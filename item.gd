extends Area2D

var can_grab = false
var grabbed_offset = Vector2()
var db : SQLite = null
var db_name := "res://DataStore/database"


func _ready():
	db = SQLite.new()
	db.path = db_name
	area_exited.connect(_on_area_exited)
	input_event.connect(_on_input_event)
	mouse_entered.connect(_on_mouse_entered)
	get_node("../Principal/neutral").area_exited.connect(_on_neutral_area_exited)
	get_node("../Principal/neutral").area_entered.connect(_on_neutral_area_entered)


func _on_input_event(viewport, event, shape_idx):
	if event is InputEventMouseButton:
		can_grab = event.pressed
		grabbed_offset = position - get_global_mouse_position()
		

func _process(delta):
	if can_grab:
		position = get_global_mouse_position()
	$Line2D.set_point_position(0, Vector2(0,0))
	$Line2D.set_point_position(1, to_local(get_node("../Principal").get_global_position()))
	pass


func _on_area_exited(area):
	print(str('saliste del area: ', area.get_name()))
	if area.get_name() == "accion" and can_grab == true:
		db.open_db()
		var frase = "select * from items where item = '" + get_name() + "' ;"
		var pregunta = db.query(frase)
		var command = db.query_result[0]["command"]
		print("El commando es: " + command)
		OS.create_process(command, [])
		print(get_name())
		db.close_db()
	pass 

func _on_mouse_entered():
	print("tocaste el item")
	can_grab = true
	pass


func _on_neutral_area_exited(area):
	if can_grab:
		$Line2D.visible = true
	pass

func _on_neutral_area_entered(area):
	
	if can_grab:
		$Line2D.visible = false
	pass


