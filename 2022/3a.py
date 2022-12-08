total = 0

with open('input/3.txt') as file:
	for line in file:
		bp = line.rstrip()
		start, end = bp[:len(bp)//2], bp[len(bp)//2:]
		shared = ''.join(set(start).intersection(end))
		if (shared.lower() == shared):
			total += ord(shared) - 96
		else:
			total += ord(shared) - 38

print(total)
