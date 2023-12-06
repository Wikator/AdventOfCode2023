# frozen_string_literal: true

require_relative 'shared'

def race_from_lines(lines)
  split_time = lines[0].split('').reject { |str| str.eql?(' ') }
  split_distance = lines[1].split('').reject { |str| str.eql?(' ') }

  time = split_reducer(split_time.reverse)
  distance = split_reducer(split_distance.reverse)
  Shared::Race.new(time, distance)
end

def split_reducer(split)
  result = split.reduce({ number: 0, next_power: 0 }) do |accumulator, current|
    number = Integer(current)
    { number: accumulator[:number] + number * 10**accumulator[:next_power], next_power: accumulator[:next_power] + 1 }
  rescue ArgumentError
    accumulator
  end
  result[:number]
end

def part2
  lines = Shared.lines_from_file
  race = race_from_lines(lines)

  Shared.number_of_ways_to_win(race)
end

p part2
