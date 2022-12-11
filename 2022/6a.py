input = open('input/6.txt').read().strip()

for i in range(3, len(input)):
	l = [input[i], input[i - 1], input[i - 2], input[i - 3]]
	if len(set(l)) == 4:
		print(i + 1)
		break