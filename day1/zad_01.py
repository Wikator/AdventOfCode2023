with open('input.txt') as file:
    lines = [line.strip() for line in file.readlines()]
    sum = 0
    for line in lines:
        first_digit = None
        last_digit = None

        for char in line:
            try:
                num = int(char)
                if first_digit is None:
                    first_digit = num

                last_digit = num
            except ValueError:
                continue
        
        sum += first_digit * 10 + last_digit

    print(sum)
