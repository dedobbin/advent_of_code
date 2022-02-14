with open('input', 'r') as f:
	input = []
	for line in f:
		input.append(int(line.rstrip('\n')))

	target = 2020
	for base in input:
		for add in input:
			for third in input:
				if base + add + third == target:
					print(base, add, third, base * add * third)