from shared import appendix_1a


with open('data/input.txt') as file:
    line = file.readline().strip()
    all_strings = line.split(',')
    result = 0
    for string in all_strings:
        result += appendix_1a(string)
    print(result)
