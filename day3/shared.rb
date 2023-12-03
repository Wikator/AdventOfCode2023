# frozen_string_literal: true

# Functions and classes used in both problems
module Shared
  # Contains a number value, and indexes where symbol should be located
  class Number
    attr_reader :value

    def initialize(value, y_index, x_start_index, x_end_index)
      @value = value
      @y_index = y_index
      @x_start_index = x_start_index
      @x_end_index = x_end_index
    end

    def possible_indexes_for_symbol
      (@x_start_index - 1..@x_end_index + 1).flat_map do |x_index|
        [[@y_index - 1, x_index], [@y_index, x_index], [@y_index + 1, x_index]]
      end
    end

    def to_s
      "Number: #{@value}, y: #{@y_index}, x: #{@x_start_index} - #{@x_end_index}"
    end
  end

  def self.reduce_digits(digits)
    digits.each_with_index.reduce(0) do |accumulator, (current, index)|
      accumulator + current * (10**(digits.count - index - 1))
    end
  end

  def self.read_from_file
    file_path = 'data/input.txt' # Replace with the actual path to your file
    IO.readlines(file_path).map { |line| line.chomp.strip }
  end

  def self.numbers_from_file
    lines = read_from_file
    (0..lines.count - 1).flat_map do |i|
      line_indexes = (0..lines[i].length - 1)
      numbers = []
      digits = []
      num_start_index = nil
      line_indexes.each do |j|
        digits.append(Integer(lines[i][j]))
        num_start_index = j if num_start_index.nil?
      rescue ArgumentError
        numbers.append(Number.new(reduce_digits(digits), i, num_start_index, j - 1)) unless num_start_index.nil?
        digits = []
        num_start_index = nil
      end
      unless num_start_index.nil?
        numbers.append(Number.new(reduce_digits(digits), i, num_start_index, line_indexes.last))
      end
      numbers
    end
  end
end
