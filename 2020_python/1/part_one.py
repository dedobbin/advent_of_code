from functools import reduce

with open("input") as f:
	input = f.readlines()

	def filter_func(x):
		if x < len(input):
			return int(input[x].rstrip('/n')) + int(input[x+1].rstrip('/n')) == 2020

	print(filter(filter_func, range(len(input))))
