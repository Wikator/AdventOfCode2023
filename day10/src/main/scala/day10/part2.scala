package day10

import scala.annotation.tailrec


@tailrec
def findBeginnings(lines: Array[String], acc: List[Coords] = List(), coords: Coords = Coords(0, 0)): List[Coords] = {
  if (lines(coords.x).length - 1 == coords.y)
    if (coords.x == 0)
      findBeginnings(lines, acc, Coords(lines.length - 1, 0))
    else
      acc
  else
    if ((lines(coords.x)(coords.y) == '|' || lines(coords.x)(coords.y) == 'J' || lines(coords.x)(coords.y) == '7') && (lines(coords.x)(coords.y + 1) == '|' || lines(coords.x)(coords.y + 1) == 'F' || lines(coords.x)(coords.y + 1) == 'L'))
      findBeginnings(lines, coords :: acc, Coords(coords.x, coords.y + 1))
    else
      findBeginnings(lines, acc, Coords(coords.x, coords.y + 1))
}

@main
def Main2(): Unit = {
  val lines = readFile("src/data/input.txt")
  println(findBeginnings(lines))
}