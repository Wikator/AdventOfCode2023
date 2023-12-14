package day14

import scala.annotation.tailrec
import scala.io.Source

case class OToDropDown(index: Int, line: Int)

def inputLines: Array[String] = {
  val bufferedSource = Source.fromFile("src/data/input.txt")
  val lines = bufferedSource.getLines.toArray
  bufferedSource.close
  lines
}

@tailrec
def moveAllUp(lines: Array[String], acc: List[String] = List()): Array[String] = {
  if (lines.length == 0)
    acc.toArray.reverse
  else
    var OsToDropDown: List[OToDropDown] = List()
    val newLine = lines.head
      .zipWithIndex
      .map((char, index) =>
        if (char == '.')
          bringODown(lines.tail, index) match
            case Some(value) =>
              OsToDropDown = value :: OsToDropDown
              'O'
            case None =>
              '.'
        else
          char
      )
      .foldLeft("")((acc, curr) => acc + curr)
    val newLines = lines
      .tail
      .zipWithIndex
      .map((line, index1) =>
        line
          .zipWithIndex
          .map((char, index2) =>
            if (OsToDropDown.exists(c => c.index == index2 && c.line == index1))
              '.'
            else
              char)
          .foldLeft("")((acc, curr) => acc + curr))
    moveAllUp(newLines, newLine :: acc)
}

@tailrec
def bringODown(lines: Array[String], index: Int, line: Int = 0): Option[OToDropDown] = {
  if (lines.length == 0)
    None
  else
    lines.head(index) match
      case 'O' => Some(OToDropDown(index, line))
      case '#' => None
      case '.' => bringODown(lines.tail, index, line + 1)
}

@tailrec
def sum(lines: Array[String], index: Int = 1, acc: Int = 0): Int = {
  if (lines.length == 0)
    acc
  else
    sum(lines.tail, index + 1, acc + lines.head.foldLeft(0)((acc, curr) => if curr == 'O' then acc + index else acc))
}

@main
def Main1(): Unit = {
  val lines = moveAllUp(inputLines)
  println(sum(lines.reverse))
}