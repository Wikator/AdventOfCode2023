from shared import get_input
from copy import deepcopy

class Ranges:
    def __init__(self, xRange, mRange, aRange, sRange):
        self.xRange = xRange
        self.mRange = mRange
        self.aRange = aRange
        self.sRange = sRange


input = get_input('data/input.txt')
big_acc = []

def get_range(workflows, acc=[]):
    for workflow in workflows:
        path = get_ranges(workflow, deepcopy(acc))
        try:
            if workflow[1] == '>':
                acc.append(workflow[0] + '<=' + workflow[2:workflow.index(':')])
            elif workflow[1] == '<':
                acc.append(workflow[0] + '>=' + workflow[2:workflow.index(':')])
        except:
            pass
        if path != None and path != []:
            big_acc.append(path)


def get_ranges(workflow, acc):
    if workflow == 'A':
        return acc
    elif workflow == 'R':
        return []
    else:
        split_workflow = workflow.split(':')
        if len(split_workflow) == 1:
            return get_range(input.workflows[split_workflow[0]], deepcopy(acc))
        else:
            acc.append(split_workflow[0])

            if split_workflow[1] == 'A':
                return deepcopy(acc)
            elif split_workflow[1] == 'R':
                return []
            else:
                return get_range(input.workflows[split_workflow[1]], deepcopy(acc))



get_range(input.workflows['in'])
acc = []
for range in big_acc:
    new_range = Ranges([1, 4000], [1, 4000], [1, 4000], [1, 4000])
    for rule in range:
        split_rule = ""
        if '>' in rule:
            split_rule = rule.split('>')
            if split_rule[0] == 'x':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.xRange[0] < num:
                        new_range.xRange[0] = num
                else:
                    num = int(split_rule[1])
                    if new_range.xRange[0] < num + 1:
                        new_range.xRange[0] = num + 1
            if split_rule[0] == 'm':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.mRange[0] < num:
                        new_range.mRange[0] = num
                else:
                    num = int(split_rule[1])
                    if new_range.mRange[0] < num + 1:
                        new_range.mRange[0] = num + 1
            if split_rule[0] == 'a':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.aRange[0] < num:
                        new_range.aRange[0] = num
                else:
                    num = int(split_rule[1])
                    if new_range.aRange[0] < num + 1:
                        new_range.aRange[0] = num + 1
            if split_rule[0] == 's':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.sRange[0] < num:
                        new_range.sRange[0] = num
                else:
                    num = int(split_rule[1])
                    if new_range.sRange[0] < num + 1:
                        new_range.sRange[0] = num + 1
        else:
            split_rule = rule.split('<')
            if split_rule[0] == 'x':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.xRange[1] > num:
                        new_range.xRange[1] = num
                else:
                    num = int(split_rule[1])
                    if new_range.xRange[1] > num - 1:
                        new_range.xRange[1] = num - 1
            if split_rule[0] == 'm':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.mRange[1] > num:
                        new_range.mRange[1] = num
                else:
                    num = int(split_rule[1])
                    if new_range.mRange[1] > num - 1:
                        new_range.mRange[1] = num - 1
            if split_rule[0] == 'a':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.aRange[1] > num:
                        new_range.aRange[1] = num
                else:
                    num = int(split_rule[1])
                    if new_range.aRange[1] > num - 1:
                        new_range.aRange[1] = num - 1
            if split_rule[0] == 's':
                if split_rule[1][0] == '=':
                    num = int(split_rule[1][1:])
                    if new_range.sRange[1] > num:
                        new_range.sRange[1] = num
                else:
                    num = int(split_rule[1])
                    if new_range.sRange[1] > num - 1:
                        new_range.sRange[1] = num - 1


    acc.append(new_range)


res = 0
for range in acc:
    res += (range.xRange[1] - range.xRange[0] + 1) * (range.mRange[1] - range.mRange[0] + 1) * (range.aRange[1] - range.aRange[0] + 1) * (range.sRange[1] - range.sRange[0] + 1)
print(res)
