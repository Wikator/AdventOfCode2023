package day10

import scala.annotation.tailrec
import scala.math.ceil


enum Direction {
  case Up, Right, Down, Left
}

def moveFromS(lines: Array[String], sLocation: Coords): Int = {
  @tailrec
  def move(lines: Array[String], currentLocation: Coords, direction: Direction, distance: Int): Int = {
    if (currentLocation.x < 0 || currentLocation.x >= lines.length || currentLocation.y < 0 || currentLocation.y >= lines(currentLocation.x).length)
      0
    else if (lines(currentLocation.x)(currentLocation.y) == 'S')
      distance - 1
    else
      direction match
        case Direction.Up =>
          lines(currentLocation.x)(currentLocation.y) match
            case '|' => move(lines, Coords(currentLocation.x - 1, currentLocation.y), Direction.Up, distance + 1)
            case '7' => move(lines, Coords(currentLocation.x, currentLocation.y - 1), Direction.Left, distance + 1)
            case 'F' => move(lines, Coords(currentLocation.x, currentLocation.y + 1), Direction.Right, distance + 1)
            case _ => 0
        case Direction.Right =>
          lines(currentLocation.x)(currentLocation.y) match
            case '-' => move(lines, Coords(currentLocation.x, currentLocation.y + 1), Direction.Right, distance + 1)
            case 'J' => move(lines, Coords(currentLocation.x - 1, currentLocation.y), Direction.Up, distance + 1)
            case '7' => move(lines, Coords(currentLocation.x + 1, currentLocation.y), Direction.Down, distance + 1)
            case _ => 0
        case Direction.Down =>
          lines(currentLocation.x)(currentLocation.y) match
            case '|' => move(lines, Coords(currentLocation.x + 1, currentLocation.y), Direction.Down, distance + 1)
            case 'L' => move(lines, Coords(currentLocation.x, currentLocation.y + 1), Direction.Right, distance + 1)
            case 'J' => move(lines, Coords(currentLocation.x, currentLocation.y - 1), Direction.Left, distance + 1)
            case _ => 0
        case Direction.Left =>
          lines(currentLocation.x)(currentLocation.y) match
            case '-' => move(lines, Coords(currentLocation.x, currentLocation.y - 1), Direction.Left, distance + 1)
            case 'L' => move(lines, Coords(currentLocation.x - 1, currentLocation.y), Direction.Up, distance + 1)
            case 'F' => move(lines, Coords(currentLocation.x + 1, currentLocation.y), Direction.Down, distance + 1)
            case _ => 0
  }

  val up = move(lines, Coords(sLocation.x - 1, sLocation.y), Direction.Up, 1)
  val down = move(lines, Coords(sLocation.x + 1, sLocation.y), Direction.Down, 1)
  val left = move(lines, Coords(sLocation.x, sLocation.y - 1), Direction.Left, 1)
  val right = move(lines, Coords(sLocation.x, sLocation.y + 1), Direction.Right, 1)

  if (up != 0)
    ceil(up / 2).toInt + 1
  else if (down != 0)
    ceil(down / 2).toInt + 1
  else if (right != 0)
    ceil(right / 2).toInt + 1
  else
    ceil(left / 2).toInt + 1
}

@main
def Main1(): Unit = {
  val lines = readFile("src/data/input.txt")
  val sLocation = findS(lines)
  val result = moveFromS(lines, sLocation)
  println(result)
}
