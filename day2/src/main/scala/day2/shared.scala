package day2

import scala.io.Source

case class Cube(color: Char, quantity: Int)
case class Game(gameNumber: Int, cubes: Array[Array[Cube]])

def secondElement[A](array: Array[A]): A =
  array.tail.head

def inputLines: Array[String] = {
  val bufferedSource = Source.fromFile("src/data/input.txt")
  val lines = bufferedSource.getLines.toArray
  bufferedSource.close
  lines
}

def colorChar(color: String): Char =
  color match
    case "blue" => 'b'
    case "red" => 'r'
    case "green" => 'g'
    case _ => throw IllegalArgumentException(color)

def cubes(sets: Array[String]): Array[Array[Cube]] =
  sets.map(s => {
    s.split(", ").map(c => {
      val splitCube = c.split(" ")
      Cube(colorChar(secondElement(splitCube)), splitCube.head.toInt)
    })
  })


def games(): Array[Game] =
  inputLines.map(l => {
    val splitLine = l.split(": ")
    val gameNumber = secondElement(splitLine.head.split(" ")).toInt
    val sets = secondElement(splitLine).split("; ")
    Game(gameNumber, cubes(sets))
  })
