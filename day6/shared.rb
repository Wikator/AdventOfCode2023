# frozen_string_literal: true

# Functions and classes used in both parts
module Shared
  # Stores data about race's duration and record distance
  class Race
    attr_reader :time, :distance

    def initialize(time, distance)
      @time = time
      @distance = distance
    end
  end

  def self.lines_from_file
    file_path = 'data/input.txt'
    IO.readlines(file_path).map { |line| line.chomp.strip }
  end

  def self.number_of_ways_to_win(race)
    (0..race.time).reduce(0) do |ways_to_win_accumulator, button_hold_time|
      distance = (race.time - button_hold_time) * button_hold_time
      distance > race.distance ? ways_to_win_accumulator + 1 : ways_to_win_accumulator
    end
  end
end
