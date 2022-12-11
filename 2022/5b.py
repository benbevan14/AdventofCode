stacks = [
	['H', 'T', 'Z', 'D'],
	['Q', 'R', 'W', 'T', 'G', 'C', 'S'],
	['P', 'B', 'F', 'Q', 'N', 'R', 'C', 'H'],
	['L', 'C', 'N', 'F', 'H', 'Z'],
	['G', 'L', 'F', 'Q', 'S'],
	['V', 'P', 'W', 'Z', 'B', 'R', 'C', 'S'],
	['Z', 'F', 'J'],
	['D', 'L', 'V', 'Z', 'R', 'H', 'Q'],
	['B', 'H', 'G', 'N', 'F', 'Z', 'L', 'D']
]

with open('input/5.txt') as file:
	for line in file:
		if (line[0] != 'm'):
			continue
	
		parts = line.rstrip('\n').split(' ')
		amt, start, end = int(parts[1]), int(parts[3]) - 1, int(parts[5]) - 1
		temp = []
		for i in range(amt):
			crate = stacks[start].pop()
			temp.append(crate)
		temp.reverse()
		for crate in temp:
			stacks[end].append(crate)


for stack in stacks:
	print(stack)