# frozen_string_literal: true

require_relative 'shared'

# Stores indexes of * symbols
class GearSymbol
  def initialize(y_index, x_index)
    @y_index = y_index
    @x_index = x_index
  end

  def indexes
    [@y_index, @x_index]
  end
end

# Numbers adjacent to a gear
class Gear
  def initialize(num1, num2)
    @num1 = num1
    @num2 = num2
  end

  def value
    @num1 * @num2
  end
end

def symbols_from_lines(lines)
  (0..lines.count - 1).flat_map do |i|
    (0..lines[i].length - 1).reduce([]) do |acc, j|
      if lines[i][j] == '*'
        acc.push(GearSymbol.new(i, j))
      else
        acc
      end
    end
  end
end

def calculate_acc(acc, adjacent_numbers)
  if adjacent_numbers.count == 2
    acc.push(Gear.new(adjacent_numbers[0].value, adjacent_numbers[1].value))
  else
    acc
  end
end

def find_gear(lines)
  numbers = Shared.numbers_from_lines(lines)
  symbols = symbols_from_lines(lines)

  symbols.reduce([]) do |acc, symbol|
    adjacent_numbers = numbers.select do |number|
      number.possible_indexes_for_symbol.include?(symbol.indexes)
    end
    calculate_acc(acc, adjacent_numbers)
  end
end

def zad02
  lines = Shared.lines_from_file
  gear = find_gear(lines)
  gear.sum(&:value)
end

puts zad02
