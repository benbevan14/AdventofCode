with open('input/10.txt') as file:
	inst = file.read().splitlines()

reg = 1
cycle = 1

interested = [20, 60, 100, 140, 180, 220]

sum = 0

for i in inst:
	if i[0] == 'n':
		if cycle in interested:
			print(cycle, reg)
			sum += cycle * reg
		cycle += 1
	else:
		for j in range(2):
			if cycle in interested:
				print(cycle, reg)
				sum += cycle * reg
			cycle += 1
		reg += int(i.split(' ')[1])

print(sum)
