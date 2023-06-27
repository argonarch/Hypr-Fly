@tool
extends CollisionPolygon2D
class_name PizzaShape2D

@export var radius : int = 100 : set = radiuser
@export var radius_interno : int = 50 : set = radiuser_inter
@export_range(0,360) var angle : float = 45 : set = angler
@export_range(2,20) var divitions : int = 2 : set = divisioner

var vectors : PackedVector2Array = PackedVector2Array([
		Vector2(0,0),
		Vector2(100,0),
		Vector2(0,100)
		])

# Called when the node enters the scene tree for the first time.
func _ready():
	self.set_polygon(vectors)
	self.set_name("pared_shape")

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
	#print(divitions1,radius1,angle1)
	vectors.append(Vector2(radius_interno1,0))
	for n in divitions1:
		vectors.append(Vector2(radius1,0).rotated(deg_to_rad((angle1/(divitions1-1))*n)))
		#print((angle1/divitions1)*n)
		#print(n)
	#vectors.append(Vector2(radius1,0).rotated(deg_to_rad(angle1)))
	#vectors.append(Vector2(radius_interno1,0).rotated(deg_to_rad(angle1)))
	
	for n in range(divitions1-1,0,-1):
		vectors.append(Vector2(radius_interno1,0).rotated(deg_to_rad((angle1/(divitions1-1))*n)))
	self.polygon = PackedVector2Array(vectors)

