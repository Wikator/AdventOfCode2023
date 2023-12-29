# frozen_string_literal: true

module ModuleType
  FLIP_FLOP = 'Flip Flop'
  CONJUNCTION = 'Conjunction'
end

module PULSE
  LOW = 'Low'
  HIGH = 'High'
end

class FlipFlop
  attr_accessor :is_on
  attr_reader :type

  def initialize
    @is_on = false
    @type = ModuleType::FLIP_FLOP
  end
end

class Conjunctions
  attr_reader :type, :storage

  def initialize
    @storage = {}
    @type = ModuleType::CONJUNCTION
  end
end

def lines_from_file
  file_path = 'data/input.txt'
  IO.readlines(file_path).map { |line| line.chomp.strip }
end

def send_signal(source, destination, type, module_types, module_connections, acc)
  return unless module_connections.include?(destination)

  if destination == 'broadcaster'
    module_connections[destination].each do |connection|
      acc[type] += 1
      send_signal(destination, connection, type, module_types, module_connections, acc)
    end
  elsif module_types[destination].type == ModuleType::FLIP_FLOP && type == PULSE::LOW
    if module_types[destination].is_on
      module_types[destination].is_on = false
      module_connections[destination].each do |connection|
        acc[PULSE::LOW] += 1
        send_signal(destination, connection, PULSE::LOW, module_types, module_connections, acc)
      end
    else
      module_types[destination].is_on = true
      module_connections[destination].each do |connection|
        acc[PULSE::HIGH] += 1
        send_signal(destination, connection, PULSE::HIGH, module_types, module_connections, acc)
      end
    end
  elsif module_types[destination].type == ModuleType::CONJUNCTION
    module_types[destination].storage[source] = type
    if module_types[destination].storage.values.all? { |value| value == PULSE::HIGH }
      module_connections[destination].each do |connection|
        acc[PULSE::LOW] += 1
        send_signal(destination, connection, PULSE::LOW, module_types, module_connections, acc)
      end
    else
      module_connections[destination].each do |connection|
        acc[PULSE::HIGH] += 1
        send_signal(destination, connection, PULSE::HIGH, module_types, module_connections, acc)
      end
    end
  end
end

def part1
  lines = lines_from_file
  module_types = {}
  module_connections = {}
  lines.each do |line|
    split_line = line.split(' -> ')
    connections = split_line[1].split(', ')
    if split_line[0] == 'broadcaster'
      module_connections[split_line[0]] = Array.new(connections)
    else
      module_name = split_line[0].slice(1..-1)
      if split_line[0][0] == '%'
        module_types[module_name] = FlipFlop.new
      elsif split_line[0][0] == '&'
        module_types[module_name] = Conjunctions.new
      end
      module_connections[module_name] = Array.new(connections)
    end
  end
  module_types.filter { |_, module_type| module_type.type == ModuleType::CONJUNCTION }.each do |module_name, obj|
    module_connections.each do |source, destinations|
      obj.storage[source] = PULSE::LOW if destinations.include?(module_name)
    end
  end
  ans = { PULSE::LOW => 0, PULSE::HIGH => 0 }
  1000.times do
    send_signal(nil, 'broadcaster', PULSE::LOW, module_types, module_connections, ans)
    ans[PULSE::LOW] += 1
  end
  p ans[PULSE::LOW] * ans[PULSE::HIGH]
end

part1
