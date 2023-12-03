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

  def to_s
    "y: #{@y_index}, x: #{@x_index}"
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

  def to_s
    "Number 1: #{@num1}, Number 2: #{@num2}"
  end
end

def find_symbols
  lines = Shared.read_from_file
  (0..lines.count - 1).flat_map do |i|
    (0..lines[i].length - 1).reduce([]) do |acc,j|
      if lines[i][j] == '*'
        acc.append(GearSymbol.new(i, j))
      else
        acc
      end
    end
  end
end

def find_gear
  symbols = find_symbols
  numbers = Shared.numbers_from_file

  symbols.reduce([]) do |acc, symbol|
    adjacent_numbers = numbers.select do |number|
      number.possible_indexes_for_symbol.include?(symbol.indexes)
    end
    if adjacent_numbers.count == 2
      acc.append(Gear.new(adjacent_numbers[0].value, adjacent_numbers[1].value))
    else
      acc
    end
  end
end

puts(find_gear.sum(&:value))
