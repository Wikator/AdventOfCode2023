# frozen_string_literal: true

require_relative 'shared'


def find_adjacent_numbers(numbers, y_index, x_index)
  numbers.select do |number|
    number.possible_indexes_for_symbol.include?([y_index, x_index])
  end
end

def find_correct_numbers(lines, symbols)
  numbers = Shared.numbers_from_lines(lines)
  (0...lines.count).flat_map do |i|
    (0...lines[i].length).each_with_object([]) do |j, acc|
      next unless symbols.include?(lines[i][j])

      find_adjacent_numbers(numbers, i, j).each do |num|
        acc.push(num)
        numbers.delete(num)
      end
    end
  end
end

def zad01
  lines = Shared.lines_from_file
  symbols = %w[@ # $ % & * - + = /]
  correct_numbers = find_correct_numbers(lines, symbols)
  correct_numbers.sum(&:value)
end

puts zad01
