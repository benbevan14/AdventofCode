counter = 0

with open('input/4.txt') as file:
	for line in file:
		pairs = line.split(',')
		p1 = [int(x) for x in pairs[0].split('-')]
		p2 = [int(x) for x in pairs[1].split('-')]
		l1 = [x for x in range(p1[0], p1[1] + 1)]
		l2 = [x for x in range(p2[0], p2[1] + 1)]
		u = list(set().union(l1, l2))
		if (len(u) < len(l1) + len(l2)):
			counter += 1

print(counter)