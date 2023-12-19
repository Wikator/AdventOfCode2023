class Input:
    def __init__(self, workflows, ratings):
        self.workflows = workflows
        self.ratings = ratings


def get_input(filePath):
    with open(filePath) as file:
        workflows = {}
        ratings = []
        blank_line_reached = False
        for line in file.readlines():
            if line == '\n':
                blank_line_reached = True
            elif blank_line_reached:
                ratings.append({split_part.split('=')[0]: int(split_part.split('=')[1]) for split_part in line.strip()[1:-1]
                               .split(',')})
            else:
                split_workflow = line.strip().split('{')
                workflows[split_workflow[0]] = split_workflow[1][0:-1].split(',')
        return Input(workflows, ratings)


input = get_input('data/input.txt')

res = 0

for rating in input.ratings:
    current_workflow = 'in'
    while current_workflow is not None:
        for workflow in input.workflows[current_workflow]:
            if '>' in workflow:
                split_workflow = workflow.split('>')
                if rating[split_workflow[0]] > int(split_workflow[1].split(':')[0]):
                    target = workflow.split(':')[1]
                    if target == 'A':
                        res += rating['x'] + rating['m'] + rating['a'] + rating['s']
                        current_workflow = None
                    elif target == 'R':
                        current_workflow = None
                    else:
                        current_workflow = target
                    break
            elif '<' in workflow:
                split_workflow = workflow.split('<')
                if rating[split_workflow[0]] < int(split_workflow[1].split(':')[0]):
                    target = workflow.split(':')[1]
                    if target == 'A':
                        res += rating['x'] + rating['m'] + rating['a'] + rating['s']
                        current_workflow = None
                    elif target == 'R':
                        current_workflow = None
                    else:
                        current_workflow = target
                    break
            elif workflow == 'A':
                current_workflow = None
                res += rating['x'] + rating['m'] + rating['a'] + rating['s']
                break
            elif workflow == 'R':
                current_workflow = None
                break
            else:
                current_workflow = workflow

print(res)
