extends StaticBody2D
class_name  Muro2D

var dimension : Vector2

var muro = CollisionShape2D.new()
var rectangleShape = RectangleShape2D.new()

func _ready():
	rectangleShape.set_size(dimension)
	muro.set_shape(rectangleShape)
	self.add_child(muro)
