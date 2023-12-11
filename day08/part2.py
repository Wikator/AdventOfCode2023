from shared import parse_input, find_distance
import math


parsed_file = parse_input('input.txt')
all_start_nodes = [key for key in parsed_file.nodes.keys() if key.endswith('A')]
distances = [find_distance(parsed_file, start_node, '^..Z$') for start_node in all_start_nodes]
result = math.lcm(*distances)
print(result)
