# frozen_string_literal: true

module Shared

  class ConditionRecord
    attr_reader :springs, :groups

    def initialize(springs, groups)
      @springs = springs
      @groups = groups
    end
  end

  def self.lines_from_file
    file_path = 'data/input.txt'
    IO.readlines(file_path).map { |line| line.chomp.strip }
  end

  def self.condition_records_from_lines(lines)
    lines.map do |line|
      split_line = line.split(' ')
      ConditionRecord.new(split_line[0], split_line[1].split(',').map { |number| Integer(number) })
    end
  end
end
