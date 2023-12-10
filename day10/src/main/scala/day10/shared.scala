package day10

import scala.annotation.tailrec
import scala.io.Source

case class Coords(x: Int, y: Int)

def readFile(filePath: String): Array[String] = {
  val bufferedSource = Source.fromFile(filePath)
  val lines = bufferedSource.getLines.toArray
  bufferedSource.close
  lines
}

@tailrec
def findS(lines: Array[String], coords: Coords = Coords(0, 0)): Coords = {
  if (lines(coords.x)(coords.y) == 'S')
    coords
  else
    if (lines(coords.x).length - 1 == coords.y)
      findS(lines, Coords(coords.x + 1, 0))
    else
      findS(lines, Coords(coords.x, coords.y + 1))
}
