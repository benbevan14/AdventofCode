score = 0

options = {
	"A": [2, 1, 3],
	"B": [3, 2, 1],
	"C": [1, 3, 2]
}

with open('input/2.txt') as file:
	for line in file:
		l = line.rstrip().split(' ')
		if l[1] == 'X': # Lose
			score += options[l[0]][2]
		elif l[1] == 'Y': # Draw
			score += options[l[0]][1] + 3
		else: # Win
			score += options[l[0]][0] + 6

print(score)
		