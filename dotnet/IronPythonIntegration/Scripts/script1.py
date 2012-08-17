class script1class:
	def Hello(self):
		message = 'Hello via the script!'
		return message
	def HelloWithName(self, name):
		message = 'Hello ' + name
		return message
	def HelloWithPerson(self, person):
		message = 'Hello ' + person.Name
		return message
	def ChangeAddress(self, person):
		person.Address = 'New York'