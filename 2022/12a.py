with open('input/test.txt') as file:
	input = file.read().splitlines()

S = (0, 0)
E = (0, 0)
steps = [[0 for x in range(len(input[0]))] for x in range(len(input))]
height = [[0 for x in range(len(input[0]))] for x in range(len(input))]

for r in range(len(input)):
	for c in range(len(input[0])):
		if input[r][c] == 'S':
			steps[r][c] = 0
			height[r][c] = 0
		elif input[r][c] == 'E':
			E = (r, c)
			height[r][c] = 25
		else:
			height[r][c] = ord(input[r][c]) - 96

for row in height:
	print(row)