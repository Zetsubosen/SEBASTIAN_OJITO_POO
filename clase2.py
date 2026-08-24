class Persona:
    def __init__(self, nombre, edad):
        self.nombre = nombre
        self.edad = edad

    def saludar(self):
            return f"hola, soy {self.nombre}"

p = Persona("Sebastian", 20)
print(p.saludar())

class Galleta:
    def __init__(self,sabor):
        self.sabor = sabor
        
        g1 = Galleta ("limon")
        g2 = Galleta ("glaseado")
        print (g1.sabor)
        print (g2.sabor)
