import re


class ParsedFile:
    def __init__(self, directions, nodes):
        self.__directions = directions
        self.__nodes = nodes

    @property
    def directions(self):
        return self.__directions

    @property
    def nodes(self):
        return self.__nodes


def parse_input(file_path):
    with open(file_path) as file:
        directions = file.readline().strip()
        file.readline()
        nodes = {}
        for line in file.readlines():
            nodes_from_file = re.findall('\w\w\w', line)
            nodes[nodes_from_file[0]] = {'L': nodes_from_file[1], 'R': nodes_from_file[2]}
        return ParsedFile(directions, nodes)


def find_distance(parsed_file, current_node, end_node_regex):
    count = 0
    current_index = 0
    while re.search(end_node_regex, current_node) is None:
        try:
            current_node = parsed_file.nodes[current_node][parsed_file.directions[current_index]]
        except IndexError:
            current_index = 0
            current_node = parsed_file.nodes[current_node][parsed_file.directions[current_index]]
        count += 1
        current_index += 1

    return count
