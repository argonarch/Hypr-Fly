@tool
extends Area2D
class_name PizzaPart2D
signal estado(nombre)
@export var radius : int = 100 : set = radiuser
@export var radius_interno : int = 50 : set = radiuser_inter
@export_range(0,360) var angle : float = 45 : set = angler
@export_range(2,20) var divitions : int = 2 : set = divisioner

var pizzaShape = CollisionPolygon2D.new()
var pizzaDraw = Polygon2D.new()

var vectors : PackedVector2Array = PackedVector2Array([
		Vector2(0,0),
		Vector2(100,0),
		Vector2(0,100)
		])


func _ready():
	if get_child_count() == 0:
		pizzaShape.set_polygon(vectors)
		pizzaDraw.set_polygon(vectors)
		pizzaDraw.set_color(Color( 0.0, 0.0, 0.0, 0.26))
		pizzaShape.visible = false
		pizzaShape.name = "Pizza_Shape"
		pizzaDraw.name = "Pizza_Draw"
		self.add_child(pizzaShape)
		self.add_child(pizzaDraw)
		self.mouse_exited.connect(_on_sector_mouse_exited)
		self.mouse_entered.connect(_on_sector_mouse_entered)
		pizzaDraw.set_owner(get_tree().edited_scene_root)
		pizzaShape.set_owner(get_tree().edited_scene_root)


func radiuser(radius0):
	radius = radius0
	armador(divitions,radius0,angle,radius_interno)
	
func radiuser_inter(radius_interno0):
	radius_interno = radius_interno0
	armador(divitions,radius,angle,radius_interno0)
	
func angler(angle0):
	angle = angle0
	armador(divitions,radius,angle0,radius_interno)
	
func divisioner(divitions0):
	divitions = divitions0
	armador(divitions0,radius,angle,radius_interno)

func armador(divitions1,radius1,angle1,radius_interno1):
	vectors.clear()
	
	vectors.append(Vector2(radius_interno1,0))
	for n in divitions1:
		vectors.append(Vector2(radius1,0).rotated(deg_to_rad((angle1/(divitions1-1))*n)))
		
	for n in range(divitions1-1,0,-1):
		vectors.append(Vector2(radius_interno1,0).rotated(deg_to_rad((angle1/(divitions1-1))*n)))
	pizzaDraw.polygon = PackedVector2Array(vectors)
	pizzaShape.polygon = PackedVector2Array(vectors)

func _on_sector_mouse_exited():
	pizzaDraw.set_color(Color( 0.0, 0.0, 0.0, 0.26))
	
func _on_sector_mouse_entered():
	pizzaDraw.set_color(Color( 0.36, 0.42, 0.75, 0.30))
	emit_signal("estado",self.name)

