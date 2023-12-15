from shared import appendix_1a


class Lens:
    def __init__(self, label, focal_length):
        self.__label = label
        self.__focal_length = focal_length

    @property
    def label(self):
        return self.__label

    @property
    def focal_length(self):
        return self.__focal_length

    @focal_length.setter
    def focal_length(self, value):
        self.__focal_length = value


def hashmap(all_strings):
    boxes = [{'number': i, 'lenses': []} for i in range(256)]
    for string in all_strings:
        if '=' in string:
            split_string = string.split('=')
            hash = appendix_1a(split_string[0])
            for i in range(len(boxes)):
                if boxes[i]['number'] != hash:
                    continue

                lens_found = False
                for j in range(len(boxes[i]['lenses'])):
                    if boxes[i]['lenses'][j].label != split_string[0]:
                        continue

                    lens_found = True
                    boxes[i]['lenses'][j].focal_length = int(split_string[1])
                    break

                if not lens_found:
                    boxes[i]['lenses'].append(Lens(split_string[0], int(split_string[1])))

                break
        else:
            split_string = string.split('-')
            hash = appendix_1a(split_string[0])
            for i in range(len(boxes)):
                if boxes[i]['number'] != hash:
                    continue

                for j in range(len(boxes[i]['lenses'])):
                    if boxes[i]['lenses'][j].label != split_string[0]:
                        continue

                    boxes[i]['lenses'].remove(boxes[i]['lenses'][j])
                    break

    return boxes


with open('data/input.txt') as file:
    line = file.readline().strip()
    all_strings = line.split(',')
    boxes = hashmap(all_strings)

    result = 0
    for box in boxes:
        for i in range(len(box['lenses'])):
            result += (1 + box['number']) * (i + 1) * box['lenses'][i].focal_length

    print(result)
