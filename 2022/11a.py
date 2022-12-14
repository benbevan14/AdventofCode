from dataclasses import dataclass
from collections import deque

@dataclass
class Monkey:
	items: deque
	op: str
	divisor: int
	true: int
	false: int
	counter: int = 0

	def operate(self, val):
		if self.op[1] == '+':
			if self.op[2] == 'old':
				return val + val
			else:
				return val + int(self.op[2])
		else:
			if self.op[2] == 'old':
				return val * val
			else:
				return val * int(self.op[2])

monkeys = []

with open('input/11.txt') as file:
	input = [i for i in file.read().splitlines() if i]

for i in range(len(input) - 1):
	if input[i][0] == 'M':
		items = deque([int(item) for item in input[i + 1].split(': ')[1].split(', ')])
		op = input[i + 2].split(' = ')[1].split(' ')
		divisor = int(input[i + 3].split(' by ')[1])
		true = int(input[i + 4].split(' monkey ')[1])
		false = int(input[i + 5].split(' monkey ')[1])
		m = Monkey(items, op, divisor, true, false)
		monkeys.append(m)

for i in range(20):
	for monkey in monkeys:
		while monkey.items:
			item = monkey.items.popleft()
			item = monkey.operate(item)
			monkey.counter += 1
			item = item // 3
			if item % monkey.divisor == 0:
				monkeys[monkey.true].items.append(item)
			else:
				monkeys[monkey.false].items.append(item)

monkeys.sort(key=lambda x: x.counter, reverse=True)

print(monkeys[0].counter * monkeys[1].counter)
