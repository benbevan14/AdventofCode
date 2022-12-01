with open('input/1.txt') as f:
	elves = f.read().replace('\n', ',')

record = 0
for elf in elves.split(',,'):
	tot = 0
	for val in elf.split(','):
		tot += int(val)
	if tot > record:
		record = tot

print(record)
	