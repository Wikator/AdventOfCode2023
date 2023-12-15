def appendix_1a(string):
    result = 0
    for char in string:
        result = ((ord(char) + result) * 17) % 256
    return result
