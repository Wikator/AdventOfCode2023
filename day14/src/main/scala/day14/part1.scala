package day14

import scala.annotation.tailrec
import scala.io.Source

case class OToDelete(line: Int, index: Int)

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
    val OsToDelete: List[OToDelete] = lines.head
      .zipWithIndex
      .foldLeft(List[OToDelete]())((acc, curr) =>
        if (curr._1 == '.')
          findOToDelete(lines.tail, curr._2) match
            case Some(value) => value :: acc
            case None => acc
        else
          acc
      )
    val newLine = lines.head
      .zipWithIndex
      .map((char, index) =>
        if (char == '.' && oExistsBelow(lines.tail, index))
          'O'
        else
          char
      )
      .foldLeft("")((acc, curr) => acc + curr)
    val newLines = lines
      .tail
      .zipWithIndex
      .map((line, lineIndex) =>
        line
          .zipWithIndex
          .map((char, charIndex) =>
            if (OsToDelete.exists(c => c.index == charIndex && c.line == lineIndex))
              '.'
            else
              char)
          .foldLeft("")((acc, curr) => acc + curr))
    moveAllUp(newLines, newLine :: acc)
}

@tailrec
def findOToDelete(lines: Array[String], index: Int, line: Int = 0): Option[OToDelete] = {
  if (lines.length == 0)
    None
  else
    lines.head(index) match
      case 'O' => Some(OToDelete(line, index))
      case '#' => None
      case '.' => findOToDelete(lines.tail, index, line + 1)
}

@tailrec
def oExistsBelow(lines: Array[String], index: Int): Boolean = {
  if (lines.length == 0)
    false
  else
    lines.head(index) match
      case 'O' => true
      case '#' => false
      case '.' => oExistsBelow(lines.tail, index)
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
