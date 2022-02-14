with open('input', 'r') as f:
	input = []
	for line in f:
		input.append(line.rstrip('\n'))
	
n_valid = 0
for entry in input:
	rule_num, rule_char, pw = entry.split(" ")
	pos1, pos2 = rule_num.split("-")
	rule_char = rule_char.rstrip(":")
	print(pw, int(pos1) + 1, int(pos2) + 1)
	valid = (pw[int(pos1)] == rule_char) ^ (pw[int(pos2)] == rule_char) 
	if (valid):
		n_valid += 1
	

print(n_valid)
		

			
