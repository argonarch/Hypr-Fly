extends Area2D

var can_grab = false
var db : SQLite = null
var db_name := "res://DataStore/database"

func _ready():
	db = SQLite.new()
	db.path = db_name
	area_exited.connect(_on_area_exited)

func _process(delta):
	if Global.neutral_exit == true and Global.sector == "sector-"+num_of_name():
		can_grab = true
		$Line2D.visible = true
		position = get_global_mouse_position()
	else:
		$Line2D.visible = false
		can_grab = false
	$Line2D.set_point_position(0, Vector2(0,0))
	$Line2D.set_point_position(1, to_local(get_node("../item").get_global_position()))
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
		get_tree().quit()
	pass 

func num_of_name():
	for num in get_name().split():
		if num.is_valid_int():
			return  str(num)
