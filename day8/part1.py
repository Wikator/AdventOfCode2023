from shared import parse_input, find_distance


parsed_file = parse_input('input.txt')
result = find_distance(parsed_file, 'AAA', 'ZZZ')
print(result)
