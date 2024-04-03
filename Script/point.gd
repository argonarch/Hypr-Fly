extends Node2D

signal tablada(tabla, icono)

var db: SQLite = null

var nombre_sector: String = "ninguno"
var zona_salida: String
var icono_center: String = "emblem-added.png"
var tabla: String = "items"

var sectores = PizzaEntered2D.new()
var items = ItemEntered2D.new()
var zonas = Zonador2D.new()
var linea = Line2D.new()


func _ready():
	db = SQLite.new()
	db.path = Global.db_name
	var images_paths = conectionDataBase("select icon from " + tabla + ";")
	var cantidad_items = conectionDataBase("select count(*) from " + tabla + ";")[0]["count(*)"]

	var bordeado = 12 - cantidad_items
	var distancia_item = 110
	var distancia_final = 200

	linea.set_points(PackedVector2Array([0, 0]))
	linea.visible = false

	items.name = "Items"
	items.division = cantidad_items
	items.distancia = distancia_item
	items.images_path = images_paths
	items.icono = icono_center

	sectores.name = "Sectors"
	sectores.radio = distancia_final
	sectores.radio_interno = 49
	sectores.division = cantidad_items
	sectores.borde = bordeado

	zonas.name = "zonas"
	zonas.zona_1 = distancia_item
	zonas.zona_2 = distancia_final + 60

	self.add_child(zonas)
	self.add_child(sectores)
	self.add_child(linea)
	self.add_child(items)

	signal_consegidor(sectores, Callable(self, "_on_sector_estado"))
	signal_consegidor(zonas, Callable(self, "_on_zona_estado"))


func _process(delta):
	if zona_salida == "zona-1":
		linea.set_point_position(1, linea.get_local_mouse_position())
		items.get_node(nombre_sector).position = items.get_local_mouse_position()


func conectionDataBase(solicitud):
	db.open_db()
	db.query(solicitud)
	var respuesta = db.query_result
	db.close_db()
	return respuesta


func signal_consegidor(nodo, funcion):
	var hijos = nodo.get_children()
	for hijo in hijos:
		hijo.estado.connect(funcion)


func _on_sector_estado(nombre):
	if nombre_sector == "ninguno":
		pass
	else:
		items.get_node(nombre_sector).auto_posicion()
	nombre_sector = nombre
	print("el sector que ejecuta es: " + nombre_sector)


func _on_zona_estado(nombre):
	zona_salida = nombre
	print("la saliste de la zona: " + nombre)
	if zona_salida == "zona-1":
		linea.visible = true
	elif zona_salida == "zona-2":
		linea.visible = false
		print("Te saliste y se ejecutara comando de " + nombre_sector)
		var command = conectionDataBase(
			"select * from " + tabla + " where item = '" + nombre_sector + "' ;"
		)[0]["command"]
		print("El commando es: " + command)
		if command == "comandado":
			var tabla_nombre = conectionDataBase(
				"select tabla from " + tabla + " where item = '" + nombre_sector + "' ;"
			)[0]["tabla"]
			var icono_nombre = conectionDataBase(
				"select icon from " + tabla + " where item = '" + nombre_sector + "' ;"
			)[0]["icon"]
			self.visible = false
			print("la tabla a elegir es: " + tabla_nombre)
			emit_signal("tablada", tabla_nombre, icono_nombre)
		else:
			OS.create_process(command, [])
			get_tree().quit()
