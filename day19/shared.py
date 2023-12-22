class Input:
    def __init__(self, workflows, ratings):
        self.workflows = workflows
        self.ratings = ratings


def get_input(file_path):
    with open(file_path) as file:
        workflows = {}
        ratings = []
        blank_line_reached = False
        for line in file.readlines():
            if line == '\n':
                blank_line_reached = True
            elif blank_line_reached:
                ratings.append({split_part.split('=')[0]: int(split_part.split('=')[1]) for split_part in line
                               .strip()[1:-1]
                               .split(',')})
            else:
                split_workflow = line.strip().split('{')
                workflows[split_workflow[0]] = split_workflow[1][0:-1].split(',')
        return Input(workflows, ratings)

