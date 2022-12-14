with open('input/10.txt') as file:
	inst = file.read().splitlines()

cycle = 1
register = 1

screen = [0]

for i in inst:
	if i[0] == 'n':
		sprite = [register - 1, register, register + 1]
		if (cycle - 1) % 40 in sprite:
			screen.append('#')
		else:
			screen.append('.')
		cycle += 1
	else:
		for j in range(2):
			sprite = [register - 1, register, register + 1]
			if (cycle - 1) % 40 in sprite:
				screen.append('#')
			else:
				screen.append('.')
			cycle += 1
		toadd = int(i.split(' ')[1])
		register += toadd

for i in range(1, len(screen)):
	if i % 40 == 0:
		print(screen[i])
	else:
		print(screen[i], end='')