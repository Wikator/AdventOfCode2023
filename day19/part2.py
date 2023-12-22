from copy import deepcopy


def get_workflows(file_path):
    with open(file_path) as file:
        workflows = {}
        for line in file.readlines():
            if line == '\n':
                return workflows

            split_workflow = line.strip().split('{')
            workflows[split_workflow[0]] = split_workflow[1][0:-1].split(',')


def get_ranges(workflow, acc_path=None):
    if acc_path is None:
        acc_path = []
    for rule in workflow:
        path = evaluate_rule(rule, deepcopy(acc_path))
        if len(rule) > 1:
            if rule[1] == '>':
                acc_path.append(rule[0] + '<=' + rule[2:rule.index(':')])
            elif rule[1] == '<':
                acc_path.append(rule[0] + '>=' + rule[2:rule.index(':')])
        if path:
            all_paths.append(path)


def evaluate_rule(workflow, acc_path):
    if workflow == 'A':
        return acc_path
    elif workflow == 'R':
        return []
    else:
        split_workflow = workflow.split(':')
        if len(split_workflow) == 1:
            get_ranges(workflows[split_workflow[0]], acc_path)
            return []
        else:
            acc_path.append(split_workflow[0])

            if split_workflow[1] == 'A':
                return acc_path
            elif split_workflow[1] == 'R':
                return []
            else:
                get_ranges(workflows[split_workflow[1]], acc_path)
                return []


def map_path_to_range(path):
    new_range = {
        'x': [1, 4000],
        'm': [1, 4000],
        'a': [1, 4000],
        's': [1, 4000],
    }
    for rule in path:
        if '>' in rule:
            split_rule = rule.split('>')
            if split_rule[1][0] == '=':
                num = int(split_rule[1][1:])
                if new_range[rule[0]][0] < num:
                    new_range[rule[0]][0] = num
            else:
                num = int(split_rule[1])
                if new_range[rule[0]][0] < num + 1:
                    new_range[rule[0]][0] = num + 1
        else:
            split_rule = rule.split('<')
            if split_rule[1][0] == '=':
                num = int(split_rule[1][1:])
                if new_range[rule[0]][1] > num:
                    new_range[rule[0]][1] = num
            else:
                num = int(split_rule[1])
                if new_range[rule[0]][1] > num - 1:
                    new_range[rule[0]][1] = num - 1

    return new_range


workflows = get_workflows('data/input.txt')
all_paths = []
get_ranges(workflows['in'])
all_ranges = map(map_path_to_range, all_paths)

res = 0
for path in all_ranges:
    res += (path['x'][1] - path['x'][0] + 1) * (path['m'][1] - path['m'][0] + 1) * (
            path['a'][1] - path['a'][0] + 1) * (path['s'][1] - path['s'][0] + 1)

print(res)
