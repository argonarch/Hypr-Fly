extends Node2D

func _ready():
	var grosor = 20
	var altura = 900
	var ancho = 1600  
	var altura_pos = altura / 2
	var ancho_pos = ancho / 2
	var posicion_1 = Vector2(ancho_pos, 0)
	var posicion_2 = Vector2(0, altura_pos)
	var posicion_3 = Vector2(ancho,altura_pos)
	var posicion_4 = Vector2(ancho_pos,altura)
	var posiciones = [posicion_1,posicion_2,posicion_3,posicion_4]
	var tamano_1 = Vector2(ancho,grosor)
	var tamano_2 = Vector2(grosor,altura)
	var tamanos = [tamano_1,tamano_2,tamano_2,tamano_1]
	for i in 4:
		var muro = Muro2D.new()
		muro.dimension = tamanos[i]
		muro.set_name("muro-"+str(i))
		muro.position = posiciones[i]
		add_child(muro)
		print(i)
	pass


