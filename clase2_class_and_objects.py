class Persona:
    def __init__(self, nombre, edad):
        self.nombre = nombre
        self.edad = edad

    def saludar(self):
        return f"Hola, soy {self.nombre}"

# Uso
p = Persona("sebastian ojito", 20)
print(p.saludar())
