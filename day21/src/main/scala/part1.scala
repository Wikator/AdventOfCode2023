import scala.annotation.tailrec
import scala.io.Source

case class Coords(Y: Int, X: Int)

def inputLines: Array[String] = {
  val bufferedSource = Source.fromFile("src/data/input.txt")
  val lines = bufferedSource.getLines.toArray
  bufferedSource.close
  lines
}

@tailrec
def findEndingPlotsCount(garden: Array[String], startingPositions: List[Coords], stepsLeft: Int): Int = {
  if (stepsLeft <= 0)
    garden.zipWithIndex.foreach((l, i1) => println(l.zipWithIndex.foldLeft("")((acc, curr) => if startingPositions.exists(c => c.X == curr._2 && c.Y == i1) then acc + "0" else acc + curr._1)))
    startingPositions.length
  else
    val newStartingPositions = startingPositions.foldLeft(List[Coords]())((acc, curr) => {
      val possible: List[Coords] = List(Coords(curr.Y + 1, curr.X), Coords(curr.Y - 1, curr.X), Coords(curr.Y, curr.X + 1), Coords(curr.Y, curr.X - 1))
      acc.concat(possible.foldLeft(List[Coords]())((acc1, curr1) =>
        if (curr1.X >= 0 && curr1.X < garden.head.length && curr1.Y >= 0 && curr1.Y < garden.length && garden(curr1.Y)(curr1.X) != '#' && !acc.exists((c: Coords) => c.Y == curr1.Y && c.X == curr1.X))
          curr1 :: acc1
        else
          acc1))
    })
    findEndingPlotsCount(garden, newStartingPositions, stepsLeft - 1)
}


@tailrec
def findS(garden: Array[String], index: Int = 0): Coords = {
  val possibleS = garden.head.zipWithIndex.foldLeft(None: Option[Coords])((acc, curr) => if curr._1 == 'S' then Some(Coords(index, curr._2)) else acc)
  possibleS match
    case Some(value) => value
    case None => findS(garden.tail, index + 1)
}

@main
def Main1(): Unit = {
  val lines = inputLines
  val sCoords = findS(lines)
  val endingPlotsCount = findEndingPlotsCount(lines, List(sCoords), 64)
  println(endingPlotsCount)
}