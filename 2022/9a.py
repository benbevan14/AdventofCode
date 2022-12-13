head = (0, 0)
tail = (0, 0)

diffs = {
	(0, 2): (0, 1),
	(1, 2): (1, 1),
	(2, 2): (1, 1),
	(2, 1): (1, 1),
	(2, 0): (1, 0),
	(2, -1): (1, -1),
	(2, -2): (1, -1),
	(1, -2): (1, -1),
	(0, -2): (0, -1),
	(-1, -2): (-1, -1),
	(-2, -2): (-1, -1),
	(-2, -1): (-1, -1),
	(-2, 0): (-1, 0),
	(-2, 1): (-1, 1),
	(-2, 2): (-1, 1),
	(-1, 2): (-1, 1)
}

dirs = {
	'U': (0, 1),
	'R': (1, 0),
	'D': (0, -1),
	'L': (-1, 0)
}

locations = [(0, 0)]

with open('input/9.txt') as file:
	input = file.read().splitlines()

for line in input:
	parts = line.split(' ')
	for i in range(int(parts[1])):
		dir = dirs[parts[0]]
		head = (head[0] + dir[0], head[1] + dir[1])
		diff = (head[0] - tail[0], head[1] - tail[1])
		if diff in diffs.keys():
			move = diffs[diff]
			tail = (tail[0] + move[0], tail[1] + move[1])
		locations.append(tail)

print(len(set(locations)))
