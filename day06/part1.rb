# frozen_string_literal: true

require_relative 'shared'

def races_from_lines(lines)
  time_numbers = numbers_from_line(lines[0])
  distance_numbers = numbers_from_line(lines[1])

  (0...time_numbers.count).map { |index| Shared::Race.new(time_numbers[index], distance_numbers[index]) }
end

def numbers_from_line(line)
  line.scan(/\d+/).map { |number_string| Integer(number_string) }
end

def part1
  lines = Shared.lines_from_file
  races = races_from_lines(lines)

  races.reduce(1) { |acc, race| acc * Shared.number_of_ways_to_win(race) }
end

p part1
