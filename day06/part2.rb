# frozen_string_literal: true

require_relative 'shared'

def race_from_lines(lines)
  time_digits = digits_from_line(lines[0])
  distance_digits = digits_from_line(lines[1])

  time = digits_reducer(time_digits)
  distance = digits_reducer(distance_digits)
  Shared::Race.new(time, distance)
end

def digits_from_line(line)
  line.scan(/\d/).map { |digit_string| Integer(digit_string) }
end

def digits_reducer(digits)
  digits.reverse.reduce({ number: 0, next_power: 0 }) do |acc, digit|
    { number: acc[:number] + digit * 10**acc[:next_power], next_power: acc[:next_power] + 1 }
  end[:number]
end

def part2
  lines = Shared.lines_from_file
  race = race_from_lines(lines)

  Shared.number_of_ways_to_win(race)
end

p part2
