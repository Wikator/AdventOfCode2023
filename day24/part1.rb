# frozen_string_literal: true

# Both position and velocity are objects of class Coords
class PositionAndVelocity
  attr_reader :position, :velocity

  def initialize(position, velocity)
    @position = position
    @velocity = velocity
  end
end

# X and Y
class Coords
  attr_reader :x, :y

  def initialize(x, y)
    @x = x
    @y = y
  end
end

# y = ax + b
class LinearEquation
  attr_reader :a, :b

  def initialize(a, b)
    @a = a
    @b = b
  end
end

# eq1: y = ax + b
# eq2: y = ax + b
def find_cross(eq1, eq2)
  reduced_equation = LinearEquation.new(eq1.a - eq2.a, eq1.b - eq2.b) # 0 = ax + b
  x = - reduced_equation.b / reduced_equation.a
  y = eq1.a * x + eq1.b
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
    a1 = lines[0].velocity.y / lines[0].velocity.x
    b1 = lines[0].position.x / lines[0].velocity.x * -1 * lines[0].velocity.y + lines[0].position.y
    a2 = lines[1].velocity.y / lines[1].velocity.x
    b2 = lines[1].position.x / lines[1].velocity.x * -1 * lines[1].velocity.y + lines[1].position.y

    cross = find_cross(LinearEquation.new(a1, b1), LinearEquation.new(a2, b2))

    if cross_in_area?(cross, min, max) && !cross_in_the_past?(cross, lines[0]) && !cross_in_the_past?(cross, lines[1])
      acc + 1
    else
      acc
    end
  end
end

grouped_lines = group_lines(lines_from_file)
p part1(grouped_lines, 200_000_000_000_000, 400_000_000_000_000)
