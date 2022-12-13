with open('input/8.txt') as file:
	grid = file.read().splitlines()

visible = [[False for x in range(len(grid))] for x in range(len(grid))]

visible[0] = [True for x in range(len(grid))]
visible[-1] = [True for x in range(len(grid))]

for row in visible:
	row[0] = True
	row[-1] = True

dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]

for dir in dirs:
	for row in range(len(grid)):
		for col in range(len(grid)):
			if visible[row][col]:
				continue

			current = grid[row][col]
			see = True
			trow = row;
			tcol = col;
			while trow > 0 and tcol > 0 and trow < len(grid) - 1 and tcol < len(grid) - 1 and see:
				trow += dir[0];
				tcol += dir[1];
				# print(f'Comparing to {grid[row][move]} at ({row},{move})')
				if grid[trow][tcol] >= current:
					# print('Blocked, can\'t see')
					see = False
			if see:
				visible[row][col] = True

count = 0

for row in range(len(visible)):
	for col in range(len(visible)):
		if visible[row][col]:
			count += 1

print(count)
