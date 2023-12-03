# frozen_string_literal: true

require_relative 'shared'

def pick_correct_numbers
  lines = Shared.read_from_file
  numbers = Shared.numbers_from_file
  symbols = %w[@ # $ % & * - + = /]
  (0..lines.count - 1).flat_map do |i|
    correct_numbers = []
    (0..lines[i].length - 1).each do |j|
      next unless symbols.include?(lines[i][j])

      adjacent_numbers = numbers.select do |number|
        number.possible_indexes_for_symbol.include?([i, j])
      end
      adjacent_numbers.each do |num|
        correct_numbers.append(num)
        numbers.delete(num)
      end
    end
    correct_numbers
  end
end

puts pick_correct_numbers.sum(&:value)
