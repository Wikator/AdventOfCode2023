# frozen_string_literal: true

require_relative 'shared'

def races_from_lines(lines)
  split_time = lines[0].scan(/\d+/)
  split_distance = lines[1].scan(/\d+/)

  (0...split_time.count).map do |index|
    Shared::Race.new(Integer(split_time[index]), Integer(split_distance[index]))
  end
end

def part1
  lines = Shared.lines_from_file
  races = races_from_lines(lines)

  races.reduce(1) do |races_accumulator, race|
    number_of_ways_to_win = Shared.number_of_ways_to_win(race)

    races_accumulator *= number_of_ways_to_win unless number_of_ways_to_win.zero?
    races_accumulator
  end
end

p part1
