with open('input/1.txt') as f:
	elves = f.read().replace('\n', ',').split(',,')

totals = []
for elf in elves:
	tot = 0
	for val in elf.split(','):
		tot += int(val)
	totals.append(tot)

totals.sort(reverse=True)
print(totals[0] + totals[1] + totals[2])
	