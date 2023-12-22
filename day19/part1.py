class Input:
    def __init__(self, workflows, ratings):
        self.workflows = workflows
        self.ratings = ratings


def get_input(file_path):
    with open(file_path) as file:
        input = Input({}, [])
        blank_line_reached = False
        for line in file.readlines():
            if line == '\n':
                blank_line_reached = True
            elif blank_line_reached:
                input.ratings.append({split_part[0]: int(split_part[1]) for split_part in
                                      map(lambda a: a.split('='), line.strip()[1:-1].split(','))})
            else:
                split_workflow = line.strip().split('{')
                input.workflows[split_workflow[0]] = split_workflow[1][0:-1].split(',')
        return input


def compare(num1, num2, symbol):
    return (symbol == '<' and num1 < num2) or (symbol == '>' and num1 > num2)


input = get_input('data/input.txt')

res = 0

for rating in input.ratings:
    current_workflow = 'in'
    while current_workflow is not None:
        for rule in input.workflows[current_workflow]:
            if '>' in rule or '<' in rule:
                if not compare(rating[rule[0]], int(rule[2:].split(':')[0]), rule[1]):
                    continue
                target = rule.split(':')[1]
                if target == 'A':
                    res += rating['x'] + rating['m'] + rating['a'] + rating['s']
                    current_workflow = None
                elif target == 'R':
                    current_workflow = None
                else:
                    current_workflow = target
            elif rule == 'A':
                current_workflow = None
                res += rating['x'] + rating['m'] + rating['a'] + rating['s']
            elif rule == 'R':
                current_workflow = None
            else:
                current_workflow = rule
            break

print(res)
