score = 0

cases = {
	"A X": 4, #D
	"A Y": 8, #W
	"A Z": 3, #L
	"B X": 1, #L
	"B Y": 5, #D
	"B Z": 9, #W
	"C X": 7, #W
	"C Y": 2, #L
	"C Z": 6  #D
}

with open('input/2.txt') as file:
	for line in file:
		l = line.rstrip()
		score += cases[l]

print(score)