with open('input', 'r') as f:
	input = []
	for line in f:
		input.append(int(line.rstrip('\n')))

	target = 2020
	for base in input:
		for add in input:
			if base + add == target:
				print(base, add, base * add)