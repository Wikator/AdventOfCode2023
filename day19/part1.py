from shared import get_input


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
