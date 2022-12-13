with open('input/8.txt') as file:
	grid = file.read().splitlines()

score = [[1 for x in range(len(grid))] for x in range(len(grid))]

dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]

for dir in dirs:
	for row in range(len(grid)):
		for col in range(len(grid)):
			current = grid[row][col]
			see = True
			c = 0
			trow = row;
			tcol = col;
			while trow > 0 and tcol > 0 and trow < len(grid) - 1 and tcol < len(grid) - 1 and see:
				trow += dir[0];
				tcol += dir[1];
				if grid[trow][tcol] >= current:
					see = False
				c += 1
			score[row][col] *= c
				
max = 0

for row in range(len(score)):
	for col in range(len(score)):
		if score[row][col] > max:
			max = score[row][col]

print(max)
