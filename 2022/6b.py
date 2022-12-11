input = open('input/6.txt').read().strip()

for i in range(13, len(input)):
	l = input[i - 13:i + 1]
	if len(set(l)) == 14:
		print(i + 1)
		break