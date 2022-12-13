pos = {
	0: (0, 0),
	1: (0, 0),
	2: (0, 0),
	3: (0, 0),
	4: (0, 0),
	5: (0, 0),
	6: (0, 0),
	7: (0, 0),
	8: (0, 0),
	9: (0, 0),
}

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
		head_move = dirs[parts[0]]
		pos[0] = (pos[0][0] + head_move[0], pos[0][1] + head_move[1])
		for j in range(1, 10):
			diff = (pos[j - 1][0] - pos[j][0], pos[j - 1][1] - pos[j][1])
			if diff in diffs.keys():
				move = diffs[diff]
				pos[j] = (pos[j][0] + move[0], pos[j][1] + move[1])
		locations.append(pos[9])

print(len(set(locations)))
