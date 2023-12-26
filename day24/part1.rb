# frozen_string_literal: true

# Both position and velocity are objects of class Coords
class PositionAndVelocity
  attr_reader :position, :velocity

  def initialize(position, velocity)
    @position = position
    @velocity = velocity
  end
end

class Coords
  attr_reader :x, :y

  def initialize(x, y)
    @x = x
    @y = y
  end
end

# ay = bx + c
class LinearEquation
  attr_reader :a, :b, :c

  def initialize(a, b, c)
    @a = a
    @b = b
    @c = c
  end

  def multiply_itself(multiplier)
    @a *= multiplier
    @b *= multiplier
    @c *= multiplier
  end
end

# ay = b
class Equation
  attr_reader :a, :b

  def initialize(a, b)
    @a = a
    @b = b
  end
end

# eq1: ay = bx + c
# eq2: ay = bx + c
def linear_equation_solver(eq1, eq2)
  if eq1.b != eq2.b
    multiplier = eq2.b / eq1.b
    eq1.multiply_itself(multiplier)
  end

  reduced_equation = Equation.new(eq1.a - eq2.a, eq1.c - eq2.c)
  y = reduced_equation.b / reduced_equation.a
  x = (eq1.a * y - eq1.c) / eq1.b
  Coords.new(x, y)
end

def cross_in_area?(cross, min, max)
  cross.x >= min && cross.x <= max && cross.y >= min && cross.y <= max
end

def cross_in_the_past?(cross, line)
  if line.velocity.x.negative?
    if line.velocity.y.negative?
      cross.x > line.position.x || cross.y > line.position.y
    else
      cross.x > line.position.x || cross.y < line.position.y
    end
  elsif line.velocity.y.negative?
    cross.x < line.position.x || cross.y > line.position.y
  else
    cross.x < line.position.x || cross.y < line.position.y
  end
end

def lines_from_file
  file_path = 'data/input.txt'
  IO.readlines(file_path).map do |line|
    split_line = line.chomp.strip.scan(/-?\d+/)
    position = Coords.new(Float(split_line[0]), Float(split_line[1]))
    velocity = Coords.new(Float(split_line[3]), Float(split_line[4]))
    PositionAndVelocity.new(position, velocity)
  end
end

def group_lines(lines)
  (0...lines.count - 1).each_with_object([]) do |i1, acc|
    (i1 + 1...lines.count).each do |i2|
      acc.append([lines[i1], lines[i2]])
    end
  end
end

def part1(grouped_lines, min, max)
  grouped_lines.reduce(0) do |acc, lines|
    distance1 = lines[0].position.x / lines[0].velocity.x
    b1 = lines[0].velocity.y / lines[0].velocity.x
    c1 = distance1 * -1 * lines[0].velocity.y + lines[0].position.y
    distance2 = lines[1].position.x / lines[1].velocity.x
    b2 = lines[1].velocity.y / lines[1].velocity.x
    c2 = distance2 * -1 * lines[1].velocity.y + lines[1].position.y

    cross = linear_equation_solver(LinearEquation.new(1, b1, c1), LinearEquation.new(1, b2, c2))

    if cross_in_area?(cross, min, max) && !cross_in_the_past?(cross, lines[0]) && !cross_in_the_past?(cross, lines[1])
      acc + 1
    else
      acc
    end
  end
end

grouped_lines = group_lines(lines_from_file)
p part1(grouped_lines, 200_000_000_000_000, 400_000_000_000_000)
