with open('input', 'r') as f:
	input = []
	for line in f:
		input.append(line.rstrip('\n'))
	
n_valid = 0
for entry in input:
	rule_num, rule_char, pw = entry.split(" ")
	min, max = rule_num.split("-")
	rule_char = rule_char.rstrip(":")
	
	count = pw.count(rule_char)
	if count >= int(min) and count <= int(max):
		n_valid+=1

print(n_valid)
		

			
