numbers = {
    'one': 1,
    'two': 2,
    'three': 3,
    'four': 4,
    'five': 5,
    'six': 6,
    'seven': 7,
    'eight': 8,
    'nine': 9
}

with open('input.txt') as file:
    lines = [line.strip() for line in file.readlines()]
    sum = 0
    for line in lines:
        first_digit = None
        last_digit = None
        for i in range(len(line)):
            try:
                num = int(line[i])
                if first_digit is None:
                    first_digit = num

                last_digit = num
            except ValueError:
                for key in numbers.keys():
                    if line[i:].startswith(key):
                        num = numbers[key]
                        if first_digit is None:
                            first_digit = num

                        last_digit = num
                        break
        sum += first_digit * 10 + last_digit

    print(sum)
