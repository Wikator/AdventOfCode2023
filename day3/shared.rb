# frozen_string_literal: true

# Functions and classes used in both problems
module Shared
  # Contains a number value, and indexes where symbol should be located
  class Number
    attr_reader :value, :possible_indexes_for_symbol

    def initialize(value, y_index, x_start_index, x_end_index)
      @value = value
      @possible_indexes_for_symbol = calculate_indexes(y_index, x_start_index, x_end_index)
    end

    private

    def calculate_indexes(y_index, x_start_index, x_end_index)
      (x_start_index - 1..x_end_index + 1).flat_map do |x_index|
        [[y_index - 1, x_index], [y_index, x_index], [y_index + 1, x_index]]
      end
    end
  end

  def self.lines_from_file
    file_path = 'data/input.txt'
    IO.readlines(file_path).map { |line| line.chomp.strip }
  end

  def self.numbers_from_lines(file)
    lines = file
    (0...lines.count).flat_map do |i|
      numbers_from_line(i, lines)
    end
  end

  def self.numbers_from_line(y_index, lines)
    line_indexes = (0...lines[y_index].length)
    numbers = line_indexes.reduce({ numbers: [], digits: [], start_index: nil }) do |acc, j|
      add_digit_to_acc(acc, j, lines, y_index)
    rescue ArgumentError
      add_number_to_acc(acc, j, y_index)
    end
    include_last_number_if_exists(line_indexes.last, numbers, y_index)
  end

  private

  private_class_method def self.sum_digits(digits)
    digits.each_with_index.sum do |(current, index)|
      current * (10**(digits.count - index - 1))
    end
  end

  private_class_method def self.include_last_number_if_exists(last_index, numbers, y_index)
    unless numbers[:start_index].nil?
      numbers[:numbers].push(Number.new(sum_digits(numbers[:digits]), y_index, numbers[:start_index], last_index))
    end
    numbers[:numbers]
  end

  private_class_method def self.add_digit_to_acc(acc, x_index, lines, y_index)
    acc[:digits].push(Integer(lines[y_index][x_index]))
    acc[:start_index] ||= x_index
    acc
  end

  private_class_method def self.add_number_to_acc(acc, x_index, y_index)
    unless acc[:start_index].nil?
      acc[:numbers].push(Number.new(sum_digits(acc[:digits]), y_index, acc[:start_index], x_index - 1))
    end
    { numbers: acc[:numbers], digits: [], start_index: nil }
  end
end
