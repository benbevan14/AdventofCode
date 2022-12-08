total = 0
lines = []
with open('input/3.txt') as file:
	for line in file:
		lines.append(line.rstrip())

for i in range(0, len(lines), 3):
	shared = ''.join(set(lines[i]).intersection(lines[i + 1]).intersection(lines[i + 2]))
	if (shared.lower() == shared):
		total += ord(shared) - 96
	else:
		total += ord(shared) - 38

print(total)